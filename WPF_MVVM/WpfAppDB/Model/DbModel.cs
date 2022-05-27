using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WpfAppDB.Classes;

namespace WpfAppDB.Model
{

    /// <summary>
    /// Model. Работа с БД
    /// </summary>
    public class DbModel : IDbModel, INotifyPropertyChanged, IDisposable
    {
        private StreamWriter logStream;// = new StreamWriter("mylog.txt", true);


        public event EventHandler ShopsDbTableChanged;
        public event EventHandler ProductsDbTableChanged;



        /// <summary>
        /// Флаг, показывает доступна ли БД
        /// </summary>
        bool isDbAvailable = false;

        /// <summary>
        /// Свойство. Показывает доступна ли БД
        /// </summary>
        public bool IsDbAvailable
        {
            get
            {
                return isDbAvailable;
            }
            private set
            {
                isDbAvailable = value;
                OnPropertyChanged();
            }
        }




        /// <summary>
        /// Настройки подключения к MSSQL-сервреру
        /// </summary>
        DbContextOptions<DBMagazineContext> options;

        /// <summary>
        /// Свойство. Настройки подключения к MSSQL-сервреру
        /// </summary>
        private DbContextOptions<DBMagazineContext> Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;
                OnPropertyChanged();
            }
        }


        Serilog.ILogger mySeriLog = Log.ForContext<DbModel>();
        

        /// <summary>
        /// Конструктор
        /// </summary>
        public DbModel()
        {
            mySeriLog.Information("Создание DbModel. Отработка конструктора.");
            CreateLogStream();
        }

        //реализация Интерфейса IDisposable.
        /// <summary>
        ///  Освобождение ресурсов. 
        ///  Осовобождение потока записи логов DBMagazineContext в файл 
        /// </summary>
        public void Dispose()
        {
            logStream.Dispose();
            mySeriLog.Information("Ресурс logStream освобождён.");
        }


        /// <summary>
        /// Настройка потока (текстового файла) для сохранения логов для Entity Framework
        /// </summary>
        void CreateLogStream()
        {
            Directory.CreateDirectory("DBLog"); //создаём папку
            string fileName = "DBLog/mylog_"              //имя файла
                + DateTime.Now.ToString("yyyy_MM._dd")
                + ".txt";
            
            logStream = new StreamWriter(fileName, true);
        }





        /// <summary>
        /// Создать новое соединение с БД
        /// </summary>
        /// <returns>true-соединение с БД успешно создано. false- соединение с БД не создано</returns>
        public bool CreateNewConnection()
        {
            mySeriLog.Information("Создание нового соединения с БД.");

            SetDBConnectionConfig();
            return CheckConnection();
        }


        /// <summary>
        /// Получаем строку подключения к БД из json-файла. Инициализируем настройки подключения к БД
        /// </summary>
        private void SetDBConnectionConfig()
        {
            try
            {
                var builder = new ConfigurationBuilder();
                // установка пути к текущему каталогу
                builder.SetBasePath(Directory.GetCurrentDirectory() + "/Model");
                // получаем конфигурацию из файла appsettings.json
                builder.AddJsonFile("appsettings.json");
                // создаем конфигурацию
                var config = builder.Build();
                // получаем строку подключения
                string connectionString = config.GetConnectionString("DefaultConnection");

                var optionsBuilder = new DbContextOptionsBuilder<DBMagazineContext>();

                //настройка логгирования работы EntityFramework, класс DBMagazineContext
                //Уровень логгирования - Information
                optionsBuilder.LogTo(logStream.WriteLine, LogLevel.Information);

                Options = optionsBuilder.UseSqlServer(connectionString).Options;

                mySeriLog.Information("connectionString="+ connectionString);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(ex, "Исключение в"+ nameof(SetDBConnectionConfig));

                //options = null;
                Options = new DbContextOptions<DBMagazineContext>();
            }
        }


        /// <summary>
        /// Проверить доступна ли БД. Асинхронный метод
        /// </summary>
        public async void CheckConnectionAsync()
        {
            await Task.Run(() => CheckConnection());
        }




        /// <summary>
        /// Проверка доступности (наличия соединения) БД
        /// </summary>
        /// <returns>true - БД доступна. false - БД недоступна</returns>
        public bool CheckConnection()
        {
            try
            {
                using (DBMagazineContext db = new DBMagazineContext(Options))
                {
                    IsDbAvailable = db.Database.CanConnect();
                }

                return IsDbAvailable;
            }
            catch (Exception) //если произошла какая-то ошибка
            {
                IsDbAvailable = false;
                return false;
            }
        }



        public async Task<(List<Shop> newShops, List<Product> newProducts)> GetShopsAndProductsAsync()
        {
           // Debug.WriteLine("Async1: " + Thread.CurrentThread.ManagedThreadId);

            List<Shop> shops = null;
            List<Product> products = null;

            try
            {
                var rezult = await Task<(List<Shop> shops, List<Product> products)>.Run(() => GetShopsAndProducts());

             //   Debug.WriteLine("Async2: " + Thread.CurrentThread.ManagedThreadId);

                shops = rezult.shops;
                products = rezult.products;

             //   Debug.WriteLine("Async3: " + Thread.CurrentThread.ManagedThreadId);

                return (shops, products);
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(GetShopsAndProductsAsync)+", Исключение: " + ex.Message);

                IsDbAvailable = false;
                throw new Exception(ex.Message);
            }
        }

        

        public (List<Shop> shops, List<Product> products) GetShopsAndProducts()
        {
            //Debug.WriteLine("sync1: " + Thread.CurrentThread.ManagedThreadId);

            if (!(CheckConnection())) throw new AccessViolationException("Список магазинов получить невозможно т.к. БД недоступна!");

            List<Shop> shops = null;
            List<Product> products = null;

            try
            {
                using (DBMagazineContext db = new DBMagazineContext(Options))
                {
                    shops = db.Shops.ToList();
                    products = db.Products.ToList(); // todo использовать AsNoTracking - для экономии времени и памяти?
                }
            }
            catch (Exception ex)
            {
                mySeriLog.Error(nameof(GetShopsAndProducts) + ", Исключение: " + ex.Message);

                throw new IOException("При чтении данных из БД возникла ошибка: " + ex.Message);
            }

            //Debug.WriteLine("sync2: " + Thread.CurrentThread.ManagedThreadId);

            return (shops, products);
        }


        /*
                public List<Shop> GetShops()
                {
                    if (!CheckConnection()) throw new AccessViolationException("Список магазинов получить невозможно т.к. БД недоступна!");

                    using (DBMagazineContext db = new DBMagazineContext(Options))
                    {
                        bool isAvalaible = db.Database.CanConnect();

                        return db.Shops.ToList();
                    }
                }


                public List<Product> GetProducts()
                {
                    if (!CheckConnection()) throw new AccessViolationException("Список продуктов получить невозможно т.к. БД недоступна!");

                    using (DBMagazineContext db = new DBMagazineContext(Options))
                    {
                        return db.Products.ToList();
                    }
                }

        */



        public async Task WriteNewShopAsync(Shop newShop)
        {
            await Task.Run(() => WriteNewShop(newShop));
        }

        public void WriteNewShop(Shop newShop)
        {
            if (!CheckConnection()) throw new AccessViolationException("Добавиь новый магазин в БД невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {
                db.Shops.Add(newShop);
                db.SaveChanges();
            }
            OnShopsDbTableChanged();
        }


        public async Task WriteNewProductAsync(Product newProduct)
        {
            await Task.Run(() => WriteNewProduct(newProduct));
        }

        public void WriteNewProduct(Product newProduct)
        {
            if (!CheckConnection()) throw new AccessViolationException("Добавиь новый продукт в БД невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {
                //todo возможно лучше создать транзакцию??? чтобы быть увереннным. что магазин не будет удалён?
                var finedShop = db.Shops.FirstOrDefault(x => x.Id == newProduct.ShopsId);

                if (finedShop == null)
                {
                    OnShopsDbTableChanged();
                    throw new AccessViolationException("Добавить новый товар невозможно, т.к. магазина уже нет в БД! \nДанные обновлены.");
                }
                else
                {
                    newProduct.Shops = finedShop;
                    newProduct.ShopsId = finedShop.Id;

                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }
            }

            OnProductsDbTableChanged();
        }








        public async Task ChangeShopAsync(Shop shop)
        {
            await Task.Run(() => ChangeShop(shop));
        }


        public void ChangeShop(Shop shop)
        {
            if (!CheckConnection()) throw new AccessViolationException("Изменить магазин невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {
                if (!db.Shops.Contains(shop))
                {
                    OnShopsDbTableChanged();
                    throw new AccessViolationException("Изменить магазин невозможно т.к. его уже нет в БД! \nДанные обновлены.");
                }

                db.Shops.Update(shop);
                db.SaveChanges();
            }
            OnShopsDbTableChanged();

        }









        public async Task ChangeProductAsync(Product product)
        {
            await Task.Run(() => ChangeProduct(product));
        }

        public void ChangeProduct(Product product)
        {
            if (!CheckConnection()) throw new AccessViolationException("Изменить продукт невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {
                //ищем в БД продукт с таким же Id, как и у отредактированного
                var finedProductById = db.Products.FirstOrDefault(x => x.Id == product.Id);

                if (finedProductById == null)
                {
                    OnProductsDbTableChanged();
                    throw new AccessViolationException("Изменить продукт невозможно т.к. его уже нет в БД! \nДанные обновлены.");
                }
                else
                {
                    finedProductById.Name = product.Name;
                    finedProductById.Price = product.Price;
                    finedProductById.ShopsId = product.ShopsId;

                    db.Products.Update(finedProductById);
                    db.SaveChanges();
                }
            }
            OnProductsDbTableChanged();
        }







        public async Task DelShopAsync(Shop shop)
        {
            await Task.Run(() => DelShop(shop));
        }


        public void DelShop(Shop shop)
        {
            if (!CheckConnection()) throw new AccessViolationException("Удалить магазин невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {
                if (!db.Shops.Contains(shop))
                {
                    OnShopsDbTableChanged();
                    throw new AccessViolationException("Удалить магазин невозможно т.к. его уже нет в БД! \nДанные обновлены.");
                }

                db.Shops.Remove(shop);
                db.SaveChanges();
            }

            OnShopsDbTableChanged();
        }


        public async Task DelProductAsync(Product product)
        {
            //Debug.WriteLine("DelProductAsync: " + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() => DelProduct(product));
            //Debug.WriteLine("DelProductAsync: " + Thread.CurrentThread.ManagedThreadId);
        }

        public void DelProduct(Product product)
        {
            //Debug.WriteLine("DelProduct: " + Thread.CurrentThread.ManagedThreadId);

            if (!CheckConnection()) throw new AccessViolationException("Удалить продукт невозможно т.к. БД недоступна!");

            using (DBMagazineContext db = new DBMagazineContext(Options))
            {

                if (!db.Products.Contains(product))
                {
                    OnProductsDbTableChanged();
                    throw new AccessViolationException("Удалить продукт невозможно т.к. его уже нет в БД! \nДанные обновлены.");
                }

                db.Products.Remove(product);
                db.SaveChangesAsync().Wait();
            }
            OnProductsDbTableChanged();
        }









        /// <summary>
        /// Метод оповещает подписчиков на событие ShopsDbTableChanged о том, что таблица Shops в БД изменилась
        /// </summary>
        private void OnShopsDbTableChanged()
        {
            ShopsDbTableChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Метод оповещает подписчиков на событие ProductsDbTableChanged о том, что таблица Products в БД изменилась
        /// </summary>
        private void OnProductsDbTableChanged()
        {
            ProductsDbTableChanged?.Invoke(this, EventArgs.Empty);
        }






        //реализация интерфейса INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
