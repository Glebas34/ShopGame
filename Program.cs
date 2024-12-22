Random rand = new();
int account = 10000;
int basicStore = 80;
int inTransfer = 0;
int shopStore = 30;

for (int modelTime = 0; modelTime <= 100; modelTime++)
{
    Console.WriteLine($"Модельное время: {modelTime}/100");
    Console.WriteLine($"Расчетный счет: {account}");

    var (volumeOfLot, priceOfUnitOfLot, priceOfLot) = GenerateLot(modelTime);
    DisplayLotInfo(volumeOfLot, priceOfUnitOfLot, priceOfLot);
    bool buyEnabled = GetUserInput("Покупаем товар? (да - 1 / нет - 0):");

    DisplayWarehouseInfo();
    bool transferEnabled = false;
    int volumeOfTransfer = 0;

    if (inTransfer == 0)
    {
        transferEnabled = GetUserInput("Перевозим товар? (да - 1 / нет - 0):");
        if (transferEnabled)
        {
            volumeOfTransfer = GetVolumeOfTransfer();
        }
    }

    bool tradingEnabled = GetUserInput("Продаем товар? (да - 1 / нет - 0):");

    if (buyEnabled)
    {
        BuyGoods(volumeOfLot, priceOfLot);
    }

    if (transferEnabled)
    {
        TransferGoods(volumeOfTransfer);
    }

    if (tradingEnabled)
    {
        TradeGoods();
    }

    Console.WriteLine("\n");
}

(int volumeOfLot, int priceOfUnitOfLot, int priceOfLot) GenerateLot(int modelTime)
{
    int volumeOfLot = 30 + rand.Next(21);
    int priceOfUnitOfLot = 35 + rand.Next(31) + (int)Math.Round(35.0 * modelTime * (0.03 + 0.01 * rand.Next(2)));
    int priceOfLot = volumeOfLot * priceOfUnitOfLot;
    return (volumeOfLot, priceOfUnitOfLot, priceOfLot);
}

void DisplayLotInfo(int volumeOfLot, int priceOfUnitOfLot, int priceOfLot)
{
    Console.WriteLine($"Объем партии: {volumeOfLot}");
    Console.WriteLine($"Цена за ед.: {priceOfUnitOfLot}");
    Console.WriteLine($"Цена партии: {priceOfLot}");
}

void DisplayWarehouseInfo()
{
    Console.WriteLine($"Основной склад: {basicStore}/500");
    Console.WriteLine($"В машине: {inTransfer}/100");
    Console.WriteLine($"Склад магазина: {shopStore}/100");
}

bool GetUserInput(string prompt)
{
    Console.WriteLine(prompt);
    return Console.ReadLine() == "1";
}

int GetVolumeOfTransfer()
{
    int volumeOfTransfer;
    Console.WriteLine("Введите объем перевозки [20; 100]:");

    while (!int.TryParse(Console.ReadLine(), out volumeOfTransfer) || volumeOfTransfer < 20 || volumeOfTransfer > 100)
    {
        Console.WriteLine("Введите объем перевозки [20; 100]:");
    }

    return volumeOfTransfer;
}

void BuyGoods(int volumeOfLot, int priceOfLot)
{
    if (priceOfLot <= account)
    {
        account -= priceOfLot;
        basicStore = Math.Min(basicStore + volumeOfLot, 500);
    }
}

void TransferGoods(int volumeOfTransfer)
{
    if (inTransfer > 0)
    {
        shopStore = Math.Min(shopStore + inTransfer, 100);
        inTransfer = 0;
    }
    else if (basicStore > 0)
    {
        inTransfer = Math.Min(volumeOfTransfer, basicStore);
        basicStore -= inTransfer;
    }
}

void TradeGoods()
{
    int retailPrice;
    Console.WriteLine("Введите розничную цену [100; 200]:");

    while (!int.TryParse(Console.ReadLine(), out retailPrice) || retailPrice < 100 || retailPrice > 200)
    {
        Console.WriteLine("Введите розничную цену [100; 200]:");
    }

    int demand = (int)Math.Round((21 + rand.Next(16)) * (1.0 - (1.0 / (1 + Math.Exp(-0.05 * (retailPrice - 100))))));
    Console.WriteLine($"Спрос: {demand}");

    if (shopStore >= demand)
    {
        shopStore -= demand;
        account += retailPrice * demand;
    }
    else
    {
        account += retailPrice * shopStore;
        shopStore = 0;
    }
}
