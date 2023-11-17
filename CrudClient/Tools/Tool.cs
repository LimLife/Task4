namespace CrudClient.Tools
{
    public static class Tool
    {
        public static bool HasDefaultValues<T>(this T obj)
        {
            if (obj is null) return false;
            var type = typeof(T);
            foreach (var property in type.GetProperties())
            {
                if (property.GetValue(obj) == null ||
                    property.GetValue(obj).Equals(GetDefault(property.PropertyType)))
                {
                    return true;
                }
            }
            return false;
        }

        private static object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
