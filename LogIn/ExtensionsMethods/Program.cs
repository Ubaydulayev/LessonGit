var myClass = new MyClass();
myClass.ChangeNumber("555");
myClass.Log();

public class MyClass
{
    public string Son { get; set; }
}

public static class MyClassExtensions
{
    public static void Log(this MyClass myClass)
    {
        Console.WriteLine(myClass.Son);
    }

    public static void ChangeNumber(this MyClass son, string newVakue)
    {
        son.Son = newVakue;
    }
}