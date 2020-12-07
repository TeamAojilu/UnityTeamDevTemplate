using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    [Serializable]
    public class PropertyVector2 : Property<Vector2, VariableVector2>
    {
        public PropertyVector2(Vector2 value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyVector2 : ReadonlyProperty<Vector2, ReadonlyVector2>
    {
        public ReadonlyPropertyVector2(Vector2 value) : base(value) { }
    }

    [Serializable]
    public class PropertyVector2Int : Property<Vector2Int, VariableVector2Int>
    {
        public PropertyVector2Int(Vector2Int value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyVector2Int : ReadonlyProperty<Vector2Int, ReadonlyVector2Int>
    {
        public ReadonlyPropertyVector2Int(Vector2Int value) : base(value) { }
    }

    [Serializable]
    public class PropertyVector3 : Property<Vector3, VariableVector3>
    {
        public PropertyVector3(Vector3 value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyVector3 : ReadonlyProperty<Vector3, ReadonlyVector3>
    {
        public ReadonlyPropertyVector3(Vector3 value) : base(value) { }
    }

    [Serializable]
    public class PropertyVector3Int : Property<Vector3Int, VariableVector3Int>
    {
        public PropertyVector3Int(Vector3Int value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyVector3Int : ReadonlyProperty<Vector3Int, ReadonlyVector3Int>
    {
        public ReadonlyPropertyVector3Int(Vector3Int value) : base(value) { }
    }

    [Serializable]
    public class PropertyColor : Property<Color, VariableColor>
    {
        public PropertyColor(Color value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyColor : ReadonlyProperty<Color, ReadonlyColor>
    {
        public ReadonlyPropertyColor(Color value) : base(value) { }
    }

    [Serializable]
    public class PropertyQuaternion : Property<Quaternion, VariableQuaternion>
    {
        public PropertyQuaternion(Quaternion value) : base(value) { }
    }
    [Serializable]
    public class ReadonlyPropertyQuaternion : ReadonlyProperty<Quaternion, ReadonlyQuaternion>
    {
        public ReadonlyPropertyQuaternion(Quaternion value) : base(value) { }
    }
}
