using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using WpfAppDB.Classes;

namespace WpfAppDB.ViewModel
{
    public class MessageWindowViewModel
    {
        /// <summary>
        /// Выводимое сообщение
        /// </summary>
        public string Message { get; set; }



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
                       //предварительно проверяем, если команда не создана, создаём её
                return closeWindowCommand ?? (closeWindowCommand = new RelayCommand(CloseWindow, CheckIsPossibleCloseWindow));
            }
        }




        /// <summary>
        /// Конструктор
        /// </summary>
        public MessageWindowViewModel(string message)
        {
            Message = message;
        }



        /// <summary>
        /// Метод. Закрыть окно без сохранения изменений
        /// </summary>
        void CloseWindow(Object parametr)
        {
            //MessageBox.Show("Проверка - Закрыть сообщение");
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
