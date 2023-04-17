using RampSql;
using RampSql.QueryBuilder;
using RampTest.Schema;

//
//WrapMySql sql = new WrapMySql(data);
//sql.Open();
//sql.Close();


string query = new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders, "ord").Column(DB.History.Permalink, "Hubdiduuuu").LeftJoin(DB.Orders.ID, DB.History.ParentOrderID, "awww").Where.Is(DB.History.DocUserID, 6).GroupBy(DB.History.DocUserID).OrderBy(DB.History.ID, SortDirection.Ascending).Limit(0)).GetQueryString();

Console.WriteLine(query);