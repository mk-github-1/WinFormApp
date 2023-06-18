// �@このパッケージが必要
// Autofac
// Microsoft.Extensions.Configuration
using Autofac;
using Microsoft.Extensions.Configuration;

using WinFormsApp.Repository;
using WinFormsApp.Services;

namespace WinFormsApp
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // �A構成情報の読み込み
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
           
            // �BAutofacのDIコンテナを準備
            IContainer container = ConfigureContainer(configuration);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // �EAutofacのDIコンテナを使用するように変更
            // Application.Run(new Form1());
            Application.Run(container.Resolve<Form1>());
        }

        // �CAutofacのDIコンテナの設定
        private static IContainer ConfigureContainer(IConfiguration configuration)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // �C-1 ここでIUserServiceやIUserRepositoryの実装クラス(UserService, UserRepository)との関連付けを行う
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();


            // �C-2 IConfigurationをDIで使用するための登録
            containerBuilder.RegisterInstance(configuration).As<IConfiguration>();

            // �C-3 Form1自身もDIコンテナに登録する必要がある
            containerBuilder.RegisterType<Form1>();


            // �C-4 DIコンテナを構築して返す
            return containerBuilder.Build();
        }
    }
}