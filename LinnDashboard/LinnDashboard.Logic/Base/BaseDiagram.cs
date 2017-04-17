using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Base
{
    public abstract class BaseDiagram
    {
        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public List<BaseFilter> Filters;
    }
}
