namespace SilCilSystem.Variables
{
    // Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
    // Unity2020では不要だと思う.
    public abstract class VariableInt : Variable<int> { }
    public abstract class VariableBool : Variable<bool> { }
    public abstract class VariableString : Variable<string> { }
    public abstract class VariableFloat : Variable<float> { }
}
