using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace MyTable {
    class DbWorker {

        private static string dataSource = "./MyTableDB.db";

        public DbWorker() {
        }

        private SqliteConnection getConnection() {
            SqliteConnectionStringBuilder scs = new SqliteConnectionStringBuilder();
            scs.DataSource = dataSource;

            return new SqliteConnection(scs.ConnectionString);
        }

        //Метод создаёт таблицу rooms в БД, если её ещё не было
        public void creatRoomsTable() {
            string s = "CREATE TABLE IF NOT EXISTS rooms (" +
                                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "building TEXT NOT NULL," +
                                "floor INTEGER NOT NULL," +
                                "room TEXT NOT NULL," +
                                "roomArea REAL NOT NULL," +
                                "addressPlan TEXT NOT NULL," +
                                "addressCircuitPlan TEXT NOT NULL," +
                                "addressCircuitLine TEXT NOT NULL," +
                                "addressCircuitWater TEXT NOT NULL," +
                                "addressCircuitHeat TEXT NOT NULL," +
                                "roomVolume REAL NOT NULL," +
                                "ratioHeat REAL NOT NULL," +
                                "coordinatesPoints TEXT DEFAULT ''" +
                                ");";

            using (SqliteConnection connection = getConnection()) {
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }
        }

        public void deleteRoomsTable() {
            string s = "DROP TABLE IF EXISTS rooms;";

            using (SqliteConnection connection = getConnection()) {
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }

        }

        //Метод вставляет данные в таблицу rooms из одного объекта Room
        public void insertRoomsTable(Room r) {

            string s = "INSERT INTO rooms (" +
                "building, floor, room, roomArea, " +
                "addressPlan, addressCircuitPlan, addressCircuitLine, " +
                "addressCircuitWater, addressCircuitHeat, roomVolume, ratioHeat) VALUES" +
                $"('{r.building}', '{r.floor}', '{r.room}', '{r.roomArea}', " +
                $"'{r.addressPlan}', '{r.addressCircuitPlan}', '{r.addressCircuitLine}', " +
                $"'{r.addressCircuitWater}', '{r.addressCircuitHeat}', '{r.roomVolume}', '{r.ratioHeat}');";

            using (SqliteConnection connection = getConnection()) {
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = s;
                cmd.ExecuteNonQuery();
            }
        }

        //Метод вытаскивает dct данные из таблицы rooms из БД и возвращает заполненный объект List<Room>
        public List<Room> selectRoomsTable() {

            string s = "SELECT * FROM rooms;";

            using (SqliteConnection connection = getConnection()) {
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = s;

                List<Room> list = new List<Room>();
                using (SqliteDataReader reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {

                        Room r = new Room();
                        r.id = reader.GetInt32(0);
                        r.building = reader.GetString(1);
                        r.floor = reader.GetInt32(2);
                        r.room = reader.GetString(3);
                        r.roomArea = Convert.ToDouble(reader.GetValue(4));
                        r.addressPlan = reader.GetString(5);
                        r.addressCircuitPlan = reader.GetString(6);
                        r.addressCircuitLine = reader.GetString(7);
                        r.addressCircuitWater = reader.GetString(8);
                        r.addressCircuitHeat = reader.GetString(9);
                        r.roomVolume = Convert.ToDouble(reader.GetValue(10));
                        r.ratioHeat = Convert.ToDouble(reader.GetValue(11));
                        r.coordinatesPoints = reader.GetString(12);

                        list.Add(r);
                    }
                }

                return list;
            }
        }


    }
}
