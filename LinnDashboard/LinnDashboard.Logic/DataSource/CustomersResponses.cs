using LinnDashboard.Logic.Base;
using LinnDashboard.Logic.Diagrams;
using LinnDashboard.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.DataSource
{
    public class CustomersResponses : Base.BaseDataSource
    {
        public override Diagrams.BarChart BarChart(List<BaseFilter> filters)
        {
            var dataSets = new List<Diagrams.BarChart.DataSet>();
            dataSets.Add(new Diagrams.BarChart.DataSet() 
            {
                Name = "Positive",
                Data = new List<double>() { GetRandom(200, 800), GetRandom(200, 800), GetRandom(200, 800), GetRandom(200, 800) },
                FillColor = Helpers.StandardColors.Green
            });
            dataSets.Add(new Diagrams.BarChart.DataSet() 
            {
                Name = "Negative",
                Data = new List<double>() { GetRandom(700, 1200), GetRandom(700, 1200), GetRandom(700, 1200), GetRandom(700, 1200) },
                FillColor = Helpers.StandardColors.Red
            });

            var barChart = new Diagrams.BarChart 
            { 
                Labels = new List<string> { "MyInventory", "OpenOrders", "Listings", "Import / Export" },
                DataSets = dataSets,
                Title = "Module Feedback"
            };

            return barChart;
        }

        public override Diagrams.PieChart PieChart(List<BaseFilter> filters)
        {
            var dataSets = new List<Diagrams.PieChart.DataSet>();
            dataSets.Add(new Diagrams.PieChart.DataSet() {
                FillColor = Helpers.StandardColors.Red,
                Label = "Negative",
                Value = GetRandom(200, 800)
            });
            dataSets.Add(new Diagrams.PieChart.DataSet() {
                FillColor = Helpers.StandardColors.Green,
                Label = "Positive",
                Value = GetRandom(100, 200)
            });
            var pieChart = new Diagrams.PieChart { DataSets = dataSets, Title = "Feedback %" };

            return pieChart;
        }

        public override Diagrams.Grid Grid(List<BaseFilter> filters)
        {
            object[] data = new object[50];
            for(int i = 0; i < 50; i++)
            {
                var stringVar1 = @"Example of a Positive Feedback response.";
                var stringVar2 = "Example of a Negative Feedback response.";
                data[i] = new {
                    Date = DateTime.Now.AddHours(i).ToString("yyyy-MM-dd HH:mm:ss"),
                    Type = i > 25 ? Icons.Positive : Icons.Negative,
                    Info = "Module: <br> Blahblah: <br>",
                    Message = i > 25 ? Helper.NewLine(System.Security.SecurityElement.Escape(stringVar1)) : Helper.TruncateLongWords(stringVar2,15)
                };
            }
            
            var columns = new List<Diagrams.Grid.Column>();
            columns.Add(new Diagrams.Grid.Column() {Name = "Date", Type = "text", Width = 10 , Align = "center"});
            columns.Add(new Diagrams.Grid.Column() { Name = "Type", Type = "text", Width = 10, Align = "center" });
            columns.Add(new Diagrams.Grid.Column() { Name = "Info", Type = "text", Width = 40, Align = "left" });
            columns.Add(new Diagrams.Grid.Column() { Name = "Message", Type = "text", Width = 90, Align = "left" });

            var grid = new Diagrams.Grid() { Columns = columns, Rows = data};

            return grid;
        }

        public override Diagrams.LineChart LineChart(List<BaseFilter> filters)
        {

            var count = 0;
            var selectedPeriod = "";
            var selectedTime = "";
            foreach(BaseFilter filter in filters)
            {
                if(filter.Name == "Period")
                {
                    selectedPeriod = filter.SelectedValue;
                }
                else if(filter.Name == "Time Scale")
                {
                    selectedTime = filter.SelectedValue;
                }
            }
            if(selectedPeriod == "Last day")
            {
                count = 24;
            }
            else if(selectedPeriod == "Last week")
            {
                if (selectedTime == "Hour")
                {
                    count = 24 * 7;
                }else if(selectedTime == "Day")
                {
                    count = 7;
                }
            }
            else if(selectedPeriod == "Last month")
            {
                if(selectedTime == "Hour")
                {
                    count = 24 * 30;
                }else if(selectedTime == "Day")
                {
                    count = 30;
                }
            }else if(selectedPeriod == "Last year")
            {
                if (selectedTime == "Hour")
                {
                    count = 24 * 365;
                }
                else if (selectedTime == "Day")
                {
                    count = 365;
                }else if(selectedTime == "Month")
                {
                    count = 12;
                }
            }

            List<string> names = new List<string>() { "Linnworks Desktop", "LinnLive", "LW.NET", "MR" };
            List<LineChart.Line> lines = new List<Diagrams.LineChart.Line>();
            foreach(var name in names)
            {
                var data = new List<int>();
                var time = new List<string>();
                for(int i = 0; i < count; i++)
                {
                    if (name == "Linnworks Desktop")
                    {
                        data.Add(GetRandom(50, 55));

                    } else if (name == "LinnLive") {
                        data.Add(GetRandom(100, 110));
                    }
                    else if (name == "LW.NET")
                    {
                        data.Add(GetRandom(200, 220));
                    }
                    else if(name == "MR")
                    {
                        data.Add(GetRandom(10, 12));
                    }


                    if (selectedTime == "Hour")
                    {
                        time.Add(DateTime.Now.AddHours(i).ToString("yyyy-MM-ddTHH:mm:ss"));
                    }
                    else if (selectedTime == "Day")
                    {
                        time.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-ddTHH:mm:ss"));
                    }
                    else if (selectedTime == "Month")
                    {
                        time.Add(DateTime.Now.AddMonths(i).ToString("yyyy-MM-ddTHH:mm:ss"));
                    }
                    
                }
                lines.Add(new Diagrams.LineChart.Line() {Name = name, Info = new Diagrams.LineChart.Info() { Data = data, Time = time} });
            }
            var lineChart = new LineChart() {Lines = lines, XName = "Time", Title = "Line Chart Diagram" };

            return lineChart;
        }

        Random random = new Random(DateTime.UtcNow.Millisecond);

        private int GetRandom(int startNumber, int endNumber)
        {
            return random.Next(startNumber, endNumber);
        }

        public override Diagrams.EmbeddedResource EmbeddedResource(List<BaseFilter> filters)
        {
            throw new NotImplementedException();
        }
    }
}
