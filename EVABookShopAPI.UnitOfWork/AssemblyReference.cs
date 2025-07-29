using System.Reflection;

namespace EVABookShopAPI.UnitofWork
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}