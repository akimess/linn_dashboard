using LinnDashboard.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.DataSource
{
    public class Servers : Base.BaseDataSource
    {
        public override Diagrams.BarChart BarChart(List<BaseFilter> filters)
        {
            throw new NotImplementedException();
        }

        public override Diagrams.PieChart PieChart(List<BaseFilter> filters)
        {
            throw new NotImplementedException();
        }

        public override Diagrams.Grid Grid(List<BaseFilter> filters)
        {
            throw new NotImplementedException();
        }

        public override Diagrams.LineChart LineChart(List<BaseFilter> filters)
        {
            throw new NotImplementedException();
        }

        public override Diagrams.EmbeddedResource EmbeddedResource(List<BaseFilter> filters)
        {
            var resource = new Diagrams.EmbeddedResource();

            resource.Content =
@"
<iframe 
    src=""/pages/servers.html""
    height=""100%""
    width=""100%"">
</iframe>";

            return resource;
        }
    }
}
