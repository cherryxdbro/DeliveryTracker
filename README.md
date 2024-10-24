# DeliveryTracker

## Описание

Приложение фильтрует заказы службы доставки в зависимости от района города и времени заказа, предоставляя результат в формате CSV.

## Установка и настройка

1. Скачайте проект и соберите его:
dotnet build

2. Настройте параметры через командную строку или через файл конфигурации:
Командная строка:
DeliveryTracker.exe -d "District1" -t "2024-10-23 14:00:00" -l "log.txt" -o "output.csv"

Файл конфигурации "appsettings.json":
{
    "CityDistrict": "861136c7-67fa-496a-81e6-dbd54df14533",
    "FirstDeliveryDateTime": "2024-10-22 09:45:00",
    "DeliveryLog": "delivery_tracker.log",
    "DeliveryOrder": "filtered_orders.csv"
}

## Запуск программы

dotnet run -- --cityDistrict District1 --firstDeliveryDateTime "2024-10-23 14:00:00" --deliveryLog "log.txt" --deliveryOrder "output.csv"
