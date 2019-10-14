using System.Reflection;

namespace Application.Queries
{
    public static class AssemblyProvider
    {
        public static Assembly GetQueriesAssembly() => Assembly.GetExecutingAssembly();
    }
}
