using System;

namespace SilCilSystem.Variables
{
    [Serializable]
    public class PropertyInt : Property<int, VariableInt>
    {
        public PropertyInt(int value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyInt : ReadonlyProperty<int, ReadonlyInt>
    {
        public ReadonlyPropertyInt(int value) : base(value) { }
    }

    [Serializable]
    public class PropertyFloat : Property<float, VariableFloat>
    {
        public PropertyFloat(float value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyFloat : ReadonlyProperty<float, ReadonlyFloat>
    {
        public ReadonlyPropertyFloat(float value) : base(value) { }
    }

    [Serializable]
    public class PropertyBool : Property<bool, VariableBool>
    {
        public PropertyBool(bool value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyBool : ReadonlyProperty<bool, ReadonlyBool>
    {
        public ReadonlyPropertyBool(bool value) : base(value) { }
    }

    [Serializable]
    public class PropertyString : Property<string, VariableString>
    {
        public PropertyString(string value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyString : ReadonlyProperty<string, ReadonlyString>
    {
        public ReadonlyPropertyString(string value) : base(value) { }
    }
}
