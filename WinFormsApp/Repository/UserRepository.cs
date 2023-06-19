using Npgsql;
using System.Collections.Generic;

namespace WinFormsApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection;

        public UserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public void Create(UserModel userModel)
        {
            string queryString = "INSERT INTO Users (name, age) VALUES (@name, @age)";
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@name", userModel.Name);
                command.Parameters.AddWithValue("@age", userModel.Age);

                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public void Update(UserModel userModel)
        {
            string queryString = "UPDATE Users "
                + " SET name = @Name, age = @Age "
                + " WHERE Id = @Id";
            using (NpgsqlCommand command = new NpgsqlCommand(queryString, _connection))
            {
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@Age", userModel.Age);

                OpenConnection();
                command.ExecuteNonQuery();
                CloseConnection();
            }
        }

        public void Delete(int id)
        {
            string queryString = "DELETE FROM Users WHERE Id = @Id";
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
            }
        }
    }
}
