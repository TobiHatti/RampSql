﻿using RampSql.QueryBuilder;
using RampSql.QuerySections;

namespace RampSql.QueryConnectors
{
    public class WhereConnector : IRampQuery
    {
        protected RampQueryData data;
        internal WhereConnector(RampQueryData data) { this.data = data; }

        public WhereQuery<WhereConnector> And
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereQuery<WhereConnector> Or
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }

        public WhereConnector SectEnd
        {
            get
            {
                return this;
            }
        }
    }
}
