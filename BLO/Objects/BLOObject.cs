using System.Web;

namespace BLO.Objects
{
    public class BLOObject
    {
        private Config _config;
        protected Config Config
        {
            get
            {
                if (_config == null)
                    _config = new Config(HttpContext.Current);
                return _config;
            }
        }
    }
}
