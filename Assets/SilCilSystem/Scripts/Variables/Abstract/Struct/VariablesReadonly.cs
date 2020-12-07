using UnityEngine;

namespace SilCilSystem.Variables
{
    // Unityが用意しているstructバージョン.
    // Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
    // Unity2020では不要だと思う.
    public abstract class ReadonlyVector2 : ReadonlyVariable<Vector2> { }
    public abstract class ReadonlyVector2Int : ReadonlyVariable<Vector2Int> { }
    public abstract class ReadonlyVector3 : ReadonlyVariable<Vector3> { }
    public abstract class ReadonlyVector3Int : ReadonlyVariable<Vector3Int> { }
    public abstract class ReadonlyQuaternion : ReadonlyVariable<Quaternion> { }
    public abstract class ReadonlyColor : ReadonlyVariable<Color> { }
}