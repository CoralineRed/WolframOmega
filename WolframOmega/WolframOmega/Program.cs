using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace WolframOmega
{
    public class Program
    {
        static void Main(string[] args)
        {
            var token = File.ReadAllLines("token.txt")[0];
            var bot = new TelegramBotClient(token);
            WriteAllUpdates(bot);
            Console.ReadKey();
        }

        static async void WriteAllUpdates(TelegramBotClient bot)
        {
            await bot.SetWebhookAsync("");
            var offset = 0;
            while (true)
            {
                try
                {
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
