using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Quorum.OnDemand.Importer.Core.Repository;


namespace TravelAgency.Infrastructure.Repositories
{
    public class TransportRepository
    {
        private readonly IConfiguration _configuration;
        public ImporterDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public virtual string ConnectionString => _configuration["POSTGRESQL_CONNECTION_STRING"];
        public virtual void ConfigureOrm()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeHandler(typeof(Dictionary<string, string>), new JsonTypeMapper());
            SqlMapper.AddTypeHandler(typeof(Dictionary<string, object>), new JsonTypeMapper());
        }
        public virtual void ConfigureDb()
        {
            NpgsqlConnection.GlobalTypeMapper.UseJsonNet();
        }
    }
}
