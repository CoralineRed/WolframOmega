using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class CommandExecutor : ICommandExecutor
    {
        private readonly List<IBotCommand> commands = new List<IBotCommand>();
        private readonly Dictionary<string, string> references = new Dictionary<string, string>();
        private readonly Dictionary<string, Func<string, string>> executions
            = new Dictionary<string, Func<string, string>>();

        public string Execute(string message)
        {
            if (message.Length == 0) throw new ArgumentException("пустая строка");
            var command = message.Split()[0];
            var input = "";
            var pos = message.Trim().IndexOf(' ');
            if (pos != message.Length) input = message.Substring(pos + 1);

            if (command.Length != 0 && references.ContainsKey(command))
                return executions[command](input);
            else return "Неизвестная команда.";
        }

        public List<string> GetAllCommandNames()
        {
            return references.Select(x => x.Key).ToList();
        }

        public void Register(IBotCommand command)
        {
            commands.Add(command);
            references[command.Command] = command.Reference;
            executions[command.Command] = command.Execute;
        }
    }
}
