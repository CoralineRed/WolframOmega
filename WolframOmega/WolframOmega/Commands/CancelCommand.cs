using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class CancelCommand : IBotCommand
    {
        public string Command => "/calculation";

        public string Reference => "отменяет текущую операцию";

        public string Responce()
        {
            return "Текущая операция была отменена.";
        }
    }
}
