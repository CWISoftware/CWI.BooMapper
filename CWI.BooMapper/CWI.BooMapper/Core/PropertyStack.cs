using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace CWI.BooMapper.Core
{
    internal class PropertyStack
    {
        private readonly Stack<PropertyInfo> stack;

        public PropertyStack()
        {
            stack = new Stack<PropertyInfo>();
        }

        public int NestedLevel
        {
            get
            {
                return stack.Count;
            }
        }

        public PropertyStack Push(PropertyInfo property)
        {
            stack.Push(property);
            return this;
        }

        public PropertyInfo Pop()
        {
            return stack.Pop();
        }

        public string ToStackString()
        {
            return string.Join(".", stack.Reverse().Select(p => p.Name));
        }

        public override string ToString()
        {
            return ToStackString();
        }
    }
}
