using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using WinFormsApp.Model;

namespace WinFormsApp.Repository
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly NpgsqlConnection _npgsqlConnection;

        public UserRepository(NpgsqlConnection npgsqlConnection)
        {
            this._npgsqlConnection = npgsqlConnection;
        }

        public IEnumerable<UserModel> FindAll()
        {
            List<UserModel> userModels = new List<UserModel>();

            string queryString = "SELECT * FROM m_user";
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

            string queryString = "SELECT * FROM m_user WHERE id = @Id";
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
        }

        public void Create(UserModel userModel)
        {
            string queryString = "INSERT INTO m_user (name, age) VALUES (@name, @age)";

            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            command.Parameters.AddWithValue("@name", userModel.Name);
            command.Parameters.Add("@Age", NpgsqlTypes.NpgsqlDbType.Integer).Value = (userModel.Age != null ? userModel.Age : DBNull.Value);

            OpenConnection();

            command.ExecuteNonQuery();
        }

        public void Update(UserModel userModel)
        {
            string queryString = "UPDATE m_user "
                + " SET name = @Name, age = @Age "
                + " WHERE id = @Id";

            NpgsqlCommand command = new NpgsqlCommand(queryString, this._npgsqlConnection);

            command.Parameters.AddWithValue("@Name", userModel.Name);
            command.Parameters.Add("@Age", NpgsqlTypes.NpgsqlDbType.Integer).Value = (userModel.Age != null ? userModel.Age : DBNull.Value);

            OpenConnection();

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            string queryString = "DELETE FROM m_user WHERE id = @Id";

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
