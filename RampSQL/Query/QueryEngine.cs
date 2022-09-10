using RampSQL.Schema;

namespace RampSQL.Query
{
    public class QueryEngine
    {

        public SelectQuery Select
        {
            get
            {
                return null;
            }
        }

        public InsertKeyValueQuery InsertInto(RampTable table)
        {
            return null;
        }

        public UpdateKeyValueQuery Update(RampTable table)
        {
            return null;
        }

        public DeleteQuery DeleteFrom(RampTable table)
        {
            return null;
        }

        public UnionQuery Union
        {
            get
            {
                return null;
            }
        }
    }
}
