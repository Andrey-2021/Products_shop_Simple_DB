using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfAppDB.Classes;

namespace WpfAppDB.Model
{
    /// <summary>
    /// Интерфейс для работы с продуктами
    /// </summary>
   public interface IProductsModel : ICheckConnection
    {

        /// <summary>
        /// Событие. Оповещает подписчиков о том, что таблица Products в БД изменилась
        /// </summary>
        public event EventHandler ProductsDbTableChanged;



        /// <summary>
        /// Записать/создать новый продукт в БД
        /// </summary>
        /// <param name="shop">Новый продукт, добавляемый в БД</param>
        public void WriteNewProduct(Product newProduct);

        /// <summary>
        /// Записать/создать новый продукт в БД. Асинхронный метод
        /// </summary>
        /// <param name="shop">Новый продукт, добавляемый в БД</param>
        public Task WriteNewProductAsync(Product newProduct);





        /// <summary>
        /// Изменить продукт
        /// </summary>
        /// <param name="product">Изменяемый продукт</param>
        public void ChangeProduct(Product product);

        /// <summary>
        /// Изменить продукт. Асинхронный метод
        /// </summary>
        /// <param name="product">Изменяемый продукт</param>
        public Task ChangeProductAsync(Product product);





        /// <summary>
        /// Удалить продукт
        /// </summary>
        /// <param name="product">Удаляемый продукт</param>
        public void DelProduct(Product product);

        /// <summary>
        /// Удалить продукт. Асинхронный метод
        /// </summary>
        /// <param name="product">Удаляемый продукт</param>
        public Task DelProductAsync(Product product);




        /// <summary>
        /// Получить список всех продуктов
        /// </summary>
        /// <returns>Список всех продуктов в БД</returns>
        // public List<Product> GetProducts();
    }
}
