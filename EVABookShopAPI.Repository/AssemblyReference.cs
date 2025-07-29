using System.Reflection;

namespace EVABookShopAPI.Repository
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}