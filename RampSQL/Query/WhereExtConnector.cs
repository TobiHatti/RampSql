﻿namespace RampSQL.Query
{
    public class WhereExtConnector : GroupQuery, IQuerySection
    {
        public WhereQuery<WhereExtConnector> And
        {
            get
            {
                return null;
            }
        }

        public WhereQuery<WhereExtConnector> Or
        {
            get
            {
                return null;
            }
        }

        public WhereExtConnector SectEnd
        {
            get
            {
                return null;
            }
        }
    }
}
