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

        public BotUi(string token)
        {
            bot = new TelegramBotClient(token);
        }

        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Text);
            await bot.SendTextMessageAsync(e.Message.Chat, "Привет");
        }

        private static ICommandExecutor CreateExecutor()
        {
            var executor = new CommandExecutor();
            executor.Register(new Arithmetic());
            return executor;
        }

        public void Run()
        {
            var executor = CreateExecutor();
            SetCommands(executor);
            bot.OnMessage += BotOnMessage;
            bot.StartReceiving();
            Console.ReadKey();
        }

        private void SetCommands(ICommandExecutor executor)
        {
            bot.
        }
    }
}
