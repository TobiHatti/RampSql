﻿using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class WhereQuery<Connector> : QueryHead, IRampQuery
    {
        internal WhereQuery(RampQueryData data) : base(data) { }

        public WhereQuery<Connector> SectStart
        {
            get
            {
                data.Where.Add(new RampWhereElement(data, null, null, WhereType.SectionStart, LikeWildcard.Unspecified, false));
                return this;
            }
        }

        public Connector Is(IRampColumn columnA, IRampColumn columnB) => WhereCondition(columnA, columnB, WhereType.Is, LikeWildcard.Unspecified, false);
        public Connector Is(IRampColumn column, IRampFunction function) => WhereCondition(column, function, WhereType.Is, LikeWildcard.Unspecified, false);
        public Connector Is(IRampColumn column, IRampQuery query) => WhereCondition(column, query, WhereType.Is, LikeWildcard.Unspecified, false);
        public Connector Is(IRampColumn column, object value) => WhereCondition(column, new RampConstant(value, null), WhereType.Is, LikeWildcard.Unspecified, true);


        public Connector Not(IRampColumn columnA, IRampColumn columnB) => WhereCondition(columnA, columnB, WhereType.IsNot, LikeWildcard.Unspecified, false);
        public Connector Not(IRampColumn column, IRampFunction function) => WhereCondition(column, function, WhereType.IsNot, LikeWildcard.Unspecified, false);
        public Connector Not(IRampColumn column, IRampQuery query) => WhereCondition(column, query, WhereType.IsNot, LikeWildcard.Unspecified, false);
        public Connector Not(IRampColumn column, object value) => WhereCondition(column, new RampConstant(value, null), WhereType.IsNot, LikeWildcard.Unspecified, true);

        public Connector Like(IRampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified) => WhereCondition(column, new RampConstant(value, null), WhereType.IsLike, likeWildcard, true);

        public Connector NotLike(IRampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified) => WhereCondition(column, new RampConstant(value, null), WhereType.IsNotLike, likeWildcard, true);

        //public Connector In(IRampColumn column, params object[] values) { return this; } // Todo: In? How to pass array

        public Connector IsNull(IRampColumn column) => WhereCondition(column, null, WhereType.IsNull, LikeWildcard.NoParameter, false);

        public Connector IsNotNull(IRampColumn column) => WhereCondition(column, null, WhereType.IsNotNull, LikeWildcard.NoParameter, false);

        private Connector WhereCondition(IRampColumn columnA, IRampValue columnB, WhereType type, LikeWildcard wildcard, bool parameterize)
        {
            data.Where.Add(new RampWhereElement(data, columnA, columnB, type, wildcard, parameterize));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
    }
}
