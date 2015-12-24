using System.Runtime.Remoting.Messaging;
using System.Web;

namespace AbiokaScrum.Api.Authentication
{
    public class ContextHolder
    {
        public static object GetData(string name) {
            if (HttpContext.Current != null) {
                return HttpContext.Current.Items[name];
            } else {
                return CallContext.GetData(name);
            }
        }
        public static void SetData(string name, object data) {
            if (HttpContext.Current != null) {
                HttpContext.Current.Items[name] = data;
            } else {
                CallContext.SetData(name, data);
            }
        }
    }
}