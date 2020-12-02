using UnityEngine;

namespace SilCilSystem.Variables
{
    // Unityが用意しているstructバージョン.
    // Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
    // Unity2020では不要だと思う.
    public abstract class VariableVector2 : Variable<Vector2> { }
    public abstract class VariableVector2Int : Variable<Vector2Int> { }
    public abstract class VariableVector3 : Variable<Vector3> { }
    public abstract class VariableVector3Int : Variable<Vector3Int> { }
    public abstract class VariableQuaternion : Variable<Quaternion> { }
    public abstract class VariableColor : Variable<Color> { }
}