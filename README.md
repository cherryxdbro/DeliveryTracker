# DeliveryTracker

## Установка и настройка

1. Склонируйте репозиторий, или скачайте и распакуйте архив проекта
2. Убедитесь, что у вас установлен .NET 9.0
3. В командной строке выполните следующую команду для запуска приложения:

```bash
dotnet run -c Release
```

Программа принимает параметры через командную строку:

- --CityDistrict - район для фильтрации заказов.
- --FirstDeliveryDateTime - время первого заказа (в формате yyyy-MM-dd HH:mm:ss).
- --DeliveryLog - путь к файлу логов.
- --DeliveryOrder - путь к файлу с отфильтрованными заказами.

### Переменные также могут быть заданы через среду

```bash
export CityDistrict=""
export FirstDeliveryDateTime=""
export DeliveryLog=""
export DeliveryOrder=""
```

### Дополнительно параметры могут быть заданы через файл конфигурации "appsettings.json"

```text
{
  "CityDistrict": "",
  "FirstDeliveryDateTime": "",
  "DeliveryLog": "",
  "DeliveryOrder": ""
}
```

## Тестирование
Проект содержит автоматические модульные тесты, написанные с использованием библиотеки XUnit.

### Запуск тестов
Для запуска тестов выполните команду:

```bash
dotnet test -c Release
```
