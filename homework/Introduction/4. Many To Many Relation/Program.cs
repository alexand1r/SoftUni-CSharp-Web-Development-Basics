namespace _4._Many_To_Many_Relation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new MyDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
