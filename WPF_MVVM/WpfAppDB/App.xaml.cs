using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfAppDB.Classes;
using WpfAppDB.Model;
using WpfAppDB.View;
using WpfAppDB.ViewModel;

namespace WpfAppDB
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Model через которую осуществляется работа с БД
        /// </summary>
        DbModel model;

        /// <summary>
        /// Конструктор
        /// </summary>
        public App()
        {
            CreateLogger();

            //Регистрируем Пары Типов View и ViewModel в контроллере 
            WindowsController.GetInstance().RegistryWindowType<MainWindowViewModel, MainWindowView>();
            WindowsController.GetInstance().RegistryWindowType<ProductWindowViewModel, ProductWindowView>();
            WindowsController.GetInstance().RegistryWindowType<ShopWindowViewModel,ShopWindowView>();
            WindowsController.GetInstance().RegistryWindowType<MessageWindowViewModel, MessageWindowView>();
        }

        
        protected override void OnStartup(System.Windows.StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                Log.Information("Запуск программы. Метод OnStartup(), файл App.xaml.cs");

                model = new DbModel(); //создали модель
                
                //используем Dependency injection через конструктор:
                var mainViewModel = new MainWindowViewModel(model); //создаём ViewModel главного окна

                WindowsController.GetInstance().ShowWindow(mainViewModel);  //показать окно, соответствующее mainViewModel
                
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Неожиданная ошибка в программе. Метод OnSturtup()");

                MessageWindowViewModel model = new MessageWindowViewModel("Ошибка выполнения программы: "+ex.Message);
                WindowsController.GetInstance().ShowWindow(model);

            }
            finally
            {
                model?.Dispose();
                Log.Information("Программа закрыта");
                Log.CloseAndFlush(); //все выключить.
                Shutdown(); //закрыть программу
            }
        }

        /// <summary>
        /// Создаём и Конфигурируем Serilog
        /// </summary>
        void CreateLogger()
        {

            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .Enrich.WithThreadId()   //Будем Добавлять в события Serilog Id потока.
                    .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day, fileSizeLimitBytes:200_000_000,
                    outputTemplate:"{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties}{NewLine}{Exception}")
                    .CreateLogger();
        }
    }
}
