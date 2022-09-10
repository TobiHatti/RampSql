using RampSQL.Binder;
using System;

namespace RampSQL.IO
{
    public abstract class RampModelSaver : IRampSaveable
    {
        public abstract RampModelBinder GetBinder();

        public void SaveModel()
        {
            throw new NotImplementedException();
        }
    }
}
