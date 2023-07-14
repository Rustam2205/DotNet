using System;

namespace ITProgerDB;

class Program
{
    public async static Task Main()
    {
        Db db = new Db();
        await db.InsertData();
    }
}