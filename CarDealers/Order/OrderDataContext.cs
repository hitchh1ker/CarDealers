using CarDealers.Repository.Car.Models;
using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;


namespace CarDealers.Repository.Order.Models
{
    public class OrderDataContext
    {
        private readonly string ConnectionString;

        public OrderDataContext(IOptions<ConnectionStrings> options)
        {
            ConnectionString = options.Value.CarDealers;
        }

        public async Task InsertAsync(Order order)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            await connection.ExecuteAsync(new CommandDefinition("INSERT INTO \"order\" (car_id, employee_id, client_id, date, cost, status_id) VALUES (@car_id, @employee_id, @client_id, @date, @cost, @status_id)",
                new { car_id = order.CarId, employee_id = order.EmployeeId, client_id = order.ClientId, date = DateTime.Now, cost = order.Cost, status_id = 3 }));
        }

        public async Task DeleteByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            await connection.ExecuteAsync(new CommandDefinition("DELETE from \"order\" WHERE id = @id", 
                new { id }));
        }

        public async Task UpdateCostByIdAsync(Order order)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            await connection.ExecuteAsync(new CommandDefinition("Update \"order\" SET cost = @cost WHERE id = @id", 
                new { id = order.Id, cost = order.Cost }));
        }

        public async Task UpdateStatusByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            await connection.ExecuteAsync(new CommandDefinition("Update \"order\" SET status_id = 1 WHERE id = @id", 
                new { id }));
        }

        //public async Task<IEnumerable<Order>> GetAllAsync()
        //{
        //    using var connection = new NpgsqlConnection(ConnectionString);
        //    connection.Open();

        //    var orders = await connection.QueryAsync<Order>("SELECT id, car_id as CarId, employee_id as EmployeeId, client_id as ClientId, date as Date, cost, status_id as StatusId FROM \"order\"");
        //    return orders;
        //}
        public async Task<IEnumerable<Order>> GetPagedAsync(int page, int pageSize)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            var offset = (page - 1) * pageSize;
            var orders = await connection.QueryAsync<Order>(new CommandDefinition($"SELECT id, car_id as CarId, employee_id as EmployeeId, client_id as ClientId, date as Date, cost, status_id as StatusId FROM \"order\" ORDER BY id OFFSET {offset} LIMIT {pageSize}"));
            return orders;
        }

        public async Task<int> GetTotalCountAsync()
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            var totalCount = await connection.ExecuteScalarAsync<int>(new CommandDefinition("SELECT COUNT(*) FROM \"order\""));
            return totalCount;
        }
    }
}
