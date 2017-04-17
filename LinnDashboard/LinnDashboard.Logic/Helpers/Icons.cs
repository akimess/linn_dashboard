using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinnDashboard.Logic.Helpers
{
    public class Icons
    {
        public static string Positive
        {
            get
            {
                return "<i class='fa fa-smile-o response-icon' style='color:"+Helpers.StandardColors.Green+"'></i>";
            }
        }

        public static string Negative
        {
            get
            {
                return "<i class='fa fa-frown-o response-icon' style='color:" + Helpers.StandardColors.Red + "'></i>";
            }
        }

        public static string Cancellation
        {
            get
            {
                return "<i class='fa fa-times response-icon'></i>";
            }
        }
    }
}
