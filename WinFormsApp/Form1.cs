using System.Data.SqlClient;

// ���̃p�b�P�[�W���K�v
// Microsoft.Extensions.Configuration
// Microsoft.Extensions.Configuration.Json
using Microsoft.Extensions.Configuration;
using WinFormsApp.Model;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // ���Ȃ̂Ńx�^�����ł����A������Repository�Ƃ�Dao�ɕ������Ǝv���܂��B
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public Form1()
        {
            // ���Ȃ̂Ńx�^�����ł����A������Repository�Ƃ�Dao�ɕ������Ǝv���܂��B

            // appsettings.json�̃f�[�^�x�[�X�ڑ��ڑ���������擾
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

            // �f�[�^�������Ȃ��̂ň�UList�ɋl�߂�`�ɂ��Ă܂��B
            // ���Ȃ̂Ńx�^�����ł����Atry�̒���Repository�Ƃ�Dao�ɕ������Ǝv���܂��B
            try
            {
                // �f�[�^�x�[�X�ڑ�
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    // SQL�R�}���h���s
                    string queryString = "SELECT * FROM user";
                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlCommand.Connection.Open();

                    // �f�[�^�擾
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    // �f�[�^�����[�v
                    while (sqlDataReader.Read() == true)
                    {
                        // C#�ł͋�̃R���X�g���N�^�Œl���ݒ�ł���(Java�ł͂ł��Ȃ������͂�)
                        UserModel userModel = new UserModel()
                        {
                            Id = sqlDataReader.IsDBNull(0) ? 0 : sqlDataReader.GetInt32(0),
                            Name = sqlDataReader.IsDBNull(0) ? string.Empty : sqlDataReader.GetString(1),
                            Age = sqlDataReader.IsDBNull(0) ? null : sqlDataReader.GetInt32(2),
                        };

                        // List�ɒǉ�����
                        userModels.Add(userModel);
                    }

                    // sqlDataReader�̂�close
                    sqlDataReader.Close();
                }

                // using���I����sqlConnection��Dispose(�j��)�����̂�close�s�v�Ǝv����
            }
            catch
            (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // ������userModels��DataGridView�ɒǉ����鏈��������



        }
    }
}