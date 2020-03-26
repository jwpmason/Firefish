using Firefish.Domain.Entities;
using Firefish.Domain.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Firefish.Persistance.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly string _connectionString;
        public SkillRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Skill>> GetAllSkillsAsync()
        {
            using(var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT id, Name, CreatedDate, UpdatedDate FROM Skill ORDER BY Name";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var skills = new List<Skill>();

                        if (reader != null)
                        {
                            while(reader.Read())
                            {
                                skills.Add(new Skill() {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate")),
                                });
                            }
                            
                        }

                        return skills;
                    }
                }
            }
                
        }
    }
}
