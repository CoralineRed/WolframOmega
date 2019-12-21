using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WolframOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            var token = "866672360:AAHpvbtFgU4BK9EP7o_lsIfJTauLwhq3Pq8";
            new BotUi(token).Run();

            //var bot = new TelegramBotClient(token);
            //bot.OnMessage += (object sender, MessageEventArgs e) =>
            //{
            //    Console.WriteLine(e.Message.Text);
            //    bot.SendTextMessageAsync(e.Message.Chat.Id, "Привет");
            //};
            //bot.StartReceiving();
            //WriteAllUpdates(new TelegramBotClient(token));

            //var a = new Arithmetic();
            //var s = Console.ReadLine();
            //while (s != "end")
            //{
            //    Console.WriteLine(a.Calculate(s));
            //    s = Console.ReadLine();
            //}

            //Console.ReadKey();
        }

        static async void WriteAllUpdates(TelegramBotClient bot)
        {
            var offset = 0;
            while (true)
            {
                try
                {
                    await bot.SetWebhookAsync("");
                    var updates = await bot.GetUpdatesAsync(offset);
                    foreach (var update in updates)
                    {
                        Console.WriteLine(update.Message);
                        offset = update.Id + 1;
                    }
                }
                catch
                {
                    Console.WriteLine("не удалось соединиться с сервером");
                }
            }
        }
    }
}
