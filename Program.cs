using System;

namespace ITProgerDB;

class Program
{
    public async static Task Main()
    {
        Db db = new Db();
        //await db.Create();
        //await db.InsertData(Console.ReadLine(), Console.ReadLine(), Console.ReadLine(), Console.ReadLine());
        await db.GetData();
    }
}