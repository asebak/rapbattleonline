namespace RapBattleAudio.interfaces
{
    internal interface ISilverlightClass
    {
        T Get<T>() where T : new();
    }
}
