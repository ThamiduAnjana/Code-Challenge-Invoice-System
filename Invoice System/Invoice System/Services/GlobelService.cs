using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice_System.Services
{
    public class GlobelService
    {
        public readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data\\store.json");
        public readonly string defultFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    }
}
