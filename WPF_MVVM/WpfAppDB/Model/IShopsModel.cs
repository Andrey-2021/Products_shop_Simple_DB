using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WpfAppDB.Classes;

namespace WpfAppDB.Model
{

    /// <summary>
    /// Интерфейс для работы с магазинами
    /// </summary>
    public interface IShopsModel: ICheckConnection
    {
        /// <summary>
        /// Событие. Оповещает подписчиков о том, что таблица Shops в БД изменилась
        /// </summary>
        public event EventHandler ShopsDbTableChanged;




        /// <summary>
        /// Записать/создать новый магазин в БД
        /// </summary>
        /// <param name="shop">Новый магазин, добавляемый в БД</param>
        public void WriteNewShop(Shop newShop);

        /// <summary>
        /// Записать/создать новый магазин в БД. Асинхронный метод
        /// </summary>
        /// <param name="shop">Новый магазин, добавляемый в БД</param>
        public Task WriteNewShopAsync(Shop newShop);




        /// <summary>
        /// Изменить магазин
        /// </summary>
        /// <param name="shop">Изменяемый магазин</param>
        public void ChangeShop(Shop shop);

        /// <summary>
        /// Изменить магазин. Асинхронный метод
        /// </summary>
        /// <param name="shop">Изменяемый магазин</param>
        public Task ChangeShopAsync(Shop shop);




        /// <summary>
        /// Удалить магазин
        /// </summary>
        /// <param name="shop">Удаляемый магазин</param>
        public void DelShop(Shop shop);

        /// <summary>
        /// Удалить магазин. Асинхронный метод
        /// </summary>
        /// <param name="shop">Удаляемый магазин</param>
        public Task DelShopAsync(Shop shop);




        /// <summary>
        /// Получить список всех магазинов
        /// </summary>
        /// <returns>Список всех магазинов в БД</returns>
        // public List<Shop> GetShops();
    }
}
