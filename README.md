# Разработка динамической потоковой модели с помощью системной динамики

## Задача 
Внести изменения в модель так, чтобы появился хотя бы еще один уровень, связанный с другими элементами модели, а именно, добавить учет перевозки товара через уровень (в машину погрузили, затем выгрузили - промежуточный между складами уровень).

## Описание изменении модели
Новый уровень – товары в процессе перевозки (**Transfer**).<br/>
Механизм перевозки товаров:

- Если флажок для перевозки активен и в данный момент уровень **Transfer** пуст, то выбранный объем товара загружается в машину, при этом он исчезает с основного склада (уровень BasicStore).

- Если на уровне **Transfer** уже находятся товары, загрузить дополнительные будет невозможно.

- На следующем этапе товары из машины выгружаются на склад магазина (уровень **ShopStore**). Если на складе недостаточно места, товары теряются (перемещаются на уровень **All_Lost**).

## Скриншоты Powersim Studio
![Снимок экрана 2024-12-22 131504](https://github.com/user-attachments/assets/72740635-a11e-4902-a9e3-5513bfae9e35)
![image copy](https://github.com/user-attachments/assets/8619d63c-a93c-42dd-a69b-733c557b1588)

## Скриншот программмы
![image-1 copy](https://github.com/user-attachments/assets/42efae22-9c4c-45ce-a76a-d65bd38a5b6b)
