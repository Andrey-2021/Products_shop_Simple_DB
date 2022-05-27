using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfAppDB.Classes;
using WpfAppDB.Model;
using Serilog;

namespace WpfAppDB.ViewModel
{
    public class ShopWindowViewModel
    {

        /// <summary>
        /// Model для этого ViewModel
        /// </summary>
        IShopsModel model;

        /// <summary>
        /// Флаг. Показывает создаём лм мы новый магазин или редактируем старый
        /// </summary>
        bool isNewShop = false;

        public Shop CurrentShop {get; set;}

        Serilog.ILogger mySeriLog = Log.ForContext<ShopWindowViewModel>();


        /// <summary>
        /// Команда. Сохранить изменения в информации о магазине
        /// </summary>
        ICommand saveShopCommand;

        /// <summary>
        /// Свойство.Сохранить изменения в информации о магазине
        /// </summary>
        public ICommand SaveShopCommand
        {
            get
            {
                return saveShopCommand ?? (saveShopCommand = new RelayCommand(SaveCurrentShop, CheckIsPossibleSaveShop));
            }
        }



        // <summary>
        /// Команда. Закрыть окно
        /// </summary>
        ICommand closeWindowCommand;

        /// <summary>
        /// Свойство.Закрыть окно
        /// </summary>
        public ICommand CloseWindowCommand
        {
            get
            {
                return closeWindowCommand ?? (closeWindowCommand= new RelayCommand(CloseWindow, CheckIsPossibleCloseWindow));
            }
        }


        /// <summary>
        /// Конструктор по умолчанию - без параметров
        /// </summary>
        public ShopWindowViewModel(IShopsModel model)
        {
            this.model = model;
            this.CurrentShop = new Shop();
            isNewShop = true;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="shop"></param>
        public ShopWindowViewModel(IShopsModel model, Shop shop)
        {
            this.model = model;
            this.CurrentShop = new Shop {Id=shop.Id, Name = shop.Name, Address = shop.Address, Phone = shop.Phone, Email = shop.Email, Products = null };
            isNewShop = false;
        }


        /// <summary>
        /// Метод. Сохранить изменения в информации о магазине
        /// </summary>
        void SaveCurrentShop(Object parametr)
        {
            try
            {
                if (isNewShop)
                { //создаём новый магазин
                    var task=model.WriteNewShopAsync(CurrentShop);


                    task.ContinueWith(t =>
                    {
                        if (task.Status == System.Threading.Tasks.TaskStatus.Faulted) //Задача завершилась из-за необработанного исключения??
                        {
                            mySeriLog.Error(nameof(SaveCurrentShop) + ",создание нового магазина. Исключение: " + task.Exception.Message);

                            model.CheckConnectionAsync(); //поскольку возникла ошибка, перепроверяем доступносьб БД

                            MessageWindowViewModel viModel = new MessageWindowViewModel("При создании данных в БД возникла ошибка.\n" + task.Exception.Message);
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                        else
                        {
                            MessageWindowViewModel viModel = new MessageWindowViewModel($"Магазин {CurrentShop.Name} создан в БД.");
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                    });
                }
                else
                { //редактируем старый магазин
                    
                   var task= model.ChangeShopAsync(CurrentShop);

                    task.ContinueWith(t =>
                    {
                        if (task.Status == System.Threading.Tasks.TaskStatus.Faulted) //Задача завершилась из-за необработанного исключения??
                        {
                            mySeriLog.Error(nameof(SaveCurrentShop) + ",редактирование магазина. Исключение: " + task.Exception.Message);

                            model.CheckConnectionAsync(); //поскольку возникла ошибка, перепроверяем доступносьб БД

                            MessageWindowViewModel viModel = new MessageWindowViewModel("При редактировании данных из БД возникла ошибка.\n" + task.Exception.Message);
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                        else
                        {
                            MessageWindowViewModel viModel = new MessageWindowViewModel($"Магазин {CurrentShop.Name} отредактирован в БД.");
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                    });
                }

                
            }
            catch (Exception ex)
            {
                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }

            //Вызвать команду "Закрыть окно"
            CloseWindowCommand.Execute(null);
        }

        /// <summary>
        /// Метод проверяет возможно ли сохранить изменения в информации о магазине
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleSaveShop(object parametr)
        {
            //todo добавить проверку полей - что введены правильно и полные
            return  CurrentShop!= null && String.IsNullOrEmpty(CurrentShop.Error);
        }





        /// <summary>
        /// Метод. Закрыть окно без сохранения изменений
        /// </summary>
        void CloseWindow(Object parametr)
        {
            //MessageBox.Show("проверка - Shop Закрыть");
            WindowsController.GetInstance().CloseWindow(this);
        }

        /// <summary>
        /// Метод проверяет возможно ли закрыть окно
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleCloseWindow(object parametr)
        {
            return true; // SelectedProduct != null ? true : false;
        }
    }
}
