using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class GrantPermissionCommand : IBotCommand, INeedResponse, IUseDB
    {
        public string Command => "/grantpermission";

        public string Reference => "Дает право указанному пользователю просматривать указанное вычисление";

        public string AskResponse => "Чтобы дать право, просто укажите ник пользователя и номер расчета в виде: ник_друга айди_вычисления";

        public string Response { get; set; }
        public string Username { get; set; }

        public string Responce()
        {
            var a = Response.Split().Where(x => x != "").ToArray();
            if (a.Length != 2) return "Неверный ввод";
            else
            {
                new Database().GrantPermission(int.Parse(a[1]), a[0], Username);
                return "Права успешно выданы";
            }
        }
    }
}