# DeliveryTracker

## Установка и настройка

1. Склонируйте репозиторий, или скачайте архив проекта
2. Убедитесь, что у вас установлен .NET 9.0
3. В командной строке выполните следующую команду для запуска приложения:

```bash
dotnet run -c Release
```

Программа принимает параметры через командную строку:

--CityDistrict - район для фильтрации заказов.
--FirstDeliveryDateTime - время первого заказа (в формате yyyy-MM-dd HH:mm:ss).
--DeliveryLog - путь к файлу логов.
--DeliveryOrder - путь к файлу с отфильтрованными заказами.

### Дополнительно параметры могут быть заданы через файл конфигурации "appsettings.json"

```text
{
  "CityDistrict": "value",
  "FirstDeliveryDateTime": "value",
  "DeliveryLog": "value",
  "DeliveryOrder": "value"
}
```

### Переменные также могут быть заданы через среду

```bash
export CityDistrict="value"
export FirstDeliveryDateTime="value"
export DeliveryLog="value"
export DeliveryOrder="value"
```
