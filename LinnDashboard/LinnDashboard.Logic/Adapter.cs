using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic
{
    public class Adapter
    {
        public static List<Base.BaseDashboard> GetDashboards()
        {
            var dashboards = new List<Base.BaseDashboard>();

            // Global
            var global = new Base.BaseDashboard();
            global.Name = "Global";

            global.Visualizations = new List<Base.BaseVisualization>();
            //global.Visualizations.Add(new Base.BaseVisualization()
            //{
            //    DataSource = new DataSource.Exceptions(),
            //    Diagram = new Diagrams.EmbeddedResource(),
            //    Width = 40,
            //    Height = 40,
            //    Timeout = -1,
            //    Left = 0,
            //    Top = 0
            //});
            global.Visualizations.Add(new Base.BaseVisualization()
            {
                DataSource = new DataSource.CustomersResponses(),
                Diagram = new Diagrams.LineChart()
                {
                    Filters = new List<Base.BaseFilter>
                    {
                        new Base.BaseFilterMetadata()
                        {
                            Name = "Period",
                            SelectedValue = "Last day",
                            PossibleValues = new List<string>()
                            {
                                "Last day",
                                "Last week",
                                "Last month",
                                "Last year"
                            }
                        },
                        new Base.BaseFilterMetadata()
                        {
                            Name = "Time Scale",
                            SelectedValue = "Hour",
                            PossibleValues = new List<string>() {
                                "Hour",
                                "Day",
                                "Month"
                            },
                            DisabledValues = new Dictionary<string, string[]>
                            {
                                {"Last day", new string[] {"Day","Month" } },
                                {"Last week", new string[] {"Month" } },
                                {"Last month", new string[] {"Month" } }
                            },
                            Dependant = "Period"
                        }
                    }
                },
                Width = 50,
                Height = 30,
                Timeout = 10000,
                Left = 0,
                Top = 30
            });
            global.Visualizations.Add(new Base.BaseVisualization()
            {
                DataSource = new DataSource.CustomersResponses(),
                Diagram = new Diagrams.PieChart() { },
                Width = 15,
                Height = 30,
                Timeout = 500000,
                Left = 3,
                Top = 65
            });
            global.Visualizations.Add(new Base.BaseVisualization()
            {
                DataSource = new DataSource.CustomersResponses(),
                Diagram = new Diagrams.BarChart(),
                Width = 30,
                Height = 30,
                Timeout = 200000,
                Left = 19,
                Top = 65
            });

            global.Visualizations.Add(new Base.BaseVisualization()
            {
                DataSource = new DataSource.CustomersResponses(),
                Diagram = new Diagrams.Grid(),
                Width = 50,
                Height = 100,
                Timeout = 200000,
                Left = 50,
                Top = 0
            });

            dashboards.Add(global);

            // Development


            // Servers
            //var servers = new Base.BaseDashboard();
            //servers.Name = "Servers";

            //servers.Visualizations = new List<Base.BaseVisualization>();
            //servers.Visualizations.Add(new Base.BaseVisualization()
            //{
            //    DataSource = new DataSource.Servers(),
            //    Diagram = new Diagrams.EmbeddedResource(),
            //    Width = 100,
            //    Height = 100,
            //    Timeout = 30000,
            //    Left = 0,
            //    Top = 0
            //});

            //dashboards.Add(servers);

           // Exceptions
           //var exceptions = new Base.BaseDashboard();
           // exceptions.Name = "Exceptions";

           // exceptions.Visualizations = new List<Base.BaseVisualization>();
           // exceptions.Visualizations.Add(new Base.BaseVisualization()
           // {
           //     DataSource = new DataSource.Exceptions(),
           //     Diagram = new Diagrams.EmbeddedResource(),
           //     Width = 100,
           //     Height = 100,
           //     Timeout = -1,
           //     Left = 0,
           //     Top = 0
           // });


           // //dashboards.Add(exceptions);

            return dashboards;
        }

        public static Base.BaseDiagram GetDataSource(string dataSource, string diagram, List<Base.BaseFilter> filters)
        {
            var dataSourceType = Type.GetType("LinnDashboard.Logic.DataSource." + dataSource);
            if (dataSourceType == null)
                throw new Exception("DataSource doesn't exist.");

            var dataSourceInstance = Activator.CreateInstance(dataSourceType);

            var parameters = new object[1] { filters };

            var diagramCall = dataSourceType.GetMethod(diagram);
            var diagramCallResult = diagramCall.Invoke(dataSourceInstance, parameters) as Base.BaseDiagram;

            return diagramCallResult;
        }
    }
}
