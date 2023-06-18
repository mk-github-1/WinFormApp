using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WinFormsApp.Model;

namespace WinFormsApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            this._connectionString = configuration["ConnectionStrings:DefaultConnection"];
        }

        public IEnumerable<UserModel> FindAll()
        {
            List<UserModel> userModels = new List<UserModel>();

            // データベース接続(postgresqlを使用するように修正が必要)
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                // SQLコマンド実行し、データ取得
                string queryString = "SELECT * FROM user";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                // データをループ
                while (sqlDataReader.Read() == true)
                {
                    // C#では空のコンストラクタで値が設定できる(Javaではできなかったはず)
                    UserModel userModel = new UserModel()
                    {
                        Id = sqlDataReader.IsDBNull(0) ? 0 : sqlDataReader.GetInt32(0),
                        Name = sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(1),
                        Age = sqlDataReader.IsDBNull(0) ? null : sqlDataReader.GetInt32(2),
                    };

                    // Listに追加する
                    userModels.Add(userModel);
                }

                // sqlDataReaderのみclose
                sqlDataReader.Close();
            }

            // usingが終わるとsqlConnectionがDispose(破棄)されるのでclose不要

            return userModels;
        }

        public UserModel? FindById(int id)
        {
            // データベース接続
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                // SQLコマンド実行し、データ取得
                string queryString = "SELECT * FROM Users WHERE Id = @Id";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Id", id);
                sqlConnection.Open();

                using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
                {
                    if (sqlDataReader.Read())
                    {
                        UserModel userModel = new UserModel
                        {
                            Id = sqlDataReader.IsDBNull(0) ? 0 : sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(1),
                            Age = sqlDataReader.IsDBNull(0) ? null : sqlDataReader.GetInt32(2),
                        };

                        // sqlDataReaderのみclose
                        sqlDataReader.Close();

                        return userModel;
                    }
                }
            }

            return null;
        }

        public void Create(UserModel userModel)
        {
            // データベース接続
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                string queryString = "INSERT INTO Users (name, age) VALUES (@name, @age)";
                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                
                sqlCommand.Parameters.AddWithValue("@name", userModel.Name);
                sqlCommand.Parameters.AddWithValue("@age", userModel.Age);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void Update(UserModel userModel)
        {
            // データベース接続
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                string queryString = "UPDATE Users "
                    + " SET name = @Name "
                    + " age = @Age "
                    + " WHERE Id = @Id";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                
                command.Parameters.AddWithValue("@Id", userModel.Id);
                command.Parameters.AddWithValue("@Name", userModel.Name);
                command.Parameters.AddWithValue("@Age", userModel.Age);

                sqlConnection.Open();
                command.ExecuteNonQuery();

            }
        }

        public void Delete(int id)
        {
            // データベース接続
            using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                string queryString = "DELETE FROM Users WHERE Id = @Id";

                SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);

                sqlCommand.Parameters.AddWithValue("@Id", id);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

            }
        }
    }
}
