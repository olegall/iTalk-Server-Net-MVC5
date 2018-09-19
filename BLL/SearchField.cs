public class SearchField
{
    public enum Types { Name, Surname, Patronymic, LTD, Category, Subcategory };

    public int Type { get; set; }
    public string Value { get; set; }

    public SearchField(int type, string value)
    {
        Type = type;
        Value = value;
    }
}