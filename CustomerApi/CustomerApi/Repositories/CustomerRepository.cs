using CustomerApi.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace CustomerApi.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<int> CreateAsync(CustomerDto customer)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    INSERT INTO 
                        public.customer
                    (
                        active, 
						title, 
						forename, 
						surname, 
						email, 
						mobile
                    )    
                    VALUES
                    (
                        true,
						@title, 
						@forename, 
						@surname, 
						@email, 
						@mobile
                    ) 
					RETURNING id";

                return await connection.QuerySingleAsync<int>(sql, customer);
            }
        }

        public async Task DeleteById(int id)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    DELETE FROM
                        public.customer
                    WHERE
                        id = @id";

                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            using (var connection = Connection)
            {
                var sql = @"
					SELECT 
						id, 
						active, 
						title, 
						forename, 
						surname, 
						email, 
						mobile
					FROM 
						public.customer;
					";

                var customers = await connection.QueryAsync<CustomerDto>(sql);
                return customers;
            }
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync(bool active)
        {
            using (var connection = Connection)
            {
                var sql = @"
					SELECT 
						id, 
						active, 
						title, 
						forename, 
						surname, 
						email, 
						mobile
					FROM 
						public.customer
                    WHERE
                        @active IS NULL OR active = @active
					";

                var customers = await connection.QueryAsync<CustomerDto>(sql, new { active });
                return customers;
            }
        }

        public async Task<CustomerDto> GetById(int id)
        {
            using (var connection = Connection)
            {
                var sql = @"
					SELECT 
						id, 
						active, 
						title, 
						forename, 
						surname, 
						email, 
						mobile
					FROM 
						public.customer
					WHERE
						id = @id
					LIMIT 1
					";

                var customers = await connection.QuerySingleAsync<CustomerDto>(sql, new { id });
                return customers;
            }
        }

        public async Task UpdateAsync(CustomerDto customer)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    UPDATE 
                        public.customer
                    SET
                        active = COALESCE(@active, active), 
						title = COALESCE(@title, title),
						forename = COALESCE(@forename, forename),
						surname = COALESCE(@surname, surname),
						email = COALESCE(@email, email),
						mobile = COALESCE(@mobile, mobile)
                    WHERE
                        id = @id;";

                await connection.ExecuteAsync(sql, customer);
            }
        }
    }
}
