using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;

namespace GeneratePowershellModule
{
    public class AssemblyReference
    {
        public string PartialName { get; set; }
        public string LongName { get; set; }
        public string File { get; set; }

        public AssemblyReference(XElement e)
        {
            XAttribute attr = e.Attribute("PartialName");
            this.PartialName = (attr == null) ? null : attr.Value;

            attr = e.Attribute("LongName");
            this.LongName = (attr == null) ? null : attr.Value;

            attr = e.Attribute("File");
            this.File = (attr == null) ? null : attr.Value;
        }

        public void LoadAssemblyReference()
        {
            if (!String.IsNullOrWhiteSpace(this.File))
                Assembly.LoadFile(this.File);
            else if (!String.IsNullOrWhiteSpace(this.LongName))
                Assembly.Load(this.LongName);
            else if (!String.IsNullOrWhiteSpace(this.PartialName))
                Assembly.LoadWithPartialName(this.PartialName);
        }

        internal void Render(PowerShellModuleWriter writer)
        {
            if (!String.IsNullOrWhiteSpace(this.File))
                writer.WriteFormat(true, "[Reflection.Assembly]::LoadFile({0}) | Out-Null;", PowerShellModuleWriter.GetQuotedString(this.File));
            else if (!String.IsNullOrWhiteSpace(this.LongName))
                writer.WriteFormat(true, "[Reflection.Assembly]::Load({0}) | Out-Null;", PowerShellModuleWriter.GetQuotedString(this.LongName));
            else if (!String.IsNullOrWhiteSpace(this.PartialName))
                writer.WriteFormat(true, "[Reflection.Assembly]::LoadWithPartialName({0}) | Out-Null;", PowerShellModuleWriter.GetQuotedString(this.PartialName));
        }
    }
}
