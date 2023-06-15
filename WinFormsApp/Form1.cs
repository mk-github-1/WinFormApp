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
        // 例題なのでベタ書きですが、ここはRepositoryに分けれると思います。
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Form1()
        {
            // 例題なのでベタ書きですが、ここはRepositoryに分けれると思います。
            // appsettings.jsonのデータベース接続接続文字列を取得
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            this._configuration = builder.Build();
            this._connectionString = this._configuration["ConnectionStrings:DefaultConnection"]
            
            // Formの初期化(移動しない)
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            // データ数が少ないので一旦Listに詰める形にしてます。
            IList<UserModel> userModels = new List<UserModel>();

            // 例外処理はDB操作、フォルダ、ファイル操作など、外部リソースを使用するときに使っているイメージです。
            try
            {
                // 例題なのでベタ書きですが、tryの中はRepositoryに分けれると思います。
                // public IList<UserModel> FindAll() { return リスト; } みたいなイメージです。
                
                // データベース接続(usingを使うと抜けるときにdisposeメソッドを持っていれば、dispose(破棄)をしてくれます。)
                using (SqlConnection sqlConnection = new SqlConnection(this._connectionString))
                {
                    // SQLコマンド実行
                    string queryString = "SELECT id, name, age FROM user";
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlCommand.Connection.Open();

                    // データ取得
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    // データをループ
                    while (sqlDataReader.Read())
                    {
                        // C#では空のコンストラクタの後、{}で値が設定できる(Javaではできないので、コンストラクタを作ってやってました)
                        UserModel userModel = new UserModel()
                        {
                            Id = sqlDataReader.IsDBNull(0) ? 0 : sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.IsDBNull(1) ? string.Empty : sqlDataReader.GetString(1),
                            Age = sqlDataReader.IsDBNull(2) ? null : sqlDataReader.GetInt32(2),
                        };

                        // Listに追加する
                        userModels.Add(userModel);
                    }

                    // sqlDataReaderのみclose
                    sqlDataReader.Close();
                }

                // usingが終わるとsqlConnectionがDispose(破棄)されるのでclose不要
            }
            catch (Exception ex)
            {
                // 適当な例外を取得してます。UpdateConcurrencyExceptionとかあります。
                Console.WriteLine(ex.ToString());
            }

            // ここでuserModelsをDataGridViewに追加する処理を書く
            
            
            // Listを使うとLINQで並び変えとか簡単にできる。
            // SQLでしていたことはほとんどできる(DBからデータ取得後にjoinとか、group byとかも)
            // この例はLINQのメソッド構文です。クエリ構文の使用は避けてください。(ネット記事にはクエリ構文の例も多いです)
            // IList<UserModel> userModels2 = userModels.OrderBy(e => e.Age);
            

        }
    }
}
