namespace RampSQL
{
    public interface IRampLoadable : IRampBindable
    {
        IRampLoadable LoadFromID<T>(T ID);
    }
}
