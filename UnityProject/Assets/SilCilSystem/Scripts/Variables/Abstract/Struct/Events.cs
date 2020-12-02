using UnityEngine;

namespace SilCilSystem.Variables
{
	// Unityが用意しているstructバージョン.
	// Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
	// Unity2020では不要だと思う.
	public abstract class GameEventVector2 : GameEvent<Vector2> { }
	public abstract class GameEventVector2Listener : GameEventListener<Vector2> { }

	public abstract class GameEventVector2Int : GameEvent<Vector2Int> { }
	public abstract class GameEventVector2IntListener : GameEventListener<Vector2Int> { }

	public abstract class GameEventVector3 : GameEvent<Vector3> { }
	public abstract class GameEventVector3Listener : GameEventListener<Vector3> { }

	public abstract class GameEventVector3Int : GameEvent<Vector3Int> { }
	public abstract class GameEventVector3IntListener : GameEventListener<Vector3Int> { }

	public abstract class GameEventQuaternion : GameEvent<Quaternion> { }
	public abstract class GameEventQuaternionListener : GameEventListener<Quaternion> { }

	public abstract class GameEventColor : GameEvent<Color> { }
	public abstract class GameEventColorListener : GameEventListener<Color> { }
}
