using System;

namespace SimulationEngine.Components
{
    public class ComponentName
    {
        public string Name { get; set; }

        public ComponentName(string name)
        {
            Name = name;
        }



        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            var componentName = obj as ComponentName;
            return componentName != null && string.Equals(Name, componentName.Name);
            
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
