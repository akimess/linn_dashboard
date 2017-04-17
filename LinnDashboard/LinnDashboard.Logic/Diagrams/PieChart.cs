using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinnDashboard.Logic.Diagrams
{
    public class PieChart : Base.BaseDiagram
    {
        public class DataSet
        {
            public string Label { get; set; }
            public double Value { get; set; }
            public string FillColor { get; set; }
        }

        public List<DataSet> DataSets { get; set; }
        public string Title { get; set; }
    }
}