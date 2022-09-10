using RampSQL.Binder;

namespace RampSQL.IO
{
    public interface IRampSaveable : IRampBindable
    {
        void SaveModel();
    }
}
