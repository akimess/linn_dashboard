using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LinnDashboard.Logic.Base
{
    public abstract class BaseDataSource
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public abstract Diagrams.BarChart BarChart(List<BaseFilter> filters);
        public abstract Diagrams.PieChart PieChart(List<BaseFilter> filters);
        public abstract Diagrams.Grid Grid(List<BaseFilter> filters);
        public abstract Diagrams.LineChart LineChart(List<BaseFilter> filters);
        public abstract Diagrams.EmbeddedResource EmbeddedResource(List<BaseFilter> filters);
    }
}