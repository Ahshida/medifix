using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using BLO.Objects;
using BLO.Objects.Enums.MediFast;
using DBO.Data.Managers;
using MediFast.dbo;

namespace BLO
{
    public class Config
    {
        private HttpContextBase _contextBase;
        public Config(HttpContextBase context)
        {
            this._contextBase = context;
        }
        private Page _page;
        public Config(Page page)
        {
            this._page = page;
        }
        private HttpContext _context;
        public Config(HttpContext context)
        {
            this._context = context;
        }
        private Controller _controller;
        public Config(Controller controller)
        {
            this._controller = controller;
            this._contextBase = controller.HttpContext;
        }

        public NameValueCollection QueryString
        {
            get
            {
                if (this._context != null)
                    return this._context.Request.QueryString;
                if (this._contextBase != null)
                    return this._contextBase.Request.QueryString;
                if (this._page != null)
                    return this._page.Request.QueryString;
                return null;
            }
        }

        public RouteData RouteData
        {
            get
            {
                return RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
            }
        }

        public NameValueCollection Form
        {
            get
            {
                if (this._context != null)
                    return this._context.Request.Form;
                if (this._contextBase != null)
                    return this._contextBase.Request.Form;
                if (this._page != null)
                    return this._page.Request.Form;
                return null;
            }
        }

        public HttpCookieCollection Cookies
        {
            get
            {
                if (this._context != null)
                    return this._context.Request.Cookies;
                else if (this._contextBase != null)
                    return this._contextBase.Request.Cookies;
                else
                    return this._page.Request.Cookies;
            }
        }

        public void SetCookies(string name, string value)
        {
            SetCookies(name, value, DateTime.Now.AddMonths(1));
        }
        public void SetCookies(string name, string value, DateTime expires)
        {
            var cookie = new HttpCookie(name, value) { Expires = expires };
            if (this._context != null)
                this._context.Response.Cookies.Add(cookie);
            else if (this._contextBase != null)
                this._contextBase.Response.Cookies.Add(cookie);
            else
                this._page.Response.Cookies.Add(cookie);
        }

        public string GetParams(string key)
        {
            var value = this.QueryString[key];
            if (string.IsNullOrEmpty(value))
                value = this.Form[key];
            if (string.IsNullOrEmpty(value) && this._controller != null)
            {
                var valueProvider = this._controller.ValueProvider.GetValue(key);
                if (valueProvider != null)
                    value = valueProvider.RawValue.ConvertTo();
            }
            if (string.IsNullOrEmpty(value) && this.RouteData != null)
                value = (this.RouteData.Values[key] ?? "").ConvertTo<string>();

            return value ?? "";
        }

        public void ResetConfig()
        {
            if (this._context != null)
                this._context.Session.RemoveAll();
            else if (this._contextBase != null)
                this._contextBase.Session.RemoveAll();
            else
                this._page.Session.RemoveAll();

            var cookieNames = this.Cookies.AllKeys;
            foreach (var cookie in cookieNames)
                SetCookies(cookie, null, DateTime.Now.AddDays(-1));
        }

        public T GetSession<T>(string key)
        {
            if (HttpContext.Current.Session == null)
                return default(T);
            else
                return HttpContext.Current.Session[key].ConvertTo<T>();
        }
        public void SetSession(string key, object value)
        {
            if (HttpContext.Current.Session == null)
                return;

            if (value == null)
            {
                HttpContext.Current.Session.Remove(key);
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        public bool IsAllowedAccess
        {
            get
            {
                if (!AppSettings.IsTestingServer)
                    return true;

                var value = this.GetSession<bool?>("a");
                if (!value.HasValue)
                {
                    var temp = this.GetParams("a");
                    if (string.IsNullOrEmpty(temp))
                        return false;
                    this.SetSession("a", temp.ConvertTo<bool>());
                    value = this.GetSession<bool?>("a");
                }
                return value.Value;
            }
        }

        //private int _userID
        //{
        //    get { return this.GetSession<int>("SessionUserID"); }
        //    set { this.SetSession("SessionUserID", value); }
        //}
        //private int _userID
        //{
        //    get
        //    {
        //        IPrincipal user = null;
        //        if (_context != null)
        //            user = this._context.User;
        //        else if (_contextBase != null)
        //            user = this._contextBase.User;

        //        string c = string.Empty;
        //        if (user != null && user.Identity != null)
        //            c = user.Identity.Name;

        //        if (!string.IsNullOrEmpty(c))
        //            return c.Decrypt<int>();
        //        return UserInfo.AnonymousUser;
        //    }
        //}
        private int _userID
        {
            get
            {
                var c = this.Cookies["medifast"];
                if (c != null)
                    return c.Value.Decrypt<int>();
                return UserInfo.AnonymousUser;
            }
            set { this.SetCookies("medifast", value.Encrypt()); }
        }

        public void SetUserID(int userID, bool rememberMe)
        {
            this.ResetConfig();
            this.SetCookies("medifast", userID.Encrypt(), rememberMe ? DateTime.Now.AddMonths(1) : DateTime.MinValue);
        }

        protected UserInfo UserInfoFromSession
        {
            get
            {
                return this.GetSession<UserInfo>("UserInfo");
            }
            set
            {
                this.SetSession("UserInfo", value);
            }
        }

        public UserInfo UserInfo
        {
            get
            {
                var user = UserInfoFromSession;

                if (user == null || user.ID != this._userID)
                {
                    if (this._userID > 0)
                        user = new UserInfo(this._userID);
                    if (user == null)
                        user = new UserInfo();
                    UserInfoFromSession = user;
                }
                return user;
            }
        }

        private T_COMPANY _company;
        public T_COMPANY Company
        {
            get
            {
                if (_company == null && this.UserInfo.CompanyID > 0)
                    _company = T_COMPANY.SelectExact(this.UserInfo.CompanyID.Value);
                if (_company == null)
                    _company = new T_COMPANY();
                return _company;
            }
        }

        public bool IsLoggedIn { get { return this.UserInfo.ID != UserInfo.AnonymousUser; } }
        public bool IsAdmin { get { return this.UserInfo.Role == Enum_USERROLE.Administrator; } }
        public bool IsSuperAdmin { get { return this.IsAdmin && (!this.UserInfo.CompanyID.HasValue || this.Company.CompanyType == Enum_COMPANYTYPE.Medifast); } }
        public bool IsInsurer { get { return this.Company.CompanyType == Enum_COMPANYTYPE.InsuranceCompany; } }
        public bool IsMedifast { get { return this.Company.CompanyType == Enum_COMPANYTYPE.Medifast; } }
    }
}

namespace BLO.Managers
{
    public static class ConfigExtension
    {
        public static Config Config(this HttpContextBase context)
        {
            return new Config(context);
        }
        public static Config Config(this Controller controller)
        {
            return new Config(controller);
        }
    }
}