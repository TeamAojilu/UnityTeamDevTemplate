namespace SilCilSystem.Variables
{
	// Unity2019ではGenericsをシリアライズできないので、それ用の型を用意する.
	// Unity2020では不要だと思う.
	public abstract class GameEventInt : GameEvent<int> { }
	public abstract class GameEventIntListener : GameEventListener<int> { }
	public abstract class GameEventBool : GameEvent<bool> { }
	public abstract class GameEventBoolListener : GameEventListener<bool> { }
	public abstract class GameEventFloat : GameEvent<float> { }
	public abstract class GameEventFloatListener : GameEventListener<float> { }
	public abstract class GameEventString : GameEvent<string> { }
	public abstract class GameEventStringListener : GameEventListener<string> { }
}
