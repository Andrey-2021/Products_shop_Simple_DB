## БД, учебный проект.
Простая БД (всего 2 таблицы), простейший интерфейс.
#### **! Цель - реализация паттерна MVVM !**


#### Проект:
- C#, Visual Studio 2022,
- WPF,
- БД MSSQL. БД создаётся скриптами.
- Entity framework core.

#### Реализовано:
1. Реализован паттерн MVVM.
2. Используем WindowsController - контейнер, в котором регистрируем пары View и ViewModel
3. Используем два логгера. 1(Serilog)-для логирования работы программы. 2- логирование Entity Framework
4. Настройки подключения к БД в json-файле appsettings.json


##### Запуск программы.
1. Настройть подключение к БД, файл ...\Model\appsettings.json
2. Создать БД MSSQL. Заполнить данными.
Инструкция и скрипты в папке <Скрипты. Создание и заполнение данными БД MSSQL>
3. Для переподкючения к БД в программе надо нажать на кнопку/надпись (находится в правом верхнем углу окна) "Нет соединения с БД (Подключиться)". Программа попытается подключиться к БД, настройки беруться из appsettings.json
