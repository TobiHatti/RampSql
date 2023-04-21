using RampSql.QueryBuilder;
using RampTest.Schema;


IRampQuery[] queries = new IRampQuery[]
{
    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Pets).Value(1, "asdasdad")).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectFrom((DB2, SubQuery) => SubQuery.SelectAllFrom(DB2.History).Where.Not(DB2.History.UserID, 6), "his").All().Where.Is(DB["his"].History.UserID, 6)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectFrom((DB2, SubQuery) => SubQuery.SelectAllFrom(DB2.History).Where.Not(DB2.History.UserID, 6), "his").All().Query<Database>((DB3, SSubQuery) => SSubQuery.Count(DB3.Items), "aeeee").Where.Is(DB["his"].History.UserID, 6)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.History).Where.In(DB.History.UserID, 4,5,6)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.Union(UnionType.Union, new IRampQuery[]{
            Query.Sub((DB, Query) => Query.SelectAllFrom(DB.History)),
            Query.Sub((DB, Query) => Query.SelectAllFrom(DB.History))
    }).GroupBy(DB.History.ID)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders).Where.Is(DB.Orders.ID, 1)).GetRampQuery(),


    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders).FuncAs(MySqlFunction.CONCAT, "HAAAA", DB.Orders.ID, new RampConstant("asd", null)).Where.Is(DB.Orders.ID, 1)).GetRampQuery(),
};


foreach (IRampQuery query in queries)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine(query.GetQuery());
    Console.ForegroundColor = ConsoleColor.Cyan;
    foreach (object param in query.GetParameters()) Console.WriteLine(param.ToString());

    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine();
}
