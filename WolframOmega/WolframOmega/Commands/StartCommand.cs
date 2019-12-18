using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class StartCommand : IBotCommand
    {
        public string Command => "/start";

        public string Reference => "Start bot.";

        public string Execute(string input)
        {
            return "Этот бот умеет";
        }
    }
}
