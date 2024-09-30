using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Settings
    {
        public required string CredentialsDir { get; set; }
        public required string OutputDirectory { get; set; }
        public required string SqlConnectionString { get; set; }
        public required string WebApp { get; set; }
    }
}
