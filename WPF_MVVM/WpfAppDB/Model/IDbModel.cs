using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using WpfAppDB.Classes;

namespace WpfAppDB.Model
{
    /// <summary>
    /// Интерфейс Model, для работы и с магазинами и с товаром
    /// </summary>
   public interface IDbModel:IShopsModel, IProductsModel
    {
        /// <summary>
        /// Свойство. Показывает доступна ли БД
        /// </summary>
        public bool IsDbAvailable { get; }

        /// <summary>
        /// Создание нового подключения:
        /// 1) Чтение настроек подключения из json-файла
        /// 2) проверка подключения
        /// </summary>
        /// <returns> true-БД досиупна. false - БД недоступна</returns>
        public bool CreateNewConnection();

        /// <summary>
        /// Событие. Оповещает подписчиков о том, что какое-то свойство изменилось.
        /// (Используется для реализации INotifyPropertyChanged)
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        

        /// <summary>
        /// Получить списко магазинов и список товаров из БД
        /// </summary>
        /// <returns>  shops- Возвращаемый список магазинов, products -Возвращаемый список товаров </returns>
        public (List<Shop> shops, List<Product> products) GetShopsAndProducts();

        /// <summary>
        /// Получить списко магазинов и список товаров из БД. Асинхронный метод
        /// </summary>
        /// <returns>  shops- Возвращаемый список магазинов, products -Возвращаемый список товаров </returns>
        public Task<(List<Shop> newShops, List<Product> newProducts)> GetShopsAndProductsAsync();

        
    }
}
