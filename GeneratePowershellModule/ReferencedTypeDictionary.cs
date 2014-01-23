using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace GeneratePowershellModule
{
    public class ReferencedTypeDictionary : IDictionary<Type, TypeReference>
    {
        private Dictionary<Type, TypeReference> _innerDictionary = new Dictionary<Type, TypeReference>();

        public ReferencedTypeDictionary() : base() { }

        public ReferencedTypeDictionary(XElement root)
        {
            root = root.Element("Types");
            if (root == null)
                return;

            foreach (XElement element in root.Elements("Type"))
            {
                XAttribute attribute = element.Attribute("Name");
                if (attribute == null || String.IsNullOrWhiteSpace(attribute.Value))
                    continue;
                string name = attribute.Value;
                attribute = element.Attribute("BaseType");
                this.Upsert(new TypeReference(name, (attribute == null) ? null : attribute.Value));
            }
        }

        public bool IsReferenced(TypeReference typeReference)
        {
            if (typeReference == null)
                throw new ArgumentNullException("typeReference");

            return this.IsReferenced(typeReference.ReferencedType);
        }

        public bool IsReferenced(Type type)
        {
            return this.Values.Any(v => v.ReferencedBaseType != null && v.ReferencedBaseType.ReferencedType.Equals(type));
        }

        public void Upsert(TypeReference value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            this[value.ReferencedType] = value;
        }

        public void Add(Type key, TypeReference value)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            if (value == null)
                throw new ArgumentNullException("value");

            if (this._innerDictionary.ContainsKey(key))
                throw new IndexOutOfRangeException("Key already exists.");

            if (!key.Equals(value.ReferencedType))
                throw new ArgumentException("Key must match referenced type", "key");

            this[key] = value;
        }

        public bool ContainsKey(Type key)
        {
            return this._innerDictionary.ContainsKey(key);
        }

        public ICollection<Type> Keys
        {
            get { return this._innerDictionary.Keys; }
        }

        public string[] KeyNames
        {
            get { return this.Keys.Select(k => k.FullName).ToArray(); }
        }

        public bool Remove(Type key)
        {
            return this._innerDictionary.Remove(key);
        }

        public bool TryGetValue(Type key, out TypeReference value)
        {
            return this._innerDictionary.TryGetValue(key, out value);
        }

        public ICollection<TypeReference> Values
        {
            get { return this._innerDictionary.Values; }
        }

        public TypeReference this[Type key]
        {
            get
            {
                return this._innerDictionary[key];
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                if (value == null)
                    throw new ArgumentNullException("value");

                if (!key.Equals(value.ReferencedType))
                    throw new ArgumentException("Key must match referenced type", "key");

                if (this._innerDictionary.ContainsKey(value.ReferencedType))
                    this._innerDictionary[key] = value;
                else
                    this._innerDictionary.Add(key, value);

                if (value.ReferencedBaseType != null)
                    this[value.ReferencedBaseType.ReferencedType] = value.ReferencedBaseType;
            }
        }

        public TypeReference this[string key]
        {
            get
            {
                Type t = this.Keys.FirstOrDefault(k => k.FullName == key);
                if (t == null)
                    return null;

                return this._innerDictionary[t];
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key");

                if (value == null)
                    throw new ArgumentNullException("value");

                Type t = t = value.ReferencedType;
                if (t.FullName != key)
                    throw new ArgumentException("Key must match referenced type", "key");

                if (this._innerDictionary.ContainsKey(value.ReferencedType))
                    this._innerDictionary[t] = value;
                else
                    this._innerDictionary.Add(t, value);

                if (value.ReferencedBaseType != null)
                    this[value.ReferencedBaseType.ReferencedType] = value.ReferencedBaseType;
            }
        }

        public void Add(KeyValuePair<Type, TypeReference> item)
        {
            if (item.Key == null)
                throw new ArgumentException("item.Key cannot be null");

            if (item.Value == null)
                throw new ArgumentNullException("item.Value cannot be null");

            if (this._innerDictionary.ContainsKey(item.Key))
                throw new IndexOutOfRangeException("Key already exists.");

            if (!item.Key.Equals(item.Value.ReferencedType))
                throw new ArgumentException("Key must match referenced type", "key");

            this[item.Key] = item.Value;
        }

        public void Clear()
        {
            this._innerDictionary.Clear();
        }

        public bool Contains(KeyValuePair<Type, TypeReference> item)
        {
            return (this._innerDictionary.ContainsKey(item.Key) && this._innerDictionary[item.Key].Equals(item.Value));
        }

        public void CopyTo(KeyValuePair<Type, TypeReference>[] array, int arrayIndex)
        {
            this._innerDictionary.ToArray().CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._innerDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<Type, TypeReference> item)
        {
            if (item.Key != null && item.Value != null && item.Key.Equals(item.Value.ReferencedType))
                return this._innerDictionary.Remove(item.Key);

            return false;
        }

        public IEnumerator<KeyValuePair<Type, TypeReference>> GetEnumerator()
        {
            return this._innerDictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this._innerDictionary.GetEnumerator();
        }

        public void Render(PowerShellModuleWriter writer)
        {
            Dictionary<Type, TypeReference> allTypes = new Dictionary<Type, TypeReference>();
            foreach (Type k in this.Keys)
                allTypes.Add(k, this[k]);

            Collection<FunctionDefinition> functions = new Collection<FunctionDefinition>();
            do
            {
                Type[] typesToRender = allTypes.Keys.Where(t => this[t].ReferencedBaseType == null || !allTypes.ContainsKey(this[t].ReferencedBaseType.ReferencedType)).ToArray();

                foreach (Type t in typesToRender)
                {
                    foreach (FunctionDefinition f in this[t].GetFunctions(this.Values.Count(v => v.ReferencedBaseType != null &&
                            v.ReferencedBaseType.ReferencedType.Equals(t)) > 0))
                        functions.Add(f);

                    allTypes.Remove(t);
                }
            } while (allTypes.Count > 0);

            for (int i = 0; i < functions.Count; i++)
            {
                if (i > 0)
                    writer.WriteLine();

                functions[i].Render(writer);
            }
        }
    }
}
