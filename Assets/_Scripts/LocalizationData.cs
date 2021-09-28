[System.Serializable]
public class LocalizationData 
{
    public LocalizationItem[] items;
}

[System.Serializable]
public class LocalizationItem
{
    public LocalizationItem(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
    public string key;
    public string value;
}