using LinnDashboard.Logic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.DataSource
{
    public class Exceptions : Base.BaseDataSource
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
    src=""http://logstore.linnworks.net/app/kibana#/dashboard/Exceptions?embed&_g=(refreshInterval:(display:'1%20minute',pause:!f,section:2,value:60000),time:(from:now-24h,mode:quick,to:now))&_a=(filters:!(),options:(darkTheme:!t),panels:!((col:1,columns:!(message,_type,database,module),id:Exceptions,row:5,size_x:12,size_y:5,sort:!('@timestamp',desc),type:search),(col:1,id:%5BLinechart%5D-Exceptions-count-per-type-and-per-time,row:1,size_x:7,size_y:4,type:visualization),(col:8,id:%5BVertical-Bar-Chart%5D-Exceptions-per-type-and-per-module,row:1,size_x:5,size_y:4,type:visualization)),query:(query_string:(analyze_wildcard:!t,query:'*')),title:Exceptions)""
    height=""100%""
    width=""100%"">
</iframe>
";

            return resource;
        }
    }
}
