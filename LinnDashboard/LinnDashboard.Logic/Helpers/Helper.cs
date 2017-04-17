using LinnDashboard.Logic.Diagrams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Helpers
{
    public class Helper
    {
        public static string TruncateLongWords(string text, int length)
        {
            var splitText = text.Split(' ');
            var truncated = splitText.Select(s => s.Length > length ? s.Substring(0,length)+"..." : s);
            return string.Join(" ", truncated);
        }

        public static string NewLine(string text)
        {
            return text.Replace(System.Environment.NewLine, "<br>");
        }
        //public object[][] PrepareLineChart(LineChart lineChart)
        //{
        //    var result = new object[lineChart.Lines[0].Info.Data.Count][];

        //    for(int x = 0; x < lineChart.Lines[0].Info.Data.Count; x++)
        //    {
        //        List<object> values = new List<object>();
        //        values.Add(lineChart.Lines[0].Info.Time[x]);
        //        for(int y = 0; y< lineChart.Lines.Count; y++) {
        //            values.Add(lineChart.Lines[y].Info.Data[x]);
        //        }
        //        result[x] = values.ToArray();
        //    }

        //    return result;
        //}
    }
}
