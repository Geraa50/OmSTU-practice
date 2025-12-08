class Product
{
    public string Name;
    public string Article;
    public int Quantity;
    public int Sold;
    public double Price;

    public Product(string name, string article, int quantity, int sold, double price)
    {
        Name = name;
        Article = article;
        Quantity = quantity;
        Sold = sold;
        Price = price;
    }

    public int GetRemainder()
    {
        return Quantity - Sold;
    }

    public double GetSalesAmount()
    {
        return Sold * Price;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Товар: {Name}");
        Console.WriteLine($"Артикул: {Article}");
        Console.WriteLine($"Остаток: {GetRemainder()}");
        Console.WriteLine($"Продано на сумму: {GetSalesAmount()}");
        Console.WriteLine();
    }
}

class Store
{
    private Product[] products;

    public Store(Product[] products)
    {
        this.products = products;
    }

    public void PrintAllProducts()
    {
        foreach (Product p in products)
        {
            p.PrintInfo();
        }
    }

    public double GetTotalSales()
    {
        double sum = 0;

        foreach (Product p in products)
        {
            sum += p.GetSalesAmount();
        }

        return sum;
    }

    public void PrintTotalSales()
    {
        Console.WriteLine($"Общая сумма продаж: {GetTotalSales()}");
    }
}

class Program
{
    static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        Product[] products = new Product[n];

        for (int i = 0; i < n; i++)
        {
            string line = Console.ReadLine();
            string[] parts = line.Split();

            string name = parts[0];
            string article = parts[1];
            int quantity = int.Parse(parts[2]);
            int sold = int.Parse(parts[3]);
            double price = double.Parse(parts[4]);

            products[i] = new Product(name, article, quantity, sold, price);
        }
        Store store = new Store(products);

        store.PrintAllProducts();

        store.PrintTotalSales();
    }
}