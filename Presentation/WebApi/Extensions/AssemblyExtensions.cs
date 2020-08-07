using System.IO;
using System.Reflection;

namespace Helpdesk.WebAPI.Extensions
{
    public static class AssemblyExtensions
    {
        public static string ReadEmbeddedResource(this Assembly assembly, string resourceName)
        {
            using var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName));
            return reader.ReadToEnd();
        }
    }
}