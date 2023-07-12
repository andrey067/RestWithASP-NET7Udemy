using System.Reflection;

namespace Infrastructure.Configuration
{
    public static class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
