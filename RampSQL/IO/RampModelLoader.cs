using RampSQL.Binder;

namespace RampSQL.IO
{
    public abstract class RampModelLoader : IRampLoadable
    {
        public abstract RampModelBinder GetBinder();

        public IRampLoadable LoadFromID<T>(T ID)
        {
            return null;
        }
    }
}
