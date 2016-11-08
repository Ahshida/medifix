using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace DBO.Data.Managers
{
    public static class PathManager
    {
        public static string FromRoot(this string url, params object[] parameters)
        {
            return url.FromRoot(false, parameters);
        }
        public static string FromRoot(this string url, bool useFullURL, params object[] parameters)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            if (parameters != null && parameters.Length > 0)
                url = string.Format(url, parameters);
            if (url.StartsWith("/"))
            {
                string root;
                try
                {
                    if (useFullURL)
                    {
                        root = ConfigurationManager.AppSettings["AppUrl"];
                        if (string.IsNullOrEmpty(root))
                        {
                            var uri = HttpContext.Current.Request.Url;
                            var port = uri.Port;
                            root = "{0}://{1}{2}{3}".FormatWith(
                                uri.Scheme,
                                uri.Host,
                                uri.Port == 80
                                    ? string.Empty
                                    : ":" + port,
                                HttpContext.Current.Request.ApplicationPath);
                        }
                    }
                    else
                        root = HttpContext.Current.Request.ApplicationPath;
                }
                catch
                {
                    root = ConfigurationManager.AppSettings["AppUrl"];
                }

                if (url.ToLower().StartsWith(root.ToLower()))
                    return url;

                if (!root.EndsWith("/"))
                    root += "/";

                url = url.Substring(1, url.Length - 1);
                return string.Format("{0}{1}", root, url);
            }
            else if (url.StartsWith(@"\") && !url.StartsWith(@"\\"))
            {
                string root = AppDomain.CurrentDomain.BaseDirectory;
                var folder = @"\{0}\".FormatWith(HttpRuntime.AppDomainAppPath.Split(@"\".ToArray(), StringSplitOptions.RemoveEmptyEntries).Last());
                if (url.StartsWith(folder))
                    url = url.Substring(folder.Length);
                return PathManager.PathCombine(PhysicalPath, url);
            }

            return url;
        }
        public static string _physicalPath;
        public static string PhysicalPath
        {
            get
            {
                if (string.IsNullOrEmpty(_physicalPath))
                {
                    _physicalPath = ConfigurationManager.AppSettings["PhysicalPath"];
                    if (string.IsNullOrEmpty(_physicalPath))
                        _physicalPath = AppDomain.CurrentDomain.BaseDirectory;
                    else
                        _physicalPath = ConfigurationManager.AppSettings["PhysicalPath"];
                }
                return _physicalPath;
            }
        }

        public static string UrlCombine(params string[] parts)
        {
            return PathCombine(parts).Replace(@"\", "/");
        }
        public static string PathCombine(params string[] parts)
        {
            var items = new List<string>();
            var count = 0;
            foreach (var part in parts)
            {
                string item = part;
                if (item == null)
                    continue;
                if (count > 0 && (item.StartsWith("/") || item.StartsWith(@"\")))
                    item = item.Substring(1);
                if (item.EndsWith("/") || item.EndsWith(@"\"))
                    item = item.Substring(0, item.Length - 1);
                item = item.Replace("%20", " ");
                items.Add(item);
                count++;
            }
            return string.Join(@"\", items).Replace("/", @"\");
        }

        public static Uri AddQuery(this Uri uri, string name, object value)
        {
            var ub = new UriBuilder(uri);

            // decodes urlencoded pairs from uri.Query to HttpValueCollection
            var httpValueCollection = HttpUtility.ParseQueryString(uri.Query);
            httpValueCollection.Add(name, value.ConvertTo<string>());

            // this code block is taken from httpValueCollection.ToString() method
            // and modified so it encodes strings with HttpUtility.UrlEncode
            if (httpValueCollection.Count == 0)
                ub.Query = String.Empty;
            else
            {
                var sb = new StringBuilder();

                for (int i = 0; i < httpValueCollection.Count; i++)
                {
                    string text = httpValueCollection.GetKey(i);
                    {
                        text = HttpUtility.UrlEncode(text);

                        string val = (text != null) ? (text + "=") : string.Empty;
                        string[] vals = httpValueCollection.GetValues(i);

                        if (sb.Length > 0)
                            sb.Append('&');

                        if (vals == null || vals.Length == 0)
                            sb.Append(val);
                        else
                        {
                            if (vals.Length == 1)
                            {
                                sb.Append(val);
                                sb.Append(HttpUtility.UrlEncode(vals[0]));
                            }
                            else
                            {
                                for (int j = 0; j < vals.Length; j++)
                                {
                                    if (j > 0)
                                        sb.Append('&');

                                    sb.Append(val);
                                    sb.Append(HttpUtility.UrlEncode(vals[j]));
                                }
                            }
                        }
                    }
                }

                ub.Query = sb.ToString();
            }

            return ub.Uri;
        }
    }
}
