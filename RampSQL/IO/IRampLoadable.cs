using RampSQL.Binder;

namespace RampSQL.IO
{
    public interface IRampLoadable : IRampBindable
    {
        IRampLoadable LoadFromID<T>(T ID);
    }
}
