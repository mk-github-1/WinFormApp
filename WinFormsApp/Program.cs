// �@���̃p�b�P�[�W���K�v
// Autofac
// Microsoft.Extensions.Configuration
using Autofac;
using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Configuration.Json;

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
            // �A�\�����̓ǂݍ���
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
           
            // �BAutofac��DI�R���e�i������
            IContainer container = ConfigureContainer(configuration);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            
            // �EAutofac��DI�R���e�i���g�p����悤�ɕύX
            // Application.Run(new Form1());
            Application.Run(container.Resolve<Form1>());
        }

        // �CAutofac��DI�R���e�i�̐ݒ�
        private static IContainer ConfigureContainer(IConfiguration configuration)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();

            // �C-1 ������IUserService��IUserRepository�̎����N���X(UserService, UserRepository)�Ƃ̊֘A�t�����s��
            containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            
            containerBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();


            // �C-2 IConfiguration��DI�Ŏg�p���邽�߂̓o�^
            containerBuilder.RegisterInstance(configuration).As<IConfiguration>();

            // �C-3 Form1���g��DI�R���e�i�ɓo�^����K�v������
            containerBuilder.RegisterType<Form1>();


            // �C-4 DI�R���e�i���\�z���ĕԂ�
            return containerBuilder.Build();
        }
    }
}