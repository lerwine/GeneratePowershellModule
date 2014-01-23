using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class TypeReference
    {
        public Type ReferencedType { get; private set; }

        public TypeReference ReferencedBaseType { get; private set; }

        private static Type ResolveType(string typeFullName)
        {
            if (typeFullName == null)
                throw new ArgumentNullException("typeFullName");

            if (String.IsNullOrWhiteSpace(typeFullName))
                throw new ArgumentException("Value cannot be empty", "typeFullName");

            Type result = System.Type.GetType(typeFullName, false);

            if (result == null)
            {
                var types = from assembly in System.AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in assembly.GetTypes()
                            where assemblyType.FullName == typeFullName
                            select assemblyType;

                result = types.FirstOrDefault();
            }

            if (result == null)
                throw new Exception(String.Format("Type {0} not found.", typeFullName));

            return result;
        }

        public static Type GetNextViableBaseType(Type type, Type stopAt)
        {
            if (stopAt == null)
                stopAt = typeof(object);

            if (type == null || type == typeof(object) || type.BaseType == null || type.BaseType.Equals(typeof(object)) || type.Equals(stopAt))
                return null;

            Type result = type.BaseType;
            if (result == null || result.Equals(typeof(object)) || result.BaseType == null || result.BaseType.Equals(typeof(object)) || result.Equals(stopAt))
                return result;

            PropertyInfo[] viableProperties = TypeReference.GetViableProperties(type, false);
            if (viableProperties.Length > 0)
                return result;

            return GetNextViableBaseType(result, stopAt);
        }

        public static PropertyInfo[] GetViableProperties(Type type, bool includeBaseProperties)
        {
            if (type == null)
                return new PropertyInfo[0];

            if (!includeBaseProperties)
                return type.GetProperties().Where(p => p.CanWrite && p.GetSetMethod() != null).ToArray();

            PropertyInfo[] baseProperties = (type.Equals(typeof(object))) ? new PropertyInfo[0] : type.BaseType.GetProperties();
            return type.GetProperties().Where(p => p.CanWrite && p.GetSetMethod() != null && p.DeclaringType.Equals(type) && !baseProperties.Any(b => b.Name == p.Name)).ToArray();
        }

        public TypeReference(Type type, Type stopAtType)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.ReferencedType = type;
            Type baseType = GetNextViableBaseType(type, stopAtType);
            if (baseType == null)
                this.ReferencedBaseType = null;
            else
                this.ReferencedBaseType = new TypeReference(baseType, stopAtType);
        }

        public TypeReference(string typeFullName, Type stopAtType) : this(TypeReference.ResolveType(typeFullName), stopAtType) { }

        public TypeReference(Type type, string stopAtTypeFullName) : this(type, (stopAtTypeFullName == null) ? null : TypeReference.ResolveType(stopAtTypeFullName)) { }

        public TypeReference(string typeFullName, string stopAtTypeFullName) : this(TypeReference.ResolveType(typeFullName), 
            (stopAtTypeFullName == null) ? null : TypeReference.ResolveType(stopAtTypeFullName))
        {
        }

        internal FunctionDefinition[] GetFunctions(bool isReferenced)
        {
            FunctionParameter[] optionalParameters = TypeReference.GetViableProperties(this.ReferencedType, this.ReferencedBaseType == null)
                .Select(p => new FunctionParameter(p.Name, p.PropertyType, false)).ToArray();

            ParamDefinition constructors;

            if (this.ReferencedType.IsInterface || this.ReferencedType.IsAbstract)
                constructors = new ParamDefinition();
            else
            {
                constructors = new ParamDefinition(
                this.ReferencedType.GetConstructors()
                    // Filter to standard public constructors defined on the referenced type
                .Where(c => c.CallingConvention.HasFlag(CallingConventions.Standard) && c.DeclaringType.Equals(this.ReferencedType) && c.IsPublic)
                    // Get constructor parameters
                .Select(c => c.GetParameters())
                    // Filter constructor parameter sets to those which are only 'in' parameters and not for deserialization
                .Where(ps => ps.All(p => p.IsIn) && ps.Length != 2 || !ps[0].ParameterType.Equals(typeof(SerializationInfo)) ||
                    !ps[1].ParameterType.Equals(typeof(StreamingContext)))
                    // Create parameter set definitions from collections of parameters
                .Select(ps => new ParamSetDefinition(ps.Select(p => new FunctionParameter(p.Name, p.ParameterType, true)).ToList()))
                .ToList());

                if (constructors.Count == 0 && this.ReferencedType.GetConstructors().Count(c => c.CallingConvention.HasFlag(CallingConventions.Standard) && c.IsPublic &&
                        c.GetParameters().Length == 0) > 0)
                    constructors.Add(new ParamSetDefinition());
            }

            if (!this.ReferencedType.IsClass && constructors.Count > 0)
            {
                for (int i = 0; i < constructors.Count; i++)
                {
                    if (constructors[i].Count == 0)
                    {
                        constructors.RemoveAt(i);
                        break;
                    }
                }
            }

            Collection<FunctionDefinition> results = new Collection<FunctionDefinition>();

            if (this.ReferencedType.IsAbstract || this.ReferencedType.IsInterface || isReferenced || constructors.Count == 0)
            {
                // TODO: Add "Set-{0}Properties
            }

            if (constructors.Count == 0)
                return results.ToArray();

            foreach (ParamSetDefinition paramDef in constructors)
            {
                foreach (FunctionParameter op in optionalParameters.Where(p => !paramDef.Any(m => String.Compare(m.Name, p.Name, true) == 0)))
                    paramDef.Add(new FunctionParameter(op.Name, op.ParameterType, false));
            }

            results.Add(new FunctionDefinition(String.Format("New-{0}", this.ReferencedType.Name), this.ReferencedType, constructors));

            return results.ToArray();
        }
    }
}
