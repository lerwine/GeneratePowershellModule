using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GeneratePowershellModule
{
    public class ModuleGenerationConfig
    {
        public string Name { get; set; }

        public AssemblyReferenceCollection AssemblyReferences { get; set; }

        public ReferencedTypeDictionary TypeReferences { get; set; }

        public ModuleGenerationConfig(string path)
        {
            XDocument document;
            try
            {
                document = XDocument.Load(path);
            }
            catch (Exception exc)
            {
                throw new Exception("Error reading from XML file", exc);
            }

            XElement root = document.Element("GenerateModule");
            if (root == null)
                return;

            XAttribute attr = root.Attribute("Name");

            this.Name = (attr == null) ? null : attr.Value;

            this.AssemblyReferences = new AssemblyReferenceCollection(root);
            this.TypeReferences = new ReferencedTypeDictionary(root);
        }

        public void GeneratePSModule(string path)
        {
            if (this.TypeReferences.Count == 0)
                throw new Exception("No types to process");

            if (this.AssemblyReferences != null)
                this.AssemblyReferences.LoadAssemblyReferences();

            PowerShellModuleWriter writer = new PowerShellModuleWriter();

            if (this.AssemblyReferences != null)
                this.AssemblyReferences.Render(writer);

            writer.WriteLine();

            if (this.TypeReferences != null)
                this.TypeReferences.Render(writer);

            System.IO.File.WriteAllText(System.IO.Path.Combine(path, this.Name + ".psm1"), writer.ToString());
        }
    }
}
