using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace Convesys.Common.Analytics.PythonDotNet
{
    public class IronPython
    {
        public static Task<string> KolmogorovSmirnovTest(string scriptPath, string serviceid, string parameter)
        {
            //https://betterprogramming.pub/running-python-script-from-c-and-working-with-the-results-843e68d230e5
            try
            {
                var engine = Python.CreateEngine(); // Extract Python language engine from their grasp
                var scope = engine.CreateScope(); // Introduce Python namespace (scope)
                var parameters = new Dictionary<string, object>
                {
                    { "serviceid", serviceid},
                    { "parameter", parameter}
                };

                scope.SetVariable("params", parameters);
                var source = engine.CreateScriptSourceFromFile(scriptPath); // Load the script
                object result = source.Execute(scope);
                parameter = scope.GetVariable<string>("parameter"); // To get the finally set variable 'parameter' from the python script
                return Task.FromResult(parameter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}