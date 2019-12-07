using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    interface ICalculation
    {
        string Name { get; }
        string Message { get; }
        string Calculate(string input);
    }
}
