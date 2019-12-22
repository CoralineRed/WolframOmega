using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WolframOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            var token = File.ReadAllText("token.txt");
            new BotUi(token).Run();
        }
    }
}
