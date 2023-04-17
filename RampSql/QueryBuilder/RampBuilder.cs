using RampSql.QueryBuilder.QueryRenderer;
using System.Runtime.InteropServices;

namespace RampSql.QueryBuilder
{
    public class RampBuilder
    {
        private RampQueryData data;
        private RampRenderEngine render;

        public RampBuilder(RampQueryData data)
        {
            this.data = data;
            render = new RampRenderEngine();
        }

        private void Preprocess()
        {
            if (data.SelectAll)
            {

                foreach (var item in data.columnCollection.Except(data.SelectValues))
                {
                    if (item.HasAlias) data.SelectValues.Add(item);

                }
                Console.WriteLine();
            }
        }

        public string Build()
        {
            Preprocess();


            switch (data.OperationType)
            {
                case OperationType.Select:
                    render.Instruction("SELECT");
                    SelectQuery();
                    JoinQuery();
                    WhereQuery();
                    GroupQuery();
                    HavingQuery();
                    OrderQuery();
                    LimitQuery();
                    break;
                case OperationType.Insert:
                    render.Instruction("INSERT INTO").Target(data.Target);
                    InsertValuesQuery();
                    InsertResultQuery();
                    break;
                case OperationType.Update:
                    render.Instruction("UPDATE").Target(data.Target).Instruction("SET");
                    UpdateValuesQuery();
                    WhereQuery();
                    break;
                case OperationType.Delete:
                    render.Instruction("DELETE FROM").Target(data.Target);
                    WhereQuery();
                    break;
                //case OperationType.Union:
                //    query.Append(UnionQuery());
                //    query.Append(WhereQuery());
                //    query.Append(GroupQuery());
                //    query.Append(HavingQuery());
                //    query.Append(OrderQuery());
                //    query.Append(LimitQuery());
                //    break;
                case OperationType.Search:
                    render.Target(data.Target);
                    JoinQuery();
                    WhereQuery();
                    GroupQuery();
                    HavingQuery();
                    OrderQuery();
                    LimitQuery();
                    break;
                case OperationType.Upsert:
                    throw new NotImplementedException("Upsert statements are not supported yet");
            }

            return render.Render();
        }

        private void SelectQuery()
        {
            bool first = true;
            foreach (IRampValue val in CollectionsMarshal.AsSpan(data.SelectValues))
            {
                if (!first) render.Instruction(",");
                render.Value(val, RampRFormat.AliasDeclaring);
                first = false;
            }
            render.Instruction("FROM").Target(data.Target);
        }

        private void JoinQuery()
        {
            foreach (RampJoinElement join in CollectionsMarshal.AsSpan(data.Join))
            {
                switch (join.JoinType)
                {
                    case TableJoinType.Left: render.Instruction("LEFT JOIN"); break;
                    case TableJoinType.Right: render.Instruction("RIGHT JOIN"); break;
                    case TableJoinType.Inner: render.Instruction("INNER JOIN"); break;
                    case TableJoinType.FullOuter: render.Instruction("FULL OUTER JOIN"); break;
                }
                render
                    .Table(join.NewColumn.ParentTable, RampRFormat.AliasDeclaring, join.Alias)
                    .Instruction("ON")
                    .Column(join.ExistingColumn, RampRFormat.RealName)
                    .Instruction("=")
                    .Column(join.NewColumn, RampRFormat.RealName)
                ;
            }
        }

        private void WhereQuery()
        {

        }

        private void GroupQuery()
        {

        }

        private void HavingQuery()
        {

        }

        private void OrderQuery()
        {

        }

        private void LimitQuery()
        {

        }

        private void InsertValuesQuery()
        {

        }

        private void InsertResultQuery()
        {

        }

        private void UpdateValuesQuery()
        {

        }
    }
}
