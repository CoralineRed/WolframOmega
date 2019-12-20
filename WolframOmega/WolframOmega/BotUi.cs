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
            executor.Register(new HelpCommand(executor.GetAllCommands()));
            this.executor = executor;
        }

        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine(e.Message.Chat.Id + " " + e.Message.Text);
            var output = executor.Execute(e);
            await bot.SendTextMessageAsync(e.Message.Chat, output);
            //if (e.Message.Text == "/start")
            //{
            //}
            //else
            //{
            //    if (!db.Exists(e.Message.Chat.Id)) db.Update(e);
            //    db.AddQuery(e.Message.Text, new Arithmetic().Execute(e.Message.Text), e.Message.Chat.Id);
            //    db.GrantPermission(3, "co_re", "Nesom1", true);
            //    var a = db.ShowAllCalculations();
            //    Console.WriteLine(e.Message.Text);
            //    await bot.SendTextMessageAsync(e.Message.Chat, "Привет");
            //}
            
        }

        public void Run()
        {
            //SetCommands(executor);
            bot.OnMessage += BotOnMessage;
            bot.StartReceiving();
           
            Console.ReadKey();
        }


        //private void SetCommands(ICommandExecutor executor)
        //{
        //    bot
        //}
    }
}
