using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class ParamDefinition : Collection<ParamSetDefinition>
    {
        public ParamDefinition() : base() { }

        public ParamDefinition(IList<ParamSetDefinition> list) : base(list) { }

        public string GetDefaultParameterSetName()
        {
            if (this.Count == 0 || this.Any(s => s.All(p => !p.Mandatory)))
                return null;

            return "Set1";
        }

        internal void Render(PowerShellModuleWriter writer)
        {
            if (this.Count == 0 || (this.Count == 1 && this[0].Count == 0))
            {
                writer.WriteLine("Param()");
                return;
            }

            writer.WriteLine("Param(");
            PowerShellModuleWriter content = new PowerShellModuleWriter(1);

            int setNum = 0;
            foreach (ParamSetDefinition psd in this)
            {
                if (psd.Any(p => p.Mandatory))
                {
                    setNum++;
                    psd.SetName = String.Format("setNum{0}", setNum);
                } else
                    psd.SetName = "";
            }

            var paramsWithNames = this.SelectMany(psd => psd.Select(p => new { SetName = psd.SetName, Param = p })).ToArray();

            int setCount = paramsWithNames.GroupBy(a => a.SetName).Count(g => g.Key.Length > 0);

            Collection<KeyValuePair<FunctionParameter, string[]>> parameterList = new Collection<KeyValuePair<FunctionParameter, string[]>>();
            while (paramsWithNames.Length > 0)
            {
                FunctionParameter item = paramsWithNames.First().Param;
                parameterList.Add(new KeyValuePair<FunctionParameter, string[]>(item, 
                    paramsWithNames.Where(a => a.Param.Equals(item)).Select(a => a.SetName).Where(s => s.Length > 0).ToArray()));
                paramsWithNames = paramsWithNames.Where(a => !a.Param.Equals(item)).ToArray();
            }

            for (int i=0; i<parameterList.Count; i++)
            {
                if (i > 0)
                    content.WriteLine();

                if (parameterList[i].Value.Length == 0 || parameterList[i].Value.Length == setCount)
                    content.WriteFormat(true, "[Parameter(Mandatory=${0})]", parameterList[i].Key.Mandatory);
                else
                {
                    foreach (string psm in parameterList[i].Value)
                        content.WriteFormat(true, "[ParaParametermter(Mandatory=${0}, ParameterSetName={1})]",
                            parameterList[i].Key.Mandatory, PowerShellModuleWriter.GetQuotedString(psm));
                }

                content.WriteFormat(true, "[{0}]${1}{2}", parameterList[i].Key.ParameterType, parameterList[i].Key.Name, (i < parameterList.Count - 1) ? "," : "");
            }

            writer.WriteLine(content.ToString());
            writer.WriteLine("}");
       }
    }
}
