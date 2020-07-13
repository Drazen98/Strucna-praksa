using System;
using System.Collections.Generic;


interface IPrintData
{
    void printData();
}

class Article {
    protected string brand;
    protected string name;
    protected double price;

    public virtual bool expired()
    {
        return false;
    }
    public void printArticle()
    {
        Console.WriteLine("Name: " + this.name + Environment.NewLine + "Brand: " + this.brand +
            Environment.NewLine + "Price: " + this.price + Environment.NewLine + "Type: " + this.GetType());
    }
    public string getName() { 
    return this.name;
    }
}

class Electronics : Article
{
    public int waranty;
    public double batteryLife;
    public double cpuSpeed;
    public bool internetConnection;

    public Electronics(string brand, string name, double price, int waranty, double batteryLife, double cpuSpeed, bool internetConnection) {
        this.brand = brand;
        this.name = name;
        this.price = price;
        this.waranty = waranty;
        this.batteryLife = batteryLife;
        this.cpuSpeed = cpuSpeed;
        this.internetConnection = internetConnection;
    }
}

abstract class Consumable : Article
{
    protected int calories;
    protected double carbons;
    protected double fats;
    protected DateTime expireDate;

    public int daysUntilExpire()
    {
        return (this.expireDate - DateTime.Now).Days;
    }
    public bool expiresSoon()
    {
        return this.daysUntilExpire() <= 2;
    }
    public override bool expired()
    {
        return daysUntilExpire() <= 0;
    }
}
class Food : Consumable, IPrintData{
    public string type;
    public bool vegetarian;
    public Food(string brand, string name, double price, int calories, double carbons, double fats, DateTime expireDate, string type, bool vegetarian)
    {
        this.brand = brand;
        this.name = name;
        this.price = price;
        this.calories = calories;
        this.carbons = carbons;
        this.fats = fats;
        this.expireDate = expireDate;
        this.type = type;
        this.vegetarian = vegetarian;
    }
    public void printData()
    {
        Console.WriteLine("brand: " + this.brand + Environment.NewLine + "name: " + this.name + Environment.NewLine + "price: " +
            this.price + Environment.NewLine + "calories: " + this.calories + Environment.NewLine + "carbons: " + this.carbons + Environment.NewLine+
            "fats: " + this.fats + Environment.NewLine + "Days until expires: " + this.daysUntilExpire() + Environment.NewLine + "type: "
            + this.type + Environment.NewLine + "Is vegetarian: " + this.vegetarian);
    }
}

class Drink : Consumable {
    public bool isCarbonated;
    public double alcoholConcentration;
    public Drink(string brand, string name, double price, int calories, double carbons, double fats, DateTime expireDate,bool isCarbonated, double alcoholConcentration)
    {
        this.brand = brand;
        this.name = name;
        this.price = price;
        this.calories = calories;
        this.carbons = carbons;
        this.fats = fats;
        this.expireDate = expireDate;
        this.isCarbonated = isCarbonated;
        this.alcoholConcentration = alcoholConcentration;
    }
    public bool isAlcoholic()
    {
        return this.alcoholConcentration != 0;
    }

}

class Person : IPrintData{
    public string firstName;
    public string lastName;
    public DateTime dateOfBirth;
    public int height;
    public double weight;

    public Person(string firstName, string lastName, DateTime dateOfBirth, int height, double weight) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
        this.height = height;
        this.weight = weight;
    }
    public Person(string firstName, string lastName, DateTime dateOfBirth) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateOfBirth = dateOfBirth;
        this.height = 0;
        this.weight = 0;
    }
    public int calculateAge()
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;
        return age;
    }
    public void printData() {
        string stringHeight = "Height: " + this.height + "cm";
        string stringWeight = "" +"Weight: " + this.weight + "kg";
        if (this.height == 0)
            stringHeight = "Height: Unspecified";
        if (this.weight == 0)
            stringWeight = "Weight: Unspecified";
        Console.WriteLine("Person info:" + Environment.NewLine + "First name: " + this.firstName + Environment.NewLine + "Last name: " + this.lastName  + Environment.NewLine + "Age: " + this.calculateAge() + " years" + Environment.NewLine + stringHeight + Environment.NewLine + stringWeight);
    }
}
class Store : IPrintData {
    private string name;
    private List<Person> employes;
    private List<Tuple<Article, int>> articles;

