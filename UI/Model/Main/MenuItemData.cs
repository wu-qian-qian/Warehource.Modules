namespace UI.Model.Main;

public class MenuItemData
{
    public string Key { get; set; }
    public string Title { get; set; }

    public string Href { get; set; }
    public List<MenuItemData> Children { get; set; }
}