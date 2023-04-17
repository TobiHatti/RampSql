using RampSql;
using RampTest.Schema;

Console.WriteLine(QueryEngine<Database>.ShowSchema());


string query = new QueryEngine<Database>((DB, Query) => Query.SelectAllFrom(DB.Orders, "ord").Column(DB.History.Permalink, "Hubdiduuuu").LeftJoin(DB.Orders.ID, DB.History.ParentOrderID, "awww")).GetQueryString();
Console.WriteLine(query);
