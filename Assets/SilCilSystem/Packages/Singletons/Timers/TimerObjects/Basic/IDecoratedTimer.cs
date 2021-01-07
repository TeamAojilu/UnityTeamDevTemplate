namespace SilCilSystem.Timers
{
    internal interface IDecoratedTimer : ITimerObject
    {
        void SetTimer(ITimerObject timer);
    }
}