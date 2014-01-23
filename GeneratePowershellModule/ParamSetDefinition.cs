using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class ParamSetDefinition : Collection<FunctionParameter>
    {
        public string SetName { get; set; }

        public ParamSetDefinition() : base() { }

        public ParamSetDefinition(IList<FunctionParameter> list) : base(list) { }
    }
}
