//using System;
//using System.IO;
//using System.Web;
//using System.Web.Mvc;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using System.Collections.Generic;

//namespace DBO.Data.Attributes
//{
//    [AttributeUsage(AttributeTargets.Method)]
//    public class ObjectFilterAttribute : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            var contentType = filterContext.HttpContext.Request.ContentType ?? string.Empty;
//            if (contentType.Contains("application/json") || contentType.Contains("application/x-www-form-urlencoded"))
//            {
//                StreamReader reader = new StreamReader(filterContext.HttpContext.Request.InputStream);
//                string text = HttpUtility.HtmlDecode(reader.ReadToEnd());

//                var json = JObject.Parse(text);

//                var parameters = new Dictionary<string, object>();
//                foreach (var item in filterContext.ActionParameters)
//                {
//                    if (json[item.Key] != null)
//                        parameters.Add(
//                            item.Key,
//                            JsonConvert.DeserializeObject(
//                                json[item.Key].ToString(),
//                                item.Value.GetType()));
//                }

//                foreach (var item in parameters)
//                    filterContext.ActionParameters[item.Key] = item.Value;
//            }
//        }
//    }
//}
