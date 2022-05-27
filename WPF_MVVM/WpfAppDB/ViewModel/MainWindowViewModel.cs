using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using WpfAppDB.Classes;
using WpfAppDB.Model;

namespace WpfAppDB.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        IDbModel _dbModel;

        Serilog.ILogger mySeriLog = Log.ForContext<MainWindowViewModel>();


        /// <summary>
        /// Показывает доступна ли БД
        /// </summary>
        public bool isDbAvalaible;

        /// <summary>
        /// Свойство. Показывает доступна ли БД
        /// </summary>
        public bool IsDbAvalaible
        {
            get { return _dbModel.IsDbAvailable; }
            private set
            {
                isDbAvalaible = value;
                OnPropertyChanged();
            }
        }





        /// <summary>
        /// Выбранный магазин
        /// </summary>
        Shop selectedShop;

        /// <summary>
        /// СВойство.Выбранный магазин
        /// </summary>
        public Shop SelectedShop
        {
            get { return selectedShop; }
            set
            {
                selectedShop = value;

                if (selectedShop != null)
                {
                    if (AllProducts != null)
                        Products = new ObservableCollection<Product>(AllProducts.Where(x => x.ShopsId == selectedShop.Id));
                    else Products = new ObservableCollection<Product>();
                }

                OnPropertyChanged("SelectedShop");
                OnPropertyChanged("Products");
            }
        }


        /// <summary>
        /// Выбранный продукт
        /// </summary>
        Product selectedProduct;

        /// <summary>
        /// Свойство. Выбранный продукт
        /// </summary>
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Shop> shops;
        /// <summary>
        /// Коллекция ВСЕХ магазинов из БД
        /// </summary>
        public ObservableCollection<Shop> Shops
        {
            get
            {
                return shops;
            }
            set
            {
                shops = value;
                OnPropertyChanged();
            }
        }



        /// <summary>
        /// Коллекция продукты для текущего/выбранного магазина
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        /// <summary>
        /// Коллекция ВСЕХ продуктов, хранящихся в БД
        /// </summary>
        public ObservableCollection<Product> AllProducts { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }




        /// <summary>
        /// Команда. Изменить магазин
        /// </summary>
        ICommand changingShop;

        /// <summary>
        /// Свойство. Изменить магазин
        /// </summary>
        public ICommand ChangingShop
        {
            get
            {
                return changingShop ?? (changingShop = new RelayCommand(ChangeSelectedShop, CheckIsPossibleChangeShop));
            }
        }



        /// <summary>
        /// Команда. Изменить продукт
        /// </summary>
        ICommand changingProduct;

        /// <summary>
        /// Свойство.Изменить продукт
        /// </summary>
        public ICommand ChangingProduct
        {
            get
            {
                return changingProduct ?? (changingProduct = new RelayCommand(ChangeSelectedProduct, CheckIsPossibleChangeProduct));
            }
        }





        /// <summary>
        /// Команда. Добавить новый магазин
        /// </summary>
        ICommand addingShop;

        /// <summary>
        /// Свойство. Добавить новый магазин
        /// </summary>
        public ICommand AddingShop
        {
            get
            {
                return addingShop ?? (addingShop = new RelayCommand(AddNewShop, CheckIsPossibleAddNewShop));
            }
        }



        /// <summary>
        /// Команда. Добавить новый продукт
        /// </summary>
        ICommand addingProduct;

        /// <summary>
        /// Свойство.Добавить новый продукт
        /// </summary>
        public ICommand AddingProduct
        {
            get
            {
                return addingProduct ?? (addingProduct = new RelayCommand(AddNewProduct, CheckIsPossibleAddNewProduct));
            }
        }






        /// <summary>
        /// Команда. Удалить магазин
        /// </summary>
        ICommand deleitingShop;

        /// <summary>
        /// Свойство. Удалить магазин
        /// </summary>
        public ICommand DeleitingShop
        {
            get
            {
                return deleitingShop ?? (deleitingShop = new RelayCommand(DelSelectedShop, CheckIsPossibleDelShop));
            }
        }




        /// <summary>
        /// Команда. Удалить продукт
        /// </summary>
        ICommand deleitingProduct;

        /// <summary>
        /// Свойство.Удалить продукт
        /// </summary>
        public ICommand DeleitingProduct
        {
            get
            {
                return deleitingProduct ?? (deleitingProduct = new RelayCommand(DelSelectedProduct, CheckIsPossibleDelProduct));
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



        // <summary>
        /// Команда. Создать новое соединение с БД
        /// </summary>
        ICommand creatingNewConnectToDbCommand;

        /// <summary>
        /// Свойство.Подключиться к БД
        /// </summary>
        public ICommand CreatingNewConnectToDbCommand
        {
            get
            {
                return creatingNewConnectToDbCommand ?? (creatingNewConnectToDbCommand = new RelayCommand(CreateNewConnectionToDb, CheckIsPossibleCreateNewConnectionToDBWindow));
            }
        }







        /// <summary>
        /// Конструктор
        /// </summary>
        public MainWindowViewModel(IDbModel db)
        {
            this._dbModel = db;

            _dbModel.PropertyChanged += PropertyInModelChanged;
            _dbModel.ProductsDbTableChanged += TableProductsInDbChanged;
            _dbModel.ShopsDbTableChanged += TableShopsInDbChanged;

            GetDataFromDB();
        }

        /// <summary>
        /// Получить данные из БД
        /// </summary>
        void GetDataFromDB()
        {
            //_dbModel.CreateNewConnection();
            //LoadDataFromModel();
            CreateNewConnectionToDb(null);
        }










        /// <summary>
        /// Обработка изменения свойства в Model
        /// </summary>
        /// <param name="sender">Класс, в котором произошло изменение свойства</param>
        /// <param name="e">Класс, представляющий информацию о свойстве, которое изменилось</param>
        void PropertyInModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            
            //если это свойство IsDbAvailable, т.е. измениласть "доступность" БД - она стала или доступна или недоступна
            if (e.PropertyName ==nameof(_dbModel.IsDbAvailable))
            {
                //меняем значение местного свойства
                IsDbAvalaible = _dbModel.IsDbAvailable;
            }

        }




        /// <summary>
        /// Временный буфер для экземпляра класса Shop
        /// </summary>
        Shop bufSelectedShop;
        /// <summary>
        /// Временный буфер для экземпляра класса Product
        /// </summary>
        Product bufSelectedProduct;


        /// <summary>
        /// Сохрангить текущие значения  SelectedShop и SelectedProduct в буфере
        /// </summary>
        void SaveSelectedShopAndSelectedProduct()
        {
            bufSelectedShop = SelectedShop;
            bufSelectedProduct = SelectedProduct;
        }


        /// <summary>
        /// Если возможно (если не были удалены), в обновлённых данных, прочитанных из БД,
        /// установить предыдущие сохранённые значения SelectedShop и SelectedProduct из буфера
        /// </summary>
        void RestoreSelectedShopAndSelectedProduct()
        {
            if (bufSelectedShop != null)
            {
                //восстановили выбранный магазин
                var newSelectedShop = Shops.FirstOrDefault(x => x.Id == bufSelectedShop.Id);
                if (newSelectedShop != null) SelectedShop = newSelectedShop;

                //воостановлдение выбранного продукта
                if (bufSelectedProduct != null) //если есть выбранный ранее продукт
                {
                    //ищем, есть ли он в обновлённом списке продуктов для этого магазина
                    var newSelectedProduct = Products.FirstOrDefault(x => x.Id == bufSelectedProduct.Id);

                    if (newSelectedShop != null)
                    {//если есть
                        SelectedShop = newSelectedShop; //выбираем этот продукт
                    }
                    else //если нет
                    {
                        //есть ли продукты у этого магазина?
                        if (Products.Count > 0)
                            SelectedProduct = Products[0]; //выбираем первый продукт из списка у этого магазина
                    }
                }
            }
        }


        /// <summary>
        /// Метод для обработки события _dbModel.ShopsDbTableChanged (Изменение таблицы Shops в БД) в Model
        /// </summary>
        void TableShopsInDbChanged(object? sender, EventArgs e)
        {
            SaveSelectedShopAndSelectedProduct();
            LoadDataFromModel();
            RestoreSelectedShopAndSelectedProduct();
        }



        /// <summary>
        /// Метод для обработки события _dbModel.ProductsDbTableChanged (Изменение таблицы Products в БД) в Model
        /// </summary>
        void TableProductsInDbChanged(object? sender, EventArgs e)
        {
            SaveSelectedShopAndSelectedProduct();
            LoadDataFromModel();
            RestoreSelectedShopAndSelectedProduct();
        }





        /// <summary>
        /// Загрузка данных (Shops И AllProducts) из Model(БД)
        /// </summary>
        /// <returns>true - данные загружены успешно. false - произошла ошибка</returns>
        private void LoadDataFromModel()
        {
            try
            {

                List<Shop> shops;
                List<Product> products;

               //Debug.WriteLine("main1: " + Thread.CurrentThread.ManagedThreadId);
                var rezult = _dbModel.GetShopsAndProductsAsync();
              //  Debug.WriteLine("main2: " + Thread.CurrentThread.ManagedThreadId);


                rezult.ContinueWith(t =>
                {
                    //Задача завершилась из-за необработанного исключения??
                    if (rezult.Status == System.Threading.Tasks.TaskStatus.Faulted) 
                    {
                        mySeriLog.Error(nameof(LoadDataFromModel) + ", Возврат с исключением: " + rezult.Exception.Message);
                        //  Debug.WriteLine("main2: " + Thread.CurrentThread.ManagedThreadId);


                        Shops = new ObservableCollection<Shop>();
                        AllProducts = new ObservableCollection<Product>();

                        MessageWindowViewModel model = new MessageWindowViewModel("При чтении данных из БД возникла ошибка.\n"
                            + rezult.Exception.Message);
                        WindowsController.GetInstance().ShowWindow(model);
                    }
                    else
                    {
                      //  Debug.WriteLine("main2: " + Thread.CurrentThread.ManagedThreadId);

                        shops = rezult.Result.newShops;
                        products = rezult.Result.newProducts;

                        Shops = new ObservableCollection<Shop>(shops);
                        AllProducts = new ObservableCollection<Product>(products);

                        if (Shops.Count > 0) SelectedShop = Shops[0];
                    }

                });


               // Debug.WriteLine("main3: " + Thread.CurrentThread.ManagedThreadId);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(LoadDataFromModel) + ", Исключение: " + ex.Message);

                //Debug.WriteLine("main3: " + Thread.CurrentThread.ManagedThreadId);

                Shops = new ObservableCollection<Shop>();
                Products = new ObservableCollection<Product>();
                AllProducts = new ObservableCollection<Product>();
                SelectedShop = new Shop();
                selectedProduct = new Product();

                MessageWindowViewModel model = new MessageWindowViewModel("Ошибка: "+ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }
        }






        /// <summary>
        /// Изменить выбранный магазин
        /// </summary>
        void ChangeSelectedShop(Object parametr)
        {
            try
            {
                //используем Dependency injection через конструктор - передача _dbModel:
                var shopWindowViewModel = new ShopWindowViewModel(_dbModel, SelectedShop);

                WindowsController.GetInstance().ShowWindow(shopWindowViewModel);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(ChangeSelectedShop) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }

        }

        /// <summary>
        /// Метод проверяет возможно ли изменить выбранный магазин
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleChangeShop(object parametr)
        {
            return IsDbAvalaible && SelectedShop != null;
        }



        /// <summary>
        /// Изменить выбранный продукт
        /// </summary>
        void ChangeSelectedProduct(Object parametr)
        {
            try
            {
                //используем Dependency injection через конструктор - передача _dbModel:
                var productWindowViewModel = new ProductWindowViewModel(_dbModel, SelectedProduct);

                WindowsController.GetInstance().ShowWindow(productWindowViewModel);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(ChangeSelectedProduct) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }

        }

        /// <summary>
        /// Метод проверяет возможно ли изменить выбранный продукт
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleChangeProduct(object parametr)
        {
            //todo Везщде добавить проверку доступна ли БД
            return IsDbAvalaible && SelectedProduct != null;
        }






        /// <summary>
        /// Добавить новый магазин
        /// </summary>
        void AddNewShop(Object parametr)
        {
            try
            {
                //используем Dependency injection через конструктор - передача _dbModel:
                var shopWindowViewModel = new ShopWindowViewModel(_dbModel);

                WindowsController.GetInstance().ShowWindow(shopWindowViewModel);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(AddNewShop) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }
        }

        /// <summary>
        /// Метод проверяет возможно ли добавить новый магазин
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleAddNewShop(object parametr)
        {
            return IsDbAvalaible; // SelectedShop != null ? true : false;
        }




        /// <summary>
        /// Добавить новый продукт
        /// </summary>
        void AddNewProduct(Object parametr)
        {
            try
            {
                //используем Dependency injection через конструктор - передача _dbModel:
                var productWindowViewModel = new ProductWindowViewModel(_dbModel, SelectedShop);

                WindowsController.GetInstance().ShowWindow(productWindowViewModel);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(AddNewProduct) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }
        }

        /// <summary>
        /// Метод проверяет возможно ли добавить новый продукт
        /// </summary>
        /// <returns>true - добавить возможно. false - добавить нельзя</returns>
        bool CheckIsPossibleAddNewProduct(object parametr)
        {
            return IsDbAvalaible && SelectedShop != null;
        }






        /// <summary>
        /// Удалить выбранный магазин
        /// </summary>
        void DelSelectedShop(Object parametr)
        {
            //todo Добавить запрос подтверждения

            string shopName = SelectedShop.Name;
            try
            {
                var task = _dbModel.DelShopAsync(SelectedShop); ;

                task.ContinueWith(t =>
                {
                    if (task.Status == System.Threading.Tasks.TaskStatus.Faulted) //Задача завершилась из-за необработанного исключения??
                    {
                        MessageWindowViewModel model = new MessageWindowViewModel("При удалении данных из БД возникла ошибка.\n"+ task.Exception.Message);
                        WindowsController.GetInstance().ShowWindow(model);
                    }
                    else
                    {
                        MessageWindowViewModel model = new MessageWindowViewModel($"Магазин {shopName} удалён из БД.");
                        WindowsController.GetInstance().ShowWindow(model);
                    }
                });
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(DelSelectedShop) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }
        }

        /// <summary>
        /// Метод проверяет возможно ли удалить выбранный магазин
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleDelShop(object parametr)
        {
            return IsDbAvalaible && SelectedShop != null;
        }







        /// <summary>
        /// Удалить выбранный  продукт - SelectedProduct
        /// </summary>
        void DelSelectedProduct(Object parametr)
        {
            string productName = SelectedProduct.Name;
            try
            {
               // Debug.WriteLine("--DelSelectedProduct: " + Thread.CurrentThread.ManagedThreadId);

                var task = _dbModel.DelProductAsync(SelectedProduct);

                task.ContinueWith(t =>
                 {
                     //Debug.WriteLine("--DelSelectedProduct: " + Thread.CurrentThread.ManagedThreadId);

                     //Задача завершилась из-за необработанного исключения??
                     if (task.Status == System.Threading.Tasks.TaskStatus.Faulted)
                     {
                         MessageWindowViewModel model = new MessageWindowViewModel("При удалении данных из БД возникла ошибка.\n"
                             + task.Exception.Message);
                         WindowsController.GetInstance().ShowWindow(model);
                     }
                     else
                     {
                          // Debug.WriteLine("--DelSelectedProduct: " + Thread.CurrentThread.ManagedThreadId);

                         MessageWindowViewModel model = new MessageWindowViewModel($"Товар {productName} удалён из БД.");
                         WindowsController.GetInstance().ShowWindow(model);
                     }
                 });
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(DelSelectedProduct) + ", Исключение: " + ex.Message);

                MessageWindowViewModel model = new MessageWindowViewModel(ex.Message);
                WindowsController.GetInstance().ShowWindow(model);
            }
        }

        /// <summary>
        /// Метод проверяет возможно ли удалить продукт
        /// </summary>
        /// <returns>true - удалить возможно. false - удалить нельзя</returns>
        bool CheckIsPossibleDelProduct(object parametr)
        {
            return IsDbAvalaible && SelectedProduct != null;
        }




        /// <summary>
        /// Метод. Закрыть окно без сохранения изменений
        /// </summary>
        void CloseWindow(Object parametr)
        {
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





        /// <summary>
        /// Метод. Создание нового соединения с БД
        /// </summary>
        void CreateNewConnectionToDb(Object parametr)
        {
            if (_dbModel.CreateNewConnection())
            {
                LoadDataFromModel();
            }
            else
            {
                MessageWindowViewModel model = new MessageWindowViewModel($"БД недоступна.\n1)Проверьте настройки в файле конфигурации подключения к БД.\n2)Проверьте доступность сервера и БД.");
                WindowsController.GetInstance().ShowWindow(model);
            }
        }

        /// <summary>
        /// Метод проверяет возможно ли закрыть окно
        /// </summary>
        /// <returns>true - изменить возможно. false - изменить нельзя</returns>
        bool CheckIsPossibleCreateNewConnectionToDBWindow(object parametr)
        {
            return _dbModel != null;
        }
    }
}
