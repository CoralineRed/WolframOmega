using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class HelpCommand : IBotCommand
    {
        private List<IBotCommand> commands;

        public HelpCommand(List<IBotCommand> commands)
        {
            this.commands = commands;
        }

        public string Command => "/help";

        public string Reference => "посмотреть доступные команды";

        public string Message
        {
            get
            {
                var builder = new StringBuilder();
                builder.Append("Этот бот может считать арифметические выражения и сохранять их. ");
                builder.Append("Вы можете делиться своими вычислениями с другими пользователями. ");
                builder.Append("Доступные команды:\n\n");
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
}
