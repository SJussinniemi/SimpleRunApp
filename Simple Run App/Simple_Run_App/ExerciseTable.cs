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
        public string Test { get; set; }
    }
}
