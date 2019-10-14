using System.Reflection;

namespace Application.Commands
{
    public static class AssemblyProvider
    {
        public static Assembly GetCommandsAssembly() => Assembly.GetExecutingAssembly();
    }
}
