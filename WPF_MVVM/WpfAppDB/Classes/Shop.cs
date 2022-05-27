using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace WpfAppDB.Classes
{
    public class Shop : INotifyPropertyChanged, IDataErrorInfo
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

        string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public virtual ICollection<Product> Products { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public Shop()
        {
            Products = new HashSet<Product>();
        }



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
            // get { throw new NotImplementedException(); }
            get
            {
                return 
                     PropertyValidation("Name")
                    + PropertyValidation("Address")
                    + PropertyValidation("Phone")
                    + PropertyValidation("Email")
                    ;
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
                case "Name":
                    //Обработка ошибок для свойства Name
                    if (String.IsNullOrEmpty(Name)) error = "Необходимо ввести название.";
                    else if (Name.Length > 75) error = "Длина названия должна быть не более 75 символов.";
                    break;

                case "Address":
                    if (String.IsNullOrEmpty(Address)) error = "Необходимо ввести адрес.";
                    else if (Address.Length > 120) error = "Длина адреса должна быть не более 120 символов.";
                    break;

                case "Phone":
                    if (String.IsNullOrEmpty(Phone)) error = "Необходимо ввести телефон.";
                    else if (Phone.Length > 20) error = "Длина телефона должна быть не более 20 символов.";
                    break;

                case "Email":
                    if (String.IsNullOrEmpty(Email)) error = "Необходимо ввести e-mail.";
                    else
                    {
                        if (Email.Length > 30) error = "Длина e-mail должна быть не более 30 символов.";
                        else
                        {
                            //проверка соответствия формату e-mail
                            // взято https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format

                            if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
                                error = "e-mail не соответствует формату.";
                        }
                    }
                    break;
            }
            return error;
        }
    }
}
