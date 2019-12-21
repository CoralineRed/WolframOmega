using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public interface INeedResponse
    {
        string AskResponse { get; }
        string Response { get; set; }
    }
}
