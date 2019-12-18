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

        public string Execute(string command)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllCommandNames()
        {
            throw new NotImplementedException();
        }

        public void Register(IBotCommand command)
        {
            commands.Add(command);
            references[command.Command] = command.Reference;
            executions[command.Command] = command.Execute;
        }
    }
}
