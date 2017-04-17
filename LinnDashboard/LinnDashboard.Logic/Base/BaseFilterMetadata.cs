using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Base
{
    class BaseFilterMetadata : BaseFilter
    {
        public List<string> PossibleValues;
        public Dictionary<string, string[]> DisabledValues;
        public string Dependant;
    }
}
