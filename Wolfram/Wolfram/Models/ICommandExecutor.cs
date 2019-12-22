using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace WolframOmega
{
    public interface ICommandExecutor
    {
        string Execute(MessageEventArgs args);
    }
}
