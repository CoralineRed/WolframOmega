using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class GrantPermissionCommand : IBotCommand, ICalculation
    {
        public string Command => "/grantpermission";

        public string Reference => "Дает право указанному пользователю просматривать указанное вычисление";

        public string Message => "Чтобы дать право, просто укажите ник пользователя и номер расчета в виде: ваш_ник, ник_друга, айди_вычисления";

        public string Calculate(string input)
        {
            var a = input.Split();
            if (a.Length != 3) return "Неверный ввод";
            else
            {
                try
                {
                    new Database().GrantPermission(int.Parse(a[2]), a[1], a[0]);
                }
                catch
                {
                    return "Что-то пошло не так. Вероятно, вы уже дали доступ этому пользователю" +
                        " или такого пользователя не существует";
                }
                return "Права успешно выданы";
            }
        }
    }
}