using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Diagrams
{
    public class LineChart : Base.BaseDiagram
    {
        public class Line
        {
            public string Name { get; set; }
            public Info Info { get; set; }
        }
        public class Info
        {
            public List<int> Data { get; set; }
            public List<string> Time { get; set; }
        }
        public string XName { get; set; }
        public List<Line> Lines { get; set; }
        public string Title { get; set; }
    }
}
