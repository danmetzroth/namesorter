using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NameSorter
{
    public class Program
    {
        static void Main(string[] args)
        {
            var nameSorter = new NameSorter(args);
            nameSorter.Run();
        }
    }
}
