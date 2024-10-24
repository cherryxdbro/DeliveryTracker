# DeliveryTracker - ����������� �� ��������� � �������������

## ��������:
���������� ��������� ������ ������ �������� � ����������� �� ������ ������ � ������� ������, ������������ ��������� � ������� CSV.

## ����������:
- .NET 6.0 SDK ��� ����
- NLog (������� � ����������� �������)

## ��������� � ���������:

1. �������� ������ � �������� ���:
dotnet build

2. ��������� ��������� ����� ��������� ������ ��� ����� ���� ������������:
��������� ������:
DeliveryTracker.exe -d "District1" -t "2024-10-23 14:00:00" -l "log.txt" -o "output.csv"

���� ������������ `appsettings.json`:
```json
{
    "CityDistrict": "District1",
    "FirstDeliveryDateTime": "2024-10-23 14:00:00",
    "DeliveryLog": "log.txt",
    "DeliveryOrder": "output.csv"
}

������ ���������:
dotnet run -- --cityDistrict District1 --firstDeliveryDateTime "2024-10-23 14:00:00" --deliveryLog "log.txt" --deliveryOrder "output.csv"
