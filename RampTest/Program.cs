//
//WrapMySql sql = new WrapMySql(data);
//sql.Open();
//sql.Close();


//IRampQuery query = new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders, "ord").Column(DB.History.Permalink, "Hubdiduuuu").LeftJoin(DB.Orders.ID, DB.History.ParentOrderID, "awww").Where.Is(DB.History.DocUserID, 6).GroupBy(DB.History.DocUserID).OrderBy(DB.History.ID, SortDirection.Ascending).Limit(0)).GetQuery();




//Console.WriteLine(query.GetQuery());


//foreach (object o in query.GetParameters())
//{
//    Console.WriteLine(o.ToString());
//}


// Register all subqueries in the main DB thingy, add possibility to assign a name to be able to back-reference it later

using RampSql.QueryBuilder;
using RampTest.Schema;

//IRampQuery query = new QueryEngine<Database>((DB, Query) => Query.SelectFrom((DB2, Query2) => Query2.Select(DB2.Pets.PetName).Where.Is(DB.Houses.HouseName, DB2.Houses.HouseName), "sub").All().Where.Is(DB.Houses.HouseName, DB["sub"].Houses)).GetRampQuery();
////IRampQuery query = new QueryEngine<Database>((DB, Query) => Query.SelectFrom(DB.Pets).All()).GetRampQuery();


IRampQuery[] queries = new IRampQuery[]
{
    new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders)).GetRampQuery(),

    new QueryEngine<Database>((DB, Query) => Query.SelectFrom((DB2, SubQuery) => SubQuery.SelectAllFrom(DB2.History).Where.Not(DB2.History.UserID, 6), "his").All().Where.Is(DB["his"].History.UserID, 6)).GetRampQuery()
};


foreach (IRampQuery query in queries)
{
    Console.WriteLine(query.GetQuery());
    Console.WriteLine();
    Console.WriteLine();
}

