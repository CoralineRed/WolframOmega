using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class TakePermissionCommand : IBotCommand
    {
        public string Command => "/takepermission";

        public string Reference => "отбирает право указанному пользователю просматривать указанное вычисление";

        public string Message => "Чтобы отобрать право, просто укажите ник пользователя и номер расчета в виде: ваш_ник, ник_друга, айди_вычисления";

        public string Calculate(string input)
        {
            var a = input.Split().Where(x => x != "").ToArray();
            if (a.Length != 3) return "Неверный ввод";
            else
            {
                try
                {
                    new Database().GrantPermission(int.Parse(a[2]), a[1], a[0], true);
                }
                catch
                {
                    return "Что-то пошло не так. Вероятно, вы уже отобрали доступ" +
                        " или такого пользователя не существует";
                }
                return "Права успешно отобраны";
            }
        }
    }
}