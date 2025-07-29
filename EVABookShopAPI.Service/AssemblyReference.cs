using System.Reflection;

namespace EVABookShopAPI.Service
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
