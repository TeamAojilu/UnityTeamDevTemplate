using SilCilSystem.Variables;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables.Converters
{
    [Variable("ToReadonlyBool", Constants.ConvertMenuPath + "Bool (from Int)", typeof(ReadonlyInt))]
    internal class VariableIntToBool : VariableToBoolBase<int, ReadonlyInt, ReadonlyPropertyInt> { }
}