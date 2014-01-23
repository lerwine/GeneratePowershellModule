using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace GeneratePowershellModule
{
    public class AssemblyReferenceCollection : Collection<AssemblyReference>
    {
        public AssemblyReferenceCollection(XElement root)
            : base(AssemblyReferenceCollection.Load(root))
        {
        }

        private static IList<AssemblyReference> Load(XElement root)
        {
            root = root.Element("Assemblies");
            if (root == null)
                return new AssemblyReference[0];

            return root.Elements("Assembly").Select(e => new AssemblyReference(e)).Where(a => a != null).ToList();
        }

        internal void LoadAssemblyReferences()
        {
            foreach (AssemblyReference assemblyRef in this)
                assemblyRef.LoadAssemblyReference();
        }

        internal void Render(PowerShellModuleWriter writer)
        {
            PowerShellModuleWriter content = new PowerShellModuleWriter();
            foreach (AssemblyReference assemblyRef in this)
                assemblyRef.Render(content);
            string s = content.ToString().Trim();
            if (s.Length == 0)
                return;

            writer.WriteLine("#region Import Required Assemblies");
            writer.WriteLine();
            writer.WriteLine(s);
            writer.WriteLine();
            writer.WriteLine("#endregion");
        }
    }
}
