using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinnDashboard.Logic.Diagrams
{
    public class Grid : Base.BaseDiagram
    {

        public class Column
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int Width { get; set; }
            public string Align { get; set; }
        }

        
        public List<Column> Columns { get; set; }
        public object[] Rows { get; set; }

        
    }
}