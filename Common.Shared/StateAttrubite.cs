namespace Common.Shared;

public class StateAttrubite : Attribute
{
    public StateAttrubite(string keyName)
    {
        KeyName = keyName;
    }

    public string KeyName { get; set; }
}