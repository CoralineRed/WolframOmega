using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class HelpCommand : IBotCommand
    {
        private Func<List<IBotCommand>> getCommands;

        public HelpCommand(Func<List<IBotCommand>> getCommands)
        {
            this.getCommands = getCommands;
        }

        public string Command => "/help";

        public string Reference => "посмотреть доступные команды";

        public string Responce()
        {
            var builder = new StringBuilder();
            builder.Append("Этот бот может считать арифметические выражения и сохранять их. ");
            builder.Append("Вы можете делиться своими вычислениями с другими пользователями. ");
            builder.Append("Доступные команды:\n\n");
            foreach (var command in getCommands())
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
