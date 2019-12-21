using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class AvailableCalculations : IBotCommand, IUseDB
    {
        public string Username { get; set; }

        public string Command => "/availablecalcs";

        public string Reference => "предоставляет информацию по доступным вычислениям";

        public string Responce()
        {
            var calcs = new Database().ShowAllCalculations(Username)
                    .GroupBy(c => c.Username);
            var builder = new StringBuilder();
            foreach (var group in calcs)
            {
                if (group.Key == Username) builder.Append("Ваши вычисления:\n");
                else builder.Append("Вычисления, предоставленные пользователем " + group.Key + ":\n");
                foreach (var calc in group)
                {
                    builder.Append("ID вычисления: " + calc.Id + ", ");
                    builder.Append("вводные данные: " + calc.Input + ", ");
                    builder.Append("результат: " + calc.Output + ".\n");
                }
                builder.Append("\n\n");
            }
            return builder.ToString();
        }
    }
}
