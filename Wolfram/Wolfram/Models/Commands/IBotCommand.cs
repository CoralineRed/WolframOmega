using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public interface IBotCommand
    {
        string Command { get; }
        string Reference { get; }
        string Responce();
    }
}
