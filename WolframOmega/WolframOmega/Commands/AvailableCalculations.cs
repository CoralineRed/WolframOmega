using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolframOmega
{
    public class AvailableCalculations : IBotCommand
    {
        private Database db;

        public AvailableCalculations(Database db)
        {
            this.db = db;
        }

        public string Command => "/availablecalcs";

        public string Reference => "предоставляет информацию по доступным вычислениям";

        private string username;
        public string Message
        {
            get
            {
                var calcs = db.ShowAllCalculations(username)
                    .GroupBy(c => c.UserId);

                return "";
            }
            set
            {
                username = value;
            }
        }
    }
}
