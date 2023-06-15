using System.Data.SqlClient;

// このパッケージが必要
// Microsoft.Extensions.Configuration
// Microsoft.Extensions.Configuration.Json
using Microsoft.Extensions.Configuration;
using WinFormsApp.Model;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // 例題なのでベタ書きですが、ここはRepositoryとかDaoに分けれると思います。
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public Form1()
        {
            // 例題なのでベタ書きですが、ここはRepositoryとかDaoに分けれると思います。

            // appsettings.jsonのデータベース接続接続文字列を取得
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            this._configuration = builder.Build();

            this.connectionString = this._configuration["ConnectionStrings:DefaultConnection"];

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            IList<UserModel> userModels = new List<UserModel>();

            // データ数が少ないので一旦Listに詰める形にしてます。
            // 例題なのでベタ書きですが、tryの中はRepositoryとかDaoに分けれると思います。
            try
            {
                // データベース接続
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    // SQLコマンド実行
                    string queryString = "SELECT * FROM user";
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlCommand.Connection.Open();

                    // データ取得
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

                // usingが終わるとsqlConnectionがDispose(破棄)されるのでclose不要と思われる
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // ここでuserModelsをDataGridViewに追加する処理を書く



        }
    }
}