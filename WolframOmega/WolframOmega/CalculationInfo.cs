using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class CalculationInfo
    {
        public int Id { get; }
        public string Input { get; }
        public string Output { get; }
        public long UserId { get; }

        public CalculationInfo(string input, string output, int id, long userId)
        {
            Input = input;
            Output = output;
            Id = id;
            UserId = userId;
        }
    }
}