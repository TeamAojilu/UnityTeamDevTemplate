using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Variables
{
    // Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
    // Unity2020では不要だと思う.
    public abstract class VariableInt : Variable<int> { }
    public abstract class VariableBool : Variable<bool> { }
    public abstract class VariableString : Variable<string> { }
    public abstract class VariableFloat : Variable<float> { }

    public abstract class ReadonlyInt : ReadonlyVariable<int> { }
    public abstract class ReadonlyBool : ReadonlyVariable<bool> { }
    public abstract class ReadonlyString : ReadonlyVariable<string> { }
    public abstract class ReadonlyFloat : ReadonlyVariable<float> { }
}
