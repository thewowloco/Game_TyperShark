using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;

namespace Main
{
    public class GetTop : Form3
    {

        public static MySqlConnection conn = Main.ConnectDb.GetDBConnection();

        //Top 5 người có điểm cao nhất
        public static List<Player> getTop5Player()
        {

            List<Player> listPlayer = new List<Player>();

            try
            {
                conn.Open();
                string sql = "SELECT player, score FROM typershark.hall ORDER BY score desc LIMIT 5";
                //Top 10 thì sửa LIMIT 5 -> LIMIT 10


                // Tạo một đối tượng Command.
                MySqlCommand cmd = new MySqlCommand();

                // Liên hợp Command với Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Player p = new Player();
                            p.name = reader.GetString(0);
                            p.score = Convert.ToInt32(reader.GetValue(1));
                            listPlayer.Add(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Lỗi kết nối cơ sở dữ liệu!");
            }
            finally
            {
                // Đóng kết nối.
                conn.Close();
                // Tiêu hủy đối tượng, giải phóng tài nguyên.
                conn.Dispose();
            }
            return listPlayer;
        }

        //Lấy danh sách người chơi để điền vào phần Login
        public static List<Player> getPlayerNameList()
        {

            List<Player> listPlayer = new List<Player>();

            try
            {
                conn.Open();
                string sql = "SELECT DISTINCT player FROM typershark.hall ORDER BY player ASC";

                // Tạo một đối tượng Command.
                MySqlCommand cmd = new MySqlCommand();

                // Liên hợp Command với Connection.
                cmd.Connection = conn;
                cmd.CommandText = sql;

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            Player p = new Player();
                            p.name = reader.GetString(0);
                            listPlayer.Add(p);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Lỗi kết nối cơ sở dữ liệu!");
            }
            finally
            {
                // Đóng kết nối.
                conn.Close();
                // Tiêu hủy đối tượng, giải phóng tài nguyên.
                conn.Dispose();
            }
            return listPlayer;
        }

    }
}
