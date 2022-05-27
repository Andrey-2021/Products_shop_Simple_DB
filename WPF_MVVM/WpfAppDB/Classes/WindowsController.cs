using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace WpfAppDB.Classes
{
   public class WindowsController
    {
        /// <summary>
        /// Словарь зарегистрированных типов
        /// </summary>
        Dictionary<Type, Type> windowsTypesList = new Dictionary<Type, Type>();

        /// <summary>
        /// Словарь существующих/созданных объектов
        /// </summary>
        Dictionary<object, Window> openWindowsList = new Dictionary<object, Window>();


        /// <summary>
        /// Единственный экземпляр этого класса WindowsController 
        /// </summary>
        private static WindowsController instance;

        /// <summary>
        /// Конструктор
        /// </summary>
        private WindowsController()
        { }

        /// <summary>
        /// Получить экземпляр этого класса (WindowsController)
        /// </summary>
        /// <returns>экземпляр этого класса (WindowsController) </returns>
        public static WindowsController GetInstance()
        {
            if (instance == null)
                instance = new WindowsController();
            return instance;
        }


        /// <summary>
        /// Регистрация пары ViewModel И View
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <typeparam name="Win">Mosel</typeparam>
        public void RegistryWindowType<VM, Win>() where Win:Window, new() where VM:class
        {
            var vmType = typeof(VM);
            if (vmType.IsInterface) throw new ArgumentException("Нельзя зарегистрировать интерфейс");
            
            if (windowsTypesList.ContainsKey(vmType)) throw new InvalidOperationException($"Тип {vmType.FullName} уже был зарегистрирован");
            
            windowsTypesList[vmType] = typeof(Win);
        }



        /// <summary>
        /// Создать экземпляр окна
        /// </summary>
        /// <param name="viewModel">ViewModel, для которой нужно создать экземпляр окно</param>
        /// <returns></returns>
        private Window CreateWindowInstance(object viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("ViewModel == null");

            var viewModelType = viewModel.GetType();
            if (!windowsTypesList.ContainsKey(viewModelType)) throw new ArgumentException($"Этот тип {viewModelType.FullName} не зарегистрирован!");

            Type windowType = null;
            windowsTypesList.TryGetValue(viewModelType, out windowType);

            if (windowType == null) throw new ArgumentException($"Не зарегистрирован тип соответствующий входящему типу {viewModelType.FullName}");

            var window = (Window)Activator.CreateInstance(windowType);

            return window;
        }


        /// <summary>
        /// Показать окно
        /// </summary>
        /// <param name="viewModel">ViewModel, для которой нужно показать окно</param>
        public void ShowWindow(object viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("ViewModel ==null");
            if (openWindowsList.ContainsKey(viewModel)) throw new ArgumentException("Окно, соотетствующее viewModel уже показано");

            //Dispatcher.CheckAccess()метод,
            //который возвращает истину, если код находится в потоке пользовательского интерфейса,
            //и ложь, если в каком-то другом потоке.

            //потокобезопасное создание окна
            if (Application.Current.Dispatcher.CheckAccess()) //проверка
            { //всё хорошо - мы в UI потоке
                var window = CreateWindowInstance(viewModel);
                window.DataContext = viewModel;
                openWindowsList.Add(viewModel, window);
                window.ShowDialog();
            }
            else //мы НЕ в UI-потоке
            {
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, 
                    new Action(()=>
                    {
                        var window = CreateWindowInstance(viewModel);
                        window.DataContext = viewModel;
                        openWindowsList.Add(viewModel, window);
                        window.ShowDialog();
                    }));
            }
        }






        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="viewModel">ViewModel, для которой нужно закрыть окно</param>
        public void CloseWindow(object viewModel)
        {

            Window window;

            if (!openWindowsList.TryGetValue(viewModel, out window))
                throw new InvalidOperationException("Нет открытого окна, соответствующего данному viewModel");

            window.Close();
            openWindowsList.Remove(viewModel);
        }


    }
}
