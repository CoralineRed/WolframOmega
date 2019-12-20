using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class CalculationInfo
    {
        public int Id { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }
        public CalculationInfo(string input, string output, int id)
        {
            Input = input;
            Output = output;
            Id = id;
        }
    }
}