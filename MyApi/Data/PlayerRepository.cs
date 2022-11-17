using Microsoft.Extensions.Configuration;
using MyApi.Contracts;
using MyApi.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Numerics;
using System.Threading.Tasks;

namespace MyApi.Data
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly string connectionString;
        public PlayerRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task DeleteById(int id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeletePlayer", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task<List<Player>> GetAll()
        {
            using(SqlConnection sql = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetAllPlayers", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<Player>();
                    await sql.OpenAsync();

                    using(var reader = await cmd.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Add(MapToPlayer(reader));
                        }
                    }
                    return response;
                }
            }
        }

        public async Task<Player> GetById(int id)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetPlayerById", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Id", id));
                    Player response = null;
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response = MapToPlayer(reader);
                        }
                    }
                    return response;
                }
            }
        }

        public async Task Insert(Player player)
        {
            using (SqlConnection sql = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertPlayer", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Name", player.Name));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }


        private Player MapToPlayer(SqlDataReader reader)
        {
            return new Player()
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
        }
    }
}
