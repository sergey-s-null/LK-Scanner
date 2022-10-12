namespace Service.Entities;

public class SuspicionType
{
    public static readonly SuspicionType Js = new SuspicionType("JS");
    public static readonly SuspicionType RmRf = new SuspicionType("rm -rf");
    public static readonly SuspicionType Rundll32 = new SuspicionType("Rundll32");

    public string Name { get; }

    private SuspicionType(string name)
    {
        Name = name;
    }
}