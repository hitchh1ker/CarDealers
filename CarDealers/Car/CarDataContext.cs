using Npgsql;
using Dapper;
using Microsoft.Extensions.Options;

namespace CarDealers.Repository.Car.Models
{
    public class CarDataContext
    {
        private readonly string ConnectionString;

        public CarDataContext(IOptions<ConnectionStrings> options)
        {
            ConnectionString = options.Value.CarDealers;
        }

        //public async Task<IEnumerable<Car>> GetAllAsync()
        //{
        //    using var connection = new NpgsqlConnection(ConnectionString);
        //    connection.Open();

        //    var cars = await connection.QueryAsync<Car>(new CommandDefinition("SELECT id, brand_id as BrandId, model_id as ModelId, release_date as ReleaseDate, miliage, color_id as ColorId, cost, cardealership_id as CardealershipId FROM car"));
        //    return cars;
        //}

        public async Task<IEnumerable<Car>> GetPagedAsync(int page, int pageSize)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            var offset = (page - 1) * pageSize;
            var cars = await connection.QueryAsync<Car>(new CommandDefinition($"SELECT id, brand_id as BrandId, model_id as ModelId, release_date as ReleaseDate, miliage, color_id as ColorId, cost, cardealership_id as CardealershipId FROM car ORDER BY id OFFSET {offset} LIMIT {pageSize}"));
            return cars;
        }

        public async Task<int> GetTotalCountAsync()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            var totalCount = await connection.ExecuteScalarAsync<int>(new CommandDefinition("SELECT COUNT(*) FROM car"));
            return totalCount;
        }
    }
}