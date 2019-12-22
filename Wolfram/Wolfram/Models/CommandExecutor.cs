using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace WolframOmega
{
    public class CommandExecutor : ICommandExecutor
    {
        private Database db = new Database();
        private readonly Dictionary<string, IBotCommand> commands = new Dictionary<string, IBotCommand>();
        private readonly Dictionary<long, IBotCommand> currentAction = new Dictionary<long, IBotCommand>();

        public string Execute(MessageEventArgs args)
        {
            var text = args.Message.Text;
            var id = args.Message.Chat.Id;
            if (text == "/start" && !db.Exists(args.Message.Chat.Id))
            {
                currentAction[id] = null;
                db.Update(args);
            }
            else if (currentAction.ContainsKey(id) && currentAction[id] != null)
            {
                if (text == "/cancel")
                {
                    currentAction[id] = null;
                    return commands["/cancel"].Responce();
                }
                else
                {
                    try
                    {
                        ((INeedResponse)currentAction[id]).Response = args.Message.Text;
                        if (currentAction[id] is IUseDB useDB)
                            useDB.Username = args.Message.Chat.Username;
                        var output = currentAction[id].Responce();
                        if (currentAction[id] is ICalculation)
                            db.AddQuery(text, output, args.Message.Chat.Id);
                        return output;
                    }
                    catch (Exception e)
                    {
                        return e.Message;
                    }
                }
            }
            else if (commands.ContainsKey(text))
            {
                if (commands[text] is INeedResponse needResponse)
                {
                    currentAction[id] = commands[text];
                    return needResponse.AskResponse;
                }
                else
                {
                    currentAction[id] = null;
                    if (commands[text] is IUseDB useDB)
                        useDB.Username = args.Message.Chat.Username;
                    return commands[text].Responce();
                }
            }
            return commands["/help"].Responce();
        }

        public List<IBotCommand> GetAllCommands()
        {
            return commands.Select(x => x.Value).ToList();
        }

        public void Register(IBotCommand command)
        {
            commands.Add(command.Command, command);
        }
    }
}
