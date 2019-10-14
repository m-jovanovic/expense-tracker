using System.Reflection;

namespace Application.Documents
{
    public static class AssemblyProvider
    {
        public static Assembly GetDocumentsAssembly() => Assembly.GetExecutingAssembly();
    }
}
