using System;
using System.Collections.Generic;

namespace RampSQL
{
    public abstract class RampSchema
    {
        public RampSchema(RampDBInterface dbType)
        {

        }

        public RampTable this[string tableName]
        {
            get
            {
                return null;
            }
        }

        public RampBuilder Build
        {
            get => new RampBuilder();
        }
    }
}
