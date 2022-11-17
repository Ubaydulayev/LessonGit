using System;
namespace Mapper.Mappers;
public class MyMapperConfig
{
public List<MyMapperConfigProperty> PropertiesConfig = new List<MyMapperConfigProperty>();

    public void Map(string fromPropertyName, string toPropertyName)
    {
        PropertiesConfig.Add(new MyMapperConfigProperty(fromPropertyName, toPropertyName));
    }
}

public class MyMapperConfigProperty
{
    public MyMapperConfigProperty(string fromProperty, string toProperty)
    {
        FromProperty = fromProperty;
        ToProperty = toProperty;
    }

    public string FromProperty { get; set; }
    public string ToProperty { get; set; }

}