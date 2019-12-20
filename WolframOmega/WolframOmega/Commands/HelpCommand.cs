using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega.Commands
{
    public class HelpCommand : IBotCommand
    {
        private Func<IBotCommand[]> getAllCommands;

        public HelpCommand(Func<IBotCommand[]> getAllCommands)
        {
            this.getAllCommands = getAllCommands;
        }

        public string Command => "/help";

        public string Reference => "Посмотреть доступные команды.";

        public string Execute(string input)
        {
            var commands = getAllCommands();
            var builder = new StringBuilder();
            foreach (var command in commands)
            {
                builder.Append(command.Command);
                builder.Append(" - ");
                builder.Append(command.Reference);
                builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}
