namespace SilCilSystem.Variables
{
    internal interface IGameEventSetter<T>
    {
        void SetGameEvent(T gameEvent);
    }
}