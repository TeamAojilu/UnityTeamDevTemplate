using SilCilSystem.Variables;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToReadonlyBool", Constants.ConvertMenuPath + "Bool (from Float)", typeof(ReadonlyFloat))]
    internal class VariableFloatToBool : VariableToBoolBase<float, ReadonlyFloat, ReadonlyPropertyFloat> { }
}