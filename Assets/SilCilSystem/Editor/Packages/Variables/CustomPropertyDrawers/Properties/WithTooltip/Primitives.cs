using SilCilSystem.Variables;
using UnityEditor;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(PropertyBool), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyBool), true)]
    internal class PropertyBoolWithToolTipDrawer : PropertyDrawerWithTooltipBase<bool> { }

    [CustomPropertyDrawer(typeof(PropertyInt), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyInt), true)]
    internal class PropertyIntWithToolTipDrawer : PropertyDrawerWithTooltipBase<int> { }

    [CustomPropertyDrawer(typeof(PropertyFloat), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyFloat), true)]
    internal class PropertyFloatWithToolTipDrawer : PropertyDrawerWithTooltipBase<float> { }

    [CustomPropertyDrawer(typeof(PropertyString), true)]
    [CustomPropertyDrawer(typeof(ReadonlyPropertyString), true)]
    internal class PropertyStringWithToolTipDrawer : PropertyDrawerWithTooltipBase<string> { }
}