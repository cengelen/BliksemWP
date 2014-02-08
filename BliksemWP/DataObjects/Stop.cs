using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BliksemWP.DataObjects
{
    public class Stop
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int docid { get; set; }
        public int c0stopindex { get; set; }
        public string c1stopname { get; set; }
    }
}
