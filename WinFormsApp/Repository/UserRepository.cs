<<<<<<< HEAD
﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using WinFormsApp.Model;
=======
using Npgsql;
using System.Collections.Generic;
>>>>>>> 75f3ae78de326aa8247277ea62c81b8e4c2c05a6

namespace WinFormsApp.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
<<<<<<< HEAD
        private readonly NpgsqlConnection _npgsqlConnection;

        public UserRepository(NpgsqlConnection npgsqlConnection)
        {
            this._npgsqlConnection = npgsqlConnection;
        }

        public IEnumerable<UserModel> FindAll()
        {
            List<UserModel> userModels = new List<UserModel>();

            string queryString = "SELECT * FROM Users";
            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            OpenConnection();

            using (NpgsqlDataReader npgsqlDataReader = command.ExecuteReader())
            {
                while (npgsqlDataReader.Read())
                {
                    UserModel userModel = new UserModel
                    {
                        Id = npgsqlDataReader.GetInt32(0),
                        Name = npgsqlDataReader.GetString(1),
                        Age = npgsqlDataReader.IsDBNull(2) ? null : (int?)npgsqlDataReader.GetInt32(2)
                    };

                    userModels.Add(userModel);
                }
            }

            return userModels;
        }

        public UserModel? FindById(int id)
        {
            UserModel? userModel = null;

            string queryString = "SELECT * FROM Users WHERE Id = @Id";
            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);
            command.Parameters.AddWithValue("@Id", id);

            OpenConnection();

            using (NpgsqlDataReader npgsqlDataReader = command.ExecuteReader())
            {
                if (npgsqlDataReader.Read())
                {
                    userModel = new UserModel
                    {
                        Id = npgsqlDataReader.GetInt32(0),
                        Name = npgsqlDataReader.GetString(1),
                        Age = npgsqlDataReader.IsDBNull(2) ? null : (int?)npgsqlDataReader.GetInt32(2)
                    };
                }
            }

            return userModel;
=======
        private readonly NpgsqlConnection _connection;

        public UserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
>>>>>>> 75f3ae78de326aa8247277ea62c81b8e4c2c05a6
        }

        public void Create(UserModel userModel)
        {
            string queryString = "INSERT INTO Users (name, age) VALUES (@name, @age)";
<<<<<<< HEAD
            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            command.Parameters.AddWithValue("@name", userModel.Name);
            command.Parameters.Add("@Age", NpgsqlTypes.NpgsqlDbType.Integer).Value = (userModel.Age != null ? userModel.Age : DBNull.Value);

            OpenConnection();

            command.ExecuteNonQuery();
=======
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@name", userModel.Name);
                command.Parameters.AddWithValue("@age", userModel.Age);

                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
>>>>>>> 75f3ae78de326aa8247277ea62c81b8e4c2c05a6
        }

        public void Update(UserModel userModel)
        {
            string queryString = "UPDATE Users "
                + " SET name = @Name, age = @Age "
                + " WHERE Id = @Id";
<<<<<<< HEAD

            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            command.Parameters.AddWithValue("@Name", userModel.Name);
            command.Parameters.Add("@Age", NpgsqlTypes.NpgsqlDbType.Integer).Value = (userModel.Age != null ? userModel.Age : DBNull.Value);

            OpenConnection();

            command.ExecuteNonQuery();
=======
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@Age", userModel.Age);

                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
>>>>>>> 75f3ae78de326aa8247277ea62c81b8e4c2c05a6
        }

        public void Delete(int id)
        {
            string queryString = "DELETE FROM Users WHERE Id = @Id";
<<<<<<< HEAD
            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            command.Parameters.AddWithValue("@Id", id);

            OpenConnection();

            command.ExecuteNonQuery();
        }

        private void OpenConnection()
        {
            if (this._npgsqlConnection.State != ConnectionState.Open)
            {
                this._npgsqlConnection.Open();
=======
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public List<UserModel> FindAll()
        {
            List<UserModel> users = new List<UserModel>();
            string queryString = "SELECT * FROM Users";
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                OpenConnection();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserModel user = new UserModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Age = reader.GetInt32(2)
                        };
                        users.Add(user);
                    }
                }
                CloseConnection();
            }

            return users;
        }

        public UserModel FindById(int id)
        {
            string queryString = "SELECT * FROM Users WHERE Id = @Id";
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                UserModel user = null;

                OpenConnection();
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserModel
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Age = reader.GetInt32(2)
                        };
                    }
                }
                CloseConnection();

                return user;
            }
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
>>>>>>> 75f3ae78de326aa8247277ea62c81b8e4c2c05a6
            }
        }

        public void Dispose()
        {
            if (this._npgsqlConnection.State != ConnectionState.Closed)
            {
                this._npgsqlConnection.Close();
            }
            this._npgsqlConnection.Dispose();
        }
    }
}
