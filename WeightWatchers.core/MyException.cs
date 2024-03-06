using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightWatchers.core
{
    public class MyException : Exception
    {
        public int Status { get; set; }
        public string NameTable { get; set; }
        public MyException(int status, string mametable, string message) : base(message)
        {
            Status = status;
            NameTable = mametable;

        }
    }
}
