using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneratePowershellModule
{
    public class FunctionParameter : IEquatable<FunctionParameter>
    {
        public string Name { get; private set; }

        public string ParameterType { get; private set; }

        public bool Mandatory { get; private set; }

        public FunctionParameter(string name, Type parameterType, bool mandatory) : this(name, (parameterType == null) ? null : parameterType.FullName, mandatory) { }

        public FunctionParameter(string name, string parameterType, bool mandatory)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            if (String.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", "name");

            this.Name = name.Trim();
            this.ParameterType = (String.IsNullOrWhiteSpace(parameterType)) ? "object" : parameterType.Trim();
            this.Mandatory = mandatory;
        }

        public bool Equals(FunctionParameter other)
        {
            return (other != null && (Object.ReferenceEquals(this, other) || (this.Name.Equals(other.Name) && this.ParameterType.Equals(other.ParameterType) &&
                this.Mandatory == other.Mandatory)));
        }

        public override bool Equals(object obj)
        {
            return (obj != null && (Object.ReferenceEquals(this, obj) || (obj is FunctionParameter && this.Equals(obj as FunctionParameter))));
        }

        public override int GetHashCode()
        {
            return (this.Mandatory.ToString() + this.Name + Environment.NewLine + this.ParameterType).GetHashCode();
        }
    }
}
