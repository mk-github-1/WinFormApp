using WinFormsApp.Model;
using WinFormsApp.Services;

namespace WinFormsApp
{
    // FormはSpringBootでいうとController、画面、javascriptがくっついているような役割
    public partial class Form1 : Form
    {
        // DIを使用するためには
        // privateフィールド、コンストラクタ引数、コンストラクタ内での初期化は必須です。
        private readonly IUserService _userService;

        // SpringBootのコンストラクタに@Autowiredをつけて使用するのと同じです。
        public Form1(IUserService userService)
        {
            this._userService = userService;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            try
            {
                // Service -> Repositoryからデータ取得
                IList<UserModel> userModels = this._userService.FindAll().ToList();

                // ここでuserModelsをDataGridViewに追加する処理を書く

                // デバッグ用確認
                foreach (UserModel userModel in userModels)
                {
                    Console.WriteLine(userModel.Id);
                    Console.WriteLine(userModel.Name);
                    Console.WriteLine(userModel.Age);
                }

                Console.WriteLine("ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}