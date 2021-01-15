namespace SilCilSystem.Timers
{
    public enum UpdateMode : int // Dictionaryのキーにするのでintキャスト.
    {
        DeltaTime,
        UnscaledDeltaTime,
        FixedDeltaTime,
        FixedUnscaledDeltaTime,
        LateUpdateDeltaTime,
        LateUpdateUnscaledDeltaTime,
    }
}