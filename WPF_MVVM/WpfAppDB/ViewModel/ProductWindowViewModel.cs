using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfAppDB.Classes;
using WpfAppDB.Model;
using Serilog;

namespace WpfAppDB.ViewModel
{
    public class ProductWindowViewModel
    {
        /// <summary>
        /// Model для этого ViewModel.
        /// Через model работаем с БД.
        /// </summary>
        IProductsModel model;

        /// <summary>
        /// Флаг. Показывает создаём лм мы новый продукт или редактируем старый
        /// </summary>
        bool isNewProduct = false;

        Serilog.ILogger mySeriLog = Log.ForContext<ProductWindowViewModel>();

        /// <summary>
        /// Текущий продукт, к которому привязано окно
        /// </summary>
        public Product CurrentProduct { get; set; }


        /// <summary>
        /// Команда добавления нового продукта в БД
        /// </summary>
        private ICommand saveProductCommand;
        
        /// <summary>
        /// Свойство добавления нового продукта в БД
        /// </summary>
        public ICommand SaveProductCommand
        {
            get
            {
                return saveProductCommand?? (saveProductCommand = new RelayCommand(SaveCurrentProduct, CheckIsPossibleSaveProduct));
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
                return closeWindowCommand ?? (closeWindowCommand = new RelayCommand(CloseWindow, CheckIsPossibleCloseWindow));
            }
        }


        /// <summary>
        /// Конструктор для создания нового продукта, привязанного к магазину shop
        /// </summary>
        public ProductWindowViewModel(IProductsModel model, Shop shop)
        {
            this.model = model;
            CurrentProduct = new Product() { ShopsId=shop.Id, Shops=shop  };
            isNewProduct = true;
        }

        /// <summary>
        /// Конструктор для редактирования выыбранного продукта
        /// </summary>
        /// <param name="product">Продукт, который хотим отредактировать</param>
        public ProductWindowViewModel(IProductsModel model, Product product)
        {
            this.model = model;

            CurrentProduct = new Product() 
            { Id = product.Id, Name = product.Name, Price = product.Price, 
                ShopsId = product.ShopsId, Shops = product.Shops };

            isNewProduct = false;
        }



        /// <summary>
        /// Метод. Сохранить изменения в информации о товаре
        /// </summary>
        void SaveCurrentProduct(Object parametr)
        {
           
            try
            {
                if (isNewProduct)
                { //создаём новый продукт

                    var task = model.WriteNewProductAsync(CurrentProduct);

                    task.ContinueWith(t =>
                    {
                        if (task.Status == System.Threading.Tasks.TaskStatus.Faulted) //Задача завершилась из-за необработанного исключения??
                        {
                            mySeriLog.Error(nameof(SaveCurrentProduct) + ",создание нового товара. Исключение: " + task.Exception.Message);

                            model.CheckConnectionAsync(); //поскольку возникла ошибка, перепроверяем доступносьб БД

                            MessageWindowViewModel viModel = new MessageWindowViewModel("При создании данных в БД возникла ошибка.\n" + task.Exception.Message);
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                        else
                        {
                            MessageWindowViewModel viModel = new MessageWindowViewModel($"Товар {CurrentProduct.Name} создан в БД.");
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                    });
                }
                else
                { //редактируем старый продукт
                    
                   var task=model.ChangeProductAsync(CurrentProduct);

                    task.ContinueWith(t =>
                    {
                        if (task.Status == System.Threading.Tasks.TaskStatus.Faulted) //Задача завершилась из-за необработанного исключения??
                        {
                            mySeriLog.Error(nameof(SaveCurrentProduct) + ",редактирование товара. Исключение: " + task.Exception.Message);

                            model.CheckConnectionAsync(); //поскольку возникла ошибка, перепроверяем доступносьб БД

                            MessageWindowViewModel viModel = new MessageWindowViewModel("При редактировании данных из БД возникла ошибка.\n" + task.Exception.Message);
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                        else
                        {
                            MessageWindowViewModel viModel = new MessageWindowViewModel($"Товар {CurrentProduct.Name} отредактирован в БД.");
                            WindowsController.GetInstance().ShowWindow(viModel);
                        }
                    });

                }

                //MessageWindowViewModel viModel = new MessageWindowViewModel($"Товар {CurrentProduct.Name} сохранён в БД.");
                //WindowsController.GetInstance().ShowWindow(viModel);
            }
            catch (Exception ex)
            {
                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }

            //вызываем команду  "закрыть окно"
            CloseWindowCommand.Execute(null);
        }

        /// <summary>
        /// Метод проверяет возможно ли сохранить изменения в информации о товаре
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleSaveProduct(object parametr)
        {
            //todo добавить проверку полей - что введены правильно и полные


            return CurrentProduct != null &&  String.IsNullOrEmpty(CurrentProduct.Error);
        }







        /// <summary>
        /// Метод. Закрыть окно без сохранения изменений
        /// </summary>
        void CloseWindow(Object parametr)
        {
            //MessageBox.Show("проверка - Закрыть");
            WindowsController.GetInstance().CloseWindow(this);
        }

        /// <summary>
        /// Метод проверяет возможно ли закрыть окно
        /// </summary>
        /// <returns>true - закрыть возможно. false - закрыть нельзя</returns>
        bool CheckIsPossibleCloseWindow(object parametr)
        {
            return true;
        }
    }
}
