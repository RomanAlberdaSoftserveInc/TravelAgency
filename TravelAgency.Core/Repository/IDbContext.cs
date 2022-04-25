namespace TravelAgency.Core.Repository
{
    public interface IDbContext
    {
        string ConnectionString { get; }
        void ConfigureDb();
        void ConfigureOrm();
    }
}
