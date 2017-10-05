using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Run_App
{
    public class History
    {
        public History(string timer, string distance, string speed)
        {
            this.Timer = timer;
            this.Distance = distance;
            this.Speed = speed;
        }
        public string Timer { set; get; }
        public string Distance { set; get; }
        public string Speed { set; get; }
    }
}
