using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class FunctionDefinition
    {
        public string Name { get; set; }

        public string OutputType { get; set; }

        public PowerShellModuleWriter DynamicParamCode { get; private set; }

        public PowerShellModuleWriter ProcessCode { get; private set; }

        public ParamDefinition Params { get; private set; }

        public FunctionDefinition(string name, string outputType, ParamDefinition paramDef)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", "name");

            this.Name = name.Trim();
            this.Params = (paramDef == null) ? new ParamDefinition() : paramDef;
            this.DynamicParamCode = new PowerShellModuleWriter();
            this.ProcessCode = new PowerShellModuleWriter();
            this.OutputType = outputType;
        }

        public FunctionDefinition(string name, string outputType) : this(name, outputType, null as ParamDefinition) { }

        public FunctionDefinition(string name, Type outputType, ParamDefinition paramDef) : this(name, (outputType == null) ? null : outputType.FullName, paramDef) { }

        public FunctionDefinition(string name, Type outputType) : this(name, (outputType == null) ? null : outputType, null as ParamDefinition) { }

        public FunctionDefinition(string name, ParamDefinition paramDef) : this(name, null as string, paramDef) { }

        public FunctionDefinition(string name) : this(name, null as ParamDefinition) { }

        internal void Render(PowerShellModuleWriter writer)
        {
            writer.WriteFormat(true, "Function {0} {{", this.Name);
            PowerShellModuleWriter content = new PowerShellModuleWriter(1);

            if (!String.IsNullOrWhiteSpace(this.OutputType))
                content.WriteFormat(true, "[OutputType([{0}])]", this.OutputType);
            string dps = this.Params.GetDefaultParameterSetName();
            if (String.IsNullOrWhiteSpace(dps))
                content.WriteLine("[CmdletBinding()]");
            else
                content.WriteFormat(true, "[CmdletBinding(DefaultParameterSetName={0})]", PowerShellModuleWriter.GetQuotedString(dps));

            this.Params.Render(content);

            if (!this.DynamicParamCode.IsEmpty)
            {
                content.WriteLine();
                content.WriteLine("DynamicParams {");
                PowerShellModuleWriter subContent = new PowerShellModuleWriter(1);
                subContent.WriteLine(this.DynamicParamCode.ToString());
                content.WriteLine(subContent);
                content.WriteLine("}");

                if (!this.ProcessCode.IsEmpty)
                {
                    content.WriteLine();
                    content.WriteLine("Process {");
                    subContent = new PowerShellModuleWriter(1);
                    subContent.WriteLine(this.ProcessCode.ToString());
                    content.WriteLine(subContent);
                    content.WriteLine("}");
                }
            } else if (!this.ProcessCode.IsEmpty)
            {
                content.WriteLine();
                content.WriteLine(this.ProcessCode.ToString());
            }

            writer.WriteLine(content.ToString());

            writer.WriteLine("}");
        }
    }
}
