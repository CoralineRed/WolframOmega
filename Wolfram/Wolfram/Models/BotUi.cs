using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WolframOmega
{
    public class BotUi
    {
        private TelegramBotClient bot;
        private ICommandExecutor executor;

        public BotUi(string token)
        {
            bot = new TelegramBotClient(token);
            var executor = new CommandExecutor();
            executor.Register(new Arithmetic());
            executor.Register(new CancelCommand());
            executor.Register(new HelpCommand(executor.GetAllCommands));
            executor.Register(new AvailableCalculations());
            executor.Register(new TakePermissionCommand());
            executor.Register(new GrantPermissionCommand());
            this.executor = executor;
        }

        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Chat.Id + " " + e.Message.Text);
            var output = executor.Execute(e);
            await bot.SendTextMessageAsync(e.Message.Chat, output);
        }

        public void Run()
        {
            bot.OnMessage += BotOnMessage;
            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}
