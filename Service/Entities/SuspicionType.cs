namespace Service.Entities;

public class SuspicionType
{
    public static readonly SuspicionType Js = new("JS");
    public static readonly SuspicionType RmRf = new("rm -rf");
    public static readonly SuspicionType Rundll32 = new("Rundll32");

    public string Name { get; }

    private SuspicionType(string name)
    {
        Name = name;
    }
}