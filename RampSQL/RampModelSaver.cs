using System;

namespace RampSQL
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
