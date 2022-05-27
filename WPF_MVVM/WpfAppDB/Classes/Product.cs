using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppDB.Classes
{
    public class Product : INotifyPropertyChanged, IDataErrorInfo
    {
        int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }


        decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }


        int shopsId;
        public int ShopsId
        {
            get { return shopsId; }
            set
            {
                shopsId = value;
                OnPropertyChanged();
            }
        }


        public virtual Shop Shops { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }


        // реализация интерфейса IDataErrorInfo.
        // Используется для проверки правильности вввода пользователем данных в поля на форме
        public string this[string columnName]
        {
            get
            {
                return PropertyValidation(columnName);
            }
        }

        public string Error
        {
            //get { throw new NotImplementedException(); }
            get
            {
                return PropertyValidation("Price") + PropertyValidation("Name");
            }
        }


        /// <summary>
        /// Проверка свойст на допустимые значения
        /// </summary>
        string PropertyValidation(string columnName)
        {
            string error = String.Empty;
            switch (columnName)
            {
                case "Price":

                    if ((Price < 0) || (Price >= decimal.MaxValue))
                    {
                        error = "Цена должен быть больше 0 и меньше " + decimal.MaxValue.ToString();
                    }
                    break;

                case "Name":
                    //Обработка ошибок для свойства Name
                    if (String.IsNullOrEmpty(Name)) error = "Необходимо ввести название.";
                    else
                    {
                        if (Name.Length > 75) error = "Длина названия должна быть не более 75 символов.";
                    }
                    break;
            }
            return error;
        }

    }
}
