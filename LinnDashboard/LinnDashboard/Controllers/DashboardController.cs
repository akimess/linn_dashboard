using LinnDashboard.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LinnDashboard.Controllers
{
    public class DashboardController : ApiController
    {
        [ActionName("GetDashboards")]
        public List<LinnDashboard.Logic.Base.BaseDashboard> GetDashboards()
        {
            return LinnDashboard.Logic.Adapter.GetDashboards();
        }

        [ActionName("GetDataSource")]
        public LinnDashboard.Logic.Base.BaseDiagram GetDataSource(string dataSource, string diagram, List<BaseFilter> filters)
        {
            return LinnDashboard.Logic.Adapter.GetDataSource(dataSource, diagram, filters);
        }
    }
}
