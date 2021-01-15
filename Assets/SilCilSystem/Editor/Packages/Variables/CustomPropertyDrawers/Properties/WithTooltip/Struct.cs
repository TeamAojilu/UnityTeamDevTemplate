using UnityEditor;
using UnityEngine;
using SilCilSystem.Variables;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(PropertyVector2), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector2), true)]
    internal class PropertyVector2WithToolTipDrawer : PropertyDrawerWithTooltipBase<Vector2> { }

    [CustomPropertyDrawer(typeof(PropertyVector2Int), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector2Int), true)]
    internal class PropertyVector2IntWithToolTipDrawer : PropertyDrawerWithTooltipBase<Vector2Int> { }

    [CustomPropertyDrawer(typeof(PropertyVector3), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector3), true)]
    internal class PropertyVector3WithToolTipDrawer : PropertyDrawerWithTooltipBase<Vector3> { }

    [CustomPropertyDrawer(typeof(PropertyVector3Int), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyVector3Int), true)]
    internal class PropertyVector3IntWithToolTipDrawer : PropertyDrawerWithTooltipBase<Vector3Int> { }

    [CustomPropertyDrawer(typeof(PropertyColor), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyColor), true)]
    internal class PropertyColorWithToolTipDrawer : PropertyDrawerWithTooltipBase<Color> { }

    [CustomPropertyDrawer(typeof(PropertyQuaternion), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyQuaternion), true)]
    internal class PropertyQuaternionWithToolTipDrawer : PropertyDrawerWithTooltipBase<Quaternion> { }
}