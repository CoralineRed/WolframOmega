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
        public string Username { get; }

        public CalculationInfo(string username, string input, string output, int id)
        {
            Input = input;
            Output = output;
            Id = id;
            Username = username;
        }
    }
}