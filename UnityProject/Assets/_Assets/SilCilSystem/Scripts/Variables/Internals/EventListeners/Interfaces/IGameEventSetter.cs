namespace SilCilSystem.Variables
{
    public interface IGameEventSetter<T>
    {
        void SetGameEvent(T gameEvent);
    }
}