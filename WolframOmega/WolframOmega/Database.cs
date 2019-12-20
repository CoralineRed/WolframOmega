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

        public List<Tuple<string, string, int>> ShowAllCalculations()
        {
            object[] b = new object[4];
            var list = new List<Tuple<string, string, int>>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM calcinfo", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        reader.GetValues(b);
                        list.Add(Tuple.Create((string)b[1], (string)b[2], (int)b[3]));
                    }                       
            }
            return list;
        }

        public void AddQuery(string query,string output, long userId)
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
