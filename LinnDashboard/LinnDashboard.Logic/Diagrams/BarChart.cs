using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinnDashboard.Logic.Diagrams
{
    public class BarChart : Base.BaseDiagram
    {
        public class DataSet
        {
            public string Name { get; set; }
            public string FillColor { get; set; }
            public List<double> Data { get; set; }    
        }

        public List<string> Labels { get; set; }
        public List<DataSet> DataSets { get; set; }
        public string Title { get; set; }
    }
}