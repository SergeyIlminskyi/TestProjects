using System;
using System.Web;

namespace ABMCloud.Helpers
{
    public class HttpContextFactory
    {
        private static HttpContextBase _mContext;
        public static HttpContextBase Current
        {
            get
            {
                if (_mContext != null)
                    return _mContext;

                if (HttpContext.Current == null)
                    throw new InvalidOperationException("HttpContext not available");

                return new HttpContextWrapper(HttpContext.Current);
            }
        }

        public static void SetCurrentContext(HttpContextBase context)
        {
            _mContext = context;
        }
    }
}