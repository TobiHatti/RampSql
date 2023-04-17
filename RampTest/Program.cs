using RampSql.QueryBuilder;
using RampTest.Schema;

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

IRampQuery query = new QueryEngine<Database>((DB, Query) => Query.SelectFrom((DB2, Query2) => Query2.Select(DB2.Pets.PetName).Where.Is(DB.Houses.HouseName, DB2.Houses.HouseName)).All().Where.Is(DB.Houses.HouseName, DB["1"].Houses.HouseName)).GetRampQuery();
//IRampQuery query = new QueryEngine<Database>((DB, Query) => Query.SelectFrom(DB.Pets).All()).GetRampQuery();

Console.WriteLine(query.GetQuery());



