using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class TakePermissionCommand : IBotCommand, IUseDB, INeedResponse
    {
        public string Command => "/takepermission";

        public string Reference => "отбирает право указанному пользователю просматривать указанное вычисление";

        public string AskResponse => "Чтобы отобрать право, просто укажите ник пользователя и номер расчета в виде: ник_друга айди_вычисления";

        public string Username { get; set; }

        public string Response { get; set; }

        public string Responce()
        {
            var a = Response.Split().Where(x => x != "").ToArray();
            if (a.Length != 2) return "Неверный ввод.";
            else
            {
                new Database().GrantPermission(int.Parse(a[1]), a[0], Username, true);
                return "Права успешно отобраны.";
            }
        }
    }
}