using RampSql;
using RampSql.QueryBuilder;
using RampTest.Schema;

//Console.WriteLine(QueryEngine<Database>.ShowSchema());

IRampQuery query = new QueryEngine<Database>((DB, Query) =>
{
    Console.WriteLine(DB.Residents.Name);
    Console.WriteLine(DB.Pets.PetName);
    return null;
}).GetQuery();

