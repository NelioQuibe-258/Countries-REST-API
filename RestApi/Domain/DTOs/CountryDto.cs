using System.Xml.Serialization;

public class CountryDto
{
    public NameInfo? Name { get; set; }
    public List<string>? Capital { get; set; }
    public string? Region { get; set; }
    public string? Subregion { get; set; }
    public long Population { get; set; }
    public double Area { get; set; }
    public List<string>? Timezones { get; set; }

    [XmlIgnore]
    public Dictionary<string, string>? NativeName { get; set; }
    public Flags? Flags { get; set; }

}

public class NameInfo
{
    public string? Common { get; set; }
    public string? Official { get; set; }

}

public class Flags
{
    public string? Png { get; set; }
    public string? Svg { get; set; }
}