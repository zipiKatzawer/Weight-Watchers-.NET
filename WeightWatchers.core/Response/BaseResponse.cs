using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightWatchers.core.Response
{
    public class BaseResponse
    {
        public bool Succsed { get; set; }

        public string message { get; set; }
        public BaseResponse()
        {
            Succsed = true;
        }
    }
}
