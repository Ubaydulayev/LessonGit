namespace Mapper.Mappers;

public static class MyMapper
{
    public static TOtherType ToOtherType<TOtherType>(this object obj, MyMapperConfig? config = null) where TOtherType : class, new()
    {
        Type objType = obj.GetType();
        TOtherType otherTypeObj = new TOtherType();

        foreach (var propertyInfo in typeof(TOtherType).GetProperties())
        {
            if (objType.GetProperties().Any(p => p.Name == propertyInfo.Name))
            {
                var objProperty = objType.GetProperty(propertyInfo.Name);

                if (objProperty?.PropertyType != propertyInfo.PropertyType)
                    continue;

                object? objPropertyValue = objProperty?.GetValue(obj);
                propertyInfo.SetValue(otherTypeObj, objPropertyValue);
            }

            if (config != null)
            {
                foreach (var configProperty in config.PropertiesConfig)
                {
                    if (propertyInfo.Name == configProperty.ToProperty)
                    {
                        var objProperty = objType.GetProperty(configProperty.FromProperty);

                        if (objProperty?.PropertyType != propertyInfo.PropertyType)
                            continue;

                        object? objPropertyValue = objProperty?.GetValue(obj);
                        propertyInfo.SetValue(otherTypeObj, objPropertyValue);
                    }
                }
            }
        }

        return otherTypeObj;
    }

    /*public static TOtherType ToOtherType<TOtherType>(this object obj) where TOtherType : class, new()
    {
        Type objType = obj.GetType();
        PropertyInfo[] objProperties = objType.GetProperties();
        object? firstPropertyValue = objProperties[0].GetValue(obj);
        string firstPropertyName = objProperties[0].Name;
        Type otherType = typeof(TOtherType);
        TOtherType otherTypeObj = new TOtherType();
        PropertyInfo[] otherTypeProperties = otherType.GetProperties();
        otherTypeProperties[0].SetValue(otherTypeObj, firstPropertyValue);
        return otherTypeObj;
    }*/
}
