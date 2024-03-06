using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightWatchers.core
{
    public class Settings
    {
        public string FileSave { get; set; }
        public int AdminIstratorId { get; set; }
        public Jwt Jwt { get; set; }
    }
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
}
