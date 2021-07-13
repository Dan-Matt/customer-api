using CustomerApi.Models;
using CustomerApi.Repositories.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Repositories
{
    public class AddressRepository : BaseRepository, IAddressRepository
    {
        public AddressRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task ClearMainForCustomerIdAsync(int customerId)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    UPDATE 
                        public.address
                    SET
                        main = false
                    WHERE
                        customer_id = @customerId;";

                await connection.ExecuteAsync(sql, new { customerId });
            }
        }

        public async Task<int> CreateAsync(AddressDto address, int customerId)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    INSERT INTO 
                        public.address
                    (
                        line1, 
                        line2, 
                        town, 
                        county, 
                        postcode, 
                        country, 
                        main, 
                        customer_id
                    )
                    VALUES 
                    (
                        @line1, 
                        @line2, 
                        @town, 
                        @county, 
                        @postcode, 
                        @country, 
                        @main, 
                        @customerId
                    )
					RETURNING id";

                return await connection.QuerySingleAsync<int>(sql, new
                {
                    address.Line1,
                    address.Line2,
                    address.Town,
                    address.County,
                    address.Postcode,
                    address.Country,
                    address.Main,
                    customerId
                });
            }
        }

        public async Task DeleteByCustomerIdAsync(int customerId)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    DELETE FROM 
                        public.address
                    WHERE
                        customer_id = @customerId;";

                await connection.ExecuteAsync(sql, new { customerId });
            }
        }

        public async Task DeleteById(int id)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    DELETE FROM 
                        public.address
                    WHERE
                        id = @id;";

                await connection.ExecuteAsync(sql, new { id });
            }
        }

        public async Task<IEnumerable<AddressDto>> GetAllAsync()
        {
            using (var connection = Connection)
            {
                var sql = @"
                    SELECT 
                        id, 
                        line1, 
                        line2, 
                        town, 
                        county, 
                        postcode, 
                        country,
                        main,
                        customer_id AS CustomerId
                    FROM 
                        public.address;";

                var result = await connection.QueryAsync<AddressDto>(sql);

                return result;
            }
        }

        public async Task<IEnumerable<AddressDto>> GetByCustomerIdAsync(int customerId)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    SELECT 
                        id, 
                        line1, 
                        line2, 
                        town, 
                        county, 
                        postcode, 
                        country,
                        main,
                        customer_id AS CustomerId
                    FROM 
                        public.address
                    WHERE
                        customer_id = @customerId";

                var result = await connection.QueryAsync<AddressDto>(sql, new { customerId });
                return result;
            }
        }

        public async Task<AddressDto> GetByIdAsync(int id)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    SELECT 
                        id, 
                        line1, 
                        line2, 
                        town, 
                        county, 
                        postcode, 
                        country,
                        main,
                        customer_id AS CustomerId
                    FROM 
                        public.address
                    WHERE
                        id = @id";

                var result = await connection.QuerySingleAsync<AddressDto>(sql, new { id });
                return result;
            }
        }

        public async Task<int> GetCustomerIdByAddressIdAsync(int id)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    SELECT
                        customer_id AS CustomerId
                    FROM 
                        public.address
                    WHERE
                        id = @id";

                var result = await connection.QuerySingleAsync<int>(sql, new { id });
                return result;
            }
        }

        public async Task UpdateAsync(AddressDto address)
        {
            using (var connection = Connection)
            {
                var sql = @"
                    UPDATE 
                        public.address
                    SET
                        line1 = COALESCE(@line1, line1), 
                        line2 = COALESCE(@line2, line2), 
                        town = COALESCE(@town, town), 
                        county = COALESCE(@county, county), 
                        postcode = COALESCE(@postcode, postcode), 
                        country = COALESCE(@country, country), 
                        main = COALESCE(@main, main)
                    WHERE
                        id = @id;";

                await connection.ExecuteAsync(sql, address);
            }
        }
    }
}
