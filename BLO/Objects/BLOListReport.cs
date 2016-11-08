using System.Web.Mvc;
using BLO.Managers;

namespace BLO.Objects
{
    public class BLOListReport<T> : ReportListing.ListReport<T>
    {
        private Config _config;
        protected Config Config
        {
            get
            {
                if (_config == null)
                    _config = this.Controller.Config();
                return _config;
            }
        }

        public BLOListReport(Controller controller)
            : base(controller)
        {
        }
    }
}
