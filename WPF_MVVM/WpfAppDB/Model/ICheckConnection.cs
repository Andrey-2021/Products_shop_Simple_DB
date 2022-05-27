using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppDB.Model
{
    //используется для перепроверки доступности БД 
   public interface ICheckConnection
    {
        /// <summary>
        /// Проверить доступна ли БД. Асинхронный метод
        /// </summary>
        public void CheckConnectionAsync();
    }
}
