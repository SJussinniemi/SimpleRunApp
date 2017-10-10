using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Simple_Run_App
{
    public class CoordinatesTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int INDEX { get; set; }
        public double LATITUDE { get; set; }
        public double LONGITUDE { get; set; }
    }
}
