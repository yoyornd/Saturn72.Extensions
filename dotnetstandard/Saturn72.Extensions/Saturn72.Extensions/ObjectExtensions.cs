
namespace Saturn72.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool NotNull(this object obj)
        {
            return !IsNull(obj);
        }
    }
}
