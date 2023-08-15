using System.Reflection;

namespace Application.Configuration
{
    public static class AssemblyReferenceApplication
    {
        public static readonly Assembly Assembly = typeof(AssemblyReferenceApplication).Assembly;
    }
}
