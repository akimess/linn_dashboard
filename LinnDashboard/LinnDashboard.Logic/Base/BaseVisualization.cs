using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Base
{
    public class BaseVisualization
    {
        public Guid Id = Guid.NewGuid();

        public Base.BaseDataSource DataSource;
        public Base.BaseDiagram Diagram;

        public int Left;
        public int Top;

        public int Width;
        public int Height;

        public int Timeout;
    }
}
