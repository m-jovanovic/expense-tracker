using System.Reflection;

namespace Application.Events
{
    public static class AssemblyProvider
    {
        public static Assembly GetEventsAssembly() => Assembly.GetExecutingAssembly();
    }
}
