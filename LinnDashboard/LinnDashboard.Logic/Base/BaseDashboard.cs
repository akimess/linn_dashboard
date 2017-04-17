using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Base
{
    public class BaseDashboard
    {
        public Guid Id = Guid.NewGuid();
        public string Name;
        public List<Base.BaseVisualization> Visualizations;
    }
}
