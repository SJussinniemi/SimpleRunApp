using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Simple_Run_App
{
    public class ExerciseTable
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DATETIME { get; set; }
        [MaxLength(10)]
        public string DURATION { get; set; }
        [MaxLength(10)]
        public string DISTANCE { get; set; }
        [MaxLength(10)]
        public string AVGSPEED { get; set; }
    }
}