    public Store(string name) {
        this.name = name;
        this.employes = new List<Person>();
        this.articles = new List<Tuple<Article, int>>();
    }

    protected void setNewQuantity(string name, int value)
    {
        for (int i = 0; i < articles.Count; i++)
            if (articles[i].Item1.getName() == name && value >= 0)
                articles[i] = new Tuple<Article, int>(articles[i].Item1, value);
    }
    public void addEmploye(ref Person employe) {
        this.employes.Add(employe);
    }
    public void addArticle(Article article, int quantity) {
        this.articles.Add(new Tuple<Article, int>(article, quantity));
    }
    public void printOlderEmployes()
    {
        Console.WriteLine("Older employes");
        foreach (var i in employes)
        {
            if (i.calculateAge() >= 60)
                i.printData();
        }
    }
    public void foodExpiringSoon()
    {
        Console.WriteLine("Food that expires soon");
        foreach (var i in articles)
        {
            if (i.Item1.expired())
                i.Item1.printArticle();

        }
    }
    public void lowQuantityArticles()
    {
        Console.WriteLine("Articles with quantaty less than 6");
        foreach (var i in articles)
            if (i.Item2 < 6)
                i.Item1.printArticle();
    }
    public void removeArticle(string name)
    {
        for (int i = articles.Count - 1; i >= 0; i--)
            if (articles[i].Item1.getName() == name)
                articles.RemoveAt(i);
    }
    public int quantityOfArticle(string name)
    {
        foreach (var i in articles)
            if (i.Item1.getName() == name)
                return i.Item2;
        return 0;
    }
    public void decresseArticle(string name) {
        this.setNewQuantity(name, quantityOfArticle(name) - 1);
    }
   public void printArticles()
    {
        Console.WriteLine("Articles: ");
        foreach (var i in articles)
        {
            i.Item1.printArticle();
            Console.WriteLine("Article quantity: " + i.Item2);
        }
    }
    public void printEmployes()
    {
        Console.WriteLine("Employes: ");
        foreach (var i in employes)
            i.printData();
    }
    public void printData()
    {
        Console.WriteLine("Store data" + Environment.NewLine + "Name: " + this.name);
        this.printArticles();
        this.printEmployes();   
    }

}
class TestClass
{
    static void Main(string[] args)
    {
        Person p1 = new Person("Pero", "Peric", new DateTime(1980, 5, 23), 174, 78.8);
        Person p2 = new Person("Ivan", "Ivic", new DateTime(1960, 8, 12));
        Person p3 = new Person("Ivo", "Ivic", new DateTime(1960, 3, 23));

        Food bajadera = new Food("Kraš", "Bajadera", 40.99, 450, 30, 5, new DateTime(2022, 10,23),"Milk products",true);
        Food majoneza = new Food("Zvijezda", "Majoneza", 20, 500, 350, 150, new DateTime(2020, 12, 23), "Sauce", true);
        Food kulen = new Food("Pik", "Kulen", 60, 400, 310, 90, new DateTime(2021, 7, 14), "Meat", false);

        Drink cola = new Drink("Coca Cola", "Coca Cola", 12, 300, 250, 0, new DateTime(2060, 8, 24), true, 0);
            

        Store store1 = new Store("Konzum");
        store1.addEmploye(ref p1);
        store1.addEmploye(ref p2);
        store1.addEmploye(ref p3);
        store1.addArticle(bajadera,10);
        store1.addArticle(majoneza, 15);
        store1.addArticle(kulen, 8);
        store1.addArticle(cola, 30);
        
        for (int i=0;i<40;i++)
            store1.decresseArticle("Coca Cola");

        store1.printData();
    }
}