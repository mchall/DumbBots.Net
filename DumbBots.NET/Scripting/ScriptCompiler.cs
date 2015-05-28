using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Scripting
{
    /// <summary>
    /// Handles the compilation of the user scripts
    /// </summary>
    internal class ScriptCompiler
    {
        internal List<ErrorInformation> CompileScript(string language, string code, int team, List<string> dllList)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "AI\\AIscript" + team + ".dll";
            return CompileScript(language, code, filename, dllList);
        }

        internal List<ErrorInformation> CompileDirectorScript(string language, string code, List<string> dllList)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "AI\\Director.dll";
            return CompileScript(language, code, filename, dllList);
        }

        internal List<ErrorInformation> CompileScript(string language, string code, string fileName, List<string> dllList)
        {
            CodeDomProvider cdp = CodeDomProvider.CreateProvider(language);
            CompilerParameters cp = new CompilerParameters();

            cp.OutputAssembly = fileName;
            cp.ReferencedAssemblies.Add("DumbBots.NET.Api.dll");
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("System.Drawing.dll");
            cp.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            cp.ReferencedAssemblies.AddRange(dllList.ToArray()); //Add external DLLs mentioned in user script

            cp.WarningLevel = 1;

            cp.CompilerOptions = "/target:library /optimize";
            cp.GenerateExecutable = false;
            cp.GenerateInMemory = false;

            System.CodeDom.Compiler.TempFileCollection tfc = new TempFileCollection(AppDomain.CurrentDomain.BaseDirectory, false);
            CompilerResults cr = new CompilerResults(tfc);

            cr = cdp.CompileAssemblyFromSource(cp, code);

            var errorList = new List<ErrorInformation>();
            foreach (CompilerError ce in cr.Errors)
            {
                errorList.Add(new ErrorInformation(String.Format("{0}: {1} Line: {2}", ce.ErrorNumber, ce.ErrorText, ce.Line), ce.Line));
            }
            return errorList;
        }
    }
}