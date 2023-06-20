using WinFormsApp.Model;
using WinFormsApp.Services;

namespace WinFormsApp
{
    // Form��SpringBoot�ł�����Controller�A��ʁAjavascript���������Ă���悤�Ȗ���
    public partial class Form1 : Form
    {
        // DI���g�p���邽�߂ɂ�
        // private�t�B�[���h�A�R���X�g���N�^�����A�R���X�g���N�^���ł̏������͕K�{�ł��B
        private readonly IUserService _userService;

        // SpringBoot�̃R���X�g���N�^��@Autowired�����Ďg�p����̂Ɠ����ł��B
        public Form1(IUserService userService)
        {
            this._userService = userService;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            try
            {
                // Service -> Repository����f�[�^�擾
                IList<UserModel> userModels = this._userService.FindAll().ToList();

                // DataGrid�Ƀo�C���h
                dataGridView1.DataSource = userModels;

                // Console.WriteLine("ok");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
