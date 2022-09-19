namespace SpendingSite.Db
{
    public class SpendKindDao
    {
        public List<(int SpendKindId, string Name)> GetSpendKinds()
        {
            using (var db = new AppDbContext())
            {
                return db.SpendKinds
                    .ToList()
                    .OrderBy(x => x.SpendKindId)
                    .Select(x => (x.SpendKindId, x.Name))
                    .ToList();
            }
        }
    }
}
