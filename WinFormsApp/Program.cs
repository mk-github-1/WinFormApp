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
            // Configurationの読み込み
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
           
            // AutofacのDIコンテナを準備
            IContainer container = ConfigureContainer(configuration);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // 最初に起動するFormはAutofacのDIコンテナを使用するように変更
            // Application.Run(new Form1());
            Application.Run(container.Resolve<Form1>());
        }

        // AutofacのDIコンテナの設定
        private static IContainer ConfigureContainer(IConfiguration configuration)
        {
            // ContainerBuilderを準備
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // NpgsqlConnectionを登録
            containerBuilder.Register(c =>
            {
                string connectionString = configuration.GetConnectionString("DefaultConnection");
                return new NpgsqlConnection(connectionString);
            }).As<NpgsqlConnection>().InstancePerLifetimeScope();
            
            // ここでDIコンテナに、Configurationのインタフェースと実装クラスを登録する
            containerBuilder.RegisterInstance(configuration).As<IConfiguration>();
            
            // ここでDIコンテナに、IUserServiceやIUserRepositoryのインタフェースと実装クラスを登録する
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();

            
            // ここでDIコンテナに、Formの実装クラスを登録する
            containerBuilder.RegisterType<Form1>();


            // DIコンテナを構築して返す
            return containerBuilder.Build();
        }
    }
}
