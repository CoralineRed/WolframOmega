using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace WolframOmega
{
    public class Database
    {
        string connString { get; set; } = "Host=localhost;Username=postgres;Password=8841ytrewq;Database=wolfram";

        public void Update(MessageEventArgs update)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO users VALUES ({update.Message.Chat.Id}, '{update.Message.Chat.Username}');";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void TakePermission(int calcId, string userName, string yourName)
        {

        }

        public void GrantPermission(int calcId, string userName, string yourName, bool toTake)
        {
            long userid1 = 0;
            long receiverid = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($"SELECT userid FROM users WHERE (username = '{yourName}')", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        userid1 = (long)reader.GetValue(0);
                    }
                using (var cmd = new NpgsqlCommand($"SELECT userid FROM users WHERE (username = '{userName}')", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        receiverid = (long)reader.GetValue(0);
                    }
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    if (!toTake)
                        cmd.CommandText = $"INSERT INTO permissions VALUES ({userid1}, {receiverid}, {calcId});";
                    else
                        cmd.CommandText = $"DELETE FROM permissions WHERE (userid = {userid1} AND receiverid = {receiverid} AND calculationid = {calcId})";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<CalculationInfo> ShowAllCalculations(string userName)
        {
            object[] b = new object[9];
            var list = new List<CalculationInfo>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand($"select * from calcinfo inner join users on calcinfo.userid = users.userid where username = '{userName}'", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        reader.GetValues(b);
                        list.Add(new CalculationInfo((string)b[1], (string)b[2], (int)b[3]));
                        //list.Add(Tuple.Create((string)b[1], (string)b[2], (int)b[3]));
                    }
                using (var cmd = new NpgsqlCommand($"select * from (select * from permissions join users on permissions.receiverid = users.userid where username = '{userName}') as foo join calcinfo on foo.calculationid = calcinfo.calculationid", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        reader.GetValues(b);
                        list.Add(new CalculationInfo((string)b[6], (string)b[7], (int)b[2]));
                        //list.Add(Tuple.Create((string)b[6], (string)b[7], (int)b[2]));
                    }
            }
            return list;
        }

        public void AddQuery(string query, string output, long userId)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = $"INSERT INTO calcinfo VALUES ({userId}, '{query}', {output});";
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool Exists(long id)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT userid FROM users", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        if (id == (long)reader.GetValue(0))
                        {
                            return true;
                        }
            }
            return false;
        }
    }
}