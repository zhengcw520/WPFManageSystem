using Furion;
using System.Reflection;

namespace SugarDemo.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "SugarDemo.Application",
            "SugarDemo.Core",
            "SugarDemo.Web.Core"
        };
    }
}