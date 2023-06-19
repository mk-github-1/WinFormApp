// このパッケージが必要
// Autofac
// Microsoft.Extensions.Configuration
// Npgsql
using Autofac;
using Microsoft.Extensions.Configuration;
using Npgsql;

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
            // 構成情報の読み込み
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
           
            // AutofacのDIコンテナを準備
            IContainer container = ConfigureContainer(configuration);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // AutofacのDIコンテナを使用するように変更
            // Application.Run(new Form1());
            Application.Run(container.Resolve<Form1>());
        }

        // AutofacのDIコンテナの設定
        private static IContainer ConfigureContainer(IConfiguration configuration)
        {
            // ContainerBuilderを準備
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // NpgsqlConnectionをDIで使用するための登録
            string connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            containerBuilder.Register(c =>
            {
                return new NpgsqlConnection(connectionString);
            }).As<NpgsqlConnection>().InstancePerLifetimeScope();

            // ここでUserServiceやUserRepositorをDIに登録し、IUserService, IUserRepositoryとの関連付けを行う
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();


            // IConfigurationをDIで使用するための登録
            containerBuilder.RegisterInstance(configuration).As<IConfiguration>();

            // Form自身もDIコンテナに登録する必要がある(実装クラスのみ)
            containerBuilder.RegisterType<Form1>();


            // DIコンテナを構築して返す
            return containerBuilder.Build();
        }
    }
}
