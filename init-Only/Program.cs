using System;

var istance = new Program {
    MyProp = "primo"
};

//istance.MyProp = "secondo";
istance.Show();

public class Program
{
    public string MyProp {get; init;}

    public void Show() {
        Console.WriteLine("MyProp = {0}", MyProp);
    }
}
