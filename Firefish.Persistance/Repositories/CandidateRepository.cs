using Firefish.Domain.Entities;
using Firefish.Domain.Repositories;
using Firefish.Persistance.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Firefish.Persistance.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly IPersistanceConfig _config;

        public CandidateRepository(IPersistanceConfig config)
        {
            _config = config;
        }

        /*
         * Fetches a list of all Candidates with their basic details
         */
        public async Task<List<Candidate>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_config.ClientDbConnectionString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = "SELECT id, FirstName, Surname, DateOfBirth, Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, CreatedDate, UpdatedDate FROM Candidate ORDER BY Surname";

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var candidates = new List<Candidate>();

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                candidates.Add(new Candidate()
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                    Surname = reader.GetString(reader.GetOrdinal("Surname")),
                                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                                    Address1 = reader.GetString(reader.GetOrdinal("Address1")),
                                    Town = reader.GetString(reader.GetOrdinal("Town")),
                                    Country = reader.GetString(reader.GetOrdinal("Country")),
                                    PostCode = reader.GetString(reader.GetOrdinal("PostCode")),
                                    PhoneHome = reader.GetString(reader.GetOrdinal("PhoneHome")),
                                    PhoneMobile = reader.GetString(reader.GetOrdinal("PhoneMobile")),
                                    PhoneWork = reader.GetString(reader.GetOrdinal("PhoneWork")),
                                    CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                    UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"))
                                });
                            }

                        }

                        return candidates;
                    }
                }
            }
        }

        /*
         * Returns a specific Candidate along with their Skills
         */
        public async Task<Candidate> GetByIdAsync(int id)
        {
            Candidate candidate;
            var skills = new List<Skill>();

            using (var conn = new SqlConnection(_config.ClientDbConnectionString))
            {
                conn.Open();

                // First get the candidates basic details
                using (var cmdCandidate = conn.CreateCommand())
                {
                    // Was considering joining all tables in order to get the Candidates skills in one query and taking the details of the first row returned as the Candidate entity
                    //cmdCandidate.CommandText = "SELECT c.id, c.FirstName, c.Surname, c.DateOfBirth, c.Address1, c.Town, c.Country, c.PostCode, c.PhoneHome, c.PhoneMobile, c.PhoneWork, c.CreatedDate, c.UpdatedDate, cs.SkillId, Name FROM Candidate AS c LEFT OUTER JOIN CandidateSkill AS cs ON c.id = cs.CandidateID LEFT OUTER JOIN Skill AS s ON cs.SkillID = s.Id WHERE id = @id";

                    
                    cmdCandidate.CommandText = "SELECT id, FirstName, Surname, DateOfBirth, Address1, Town, Country, PostCode, PhoneHome, PhoneMobile, PhoneWork, CreatedDate, UpdatedDate FROM Candidate WHERE id = @id";
                    cmdCandidate.Parameters.AddWithValue("@id", id);

                    using(var reader = await cmdCandidate.ExecuteReaderAsync())
                    {
                        if(!reader.Read())
                        {
                            return null;
                        }

                        candidate = new Candidate
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            Surname = reader.GetString(reader.GetOrdinal("Surname")),
                            DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                            Address1 = reader.GetString(reader.GetOrdinal("Address1")),
                            Town = reader.GetString(reader.GetOrdinal("Town")),
                            Country = reader.GetString(reader.GetOrdinal("Country")),
                            PostCode = reader.GetString(reader.GetOrdinal("PostCode")),
                            PhoneHome = reader.GetString(reader.GetOrdinal("PhoneHome")),
                            PhoneMobile = reader.GetString(reader.GetOrdinal("PhoneMobile")),
                            PhoneWork = reader.GetString(reader.GetOrdinal("PhoneWork")),
                            CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                            UpdatedDate = reader.GetDateTime(reader.GetOrdinal("UpdatedDate"))
                        };
                    }
                }

                // Then get all the skills associated with the candidate
                using (var cmdSkills = conn.CreateCommand())
                {
                    cmdSkills.CommandText = "SELECT s.id, s.Name, s.CreatedDate, s.UpdatedDate FROM Skill AS s INNER JOIN CandidateSkill AS cs ON s.Id = cs.SkillID WHERE cs.CandidateID = @candidateId";
                    cmdSkills.Parameters.AddWithValue("@candidateId", id);

                    using (var sReader = await cmdSkills.ExecuteReaderAsync())
                    {
                        if (sReader != null)
                        {
                            while(sReader.Read())
                            {
                                skills.Add(new Skill()
                                {
                                    Id = sReader.GetInt32(sReader.GetOrdinal("id")),
                                    Name = sReader.GetString(sReader.GetOrdinal("name")),
                                    CreatedDate = sReader.GetDateTime(sReader.GetOrdinal("CreatedDate")),
                                    UpdatedDate = sReader.GetDateTime(sReader.GetOrdinal("UpdatedDate"))
                                });
                            }

                        }
                    }
                }
            }

            candidate.Skills = skills;

            return candidate;
        }

        /*
         * Creates a new Candidate
         * Returns the auto-generated ID of the newly created row
         */
        public async Task<int> Add(Candidate candidate)
        {
            using (var conn = new SqlConnection(_config.ClientDbConnectionString))
            {
                conn.Open();
                
                using (var cmdCandidate = conn.CreateCommand())
                {
                    cmdCandidate.CommandText = "INSERT INTO Candidate (Firstname, Surname, DateOfBirth, Address1, Town, Country, Postcode, PhoneHome, PhoneMobile, PhoneWork, UpdatedDate, CreatedDate) VALUES (@firstname, @surname, @dob, @address1, @town, @country, @postcode, @phoneHome, @phonemobile, @phoneWork, @updatedDate, @createdDate)";
                    cmdCandidate.CommandText += "SELECT CAST(scope_identity() AS int)";

                    cmdCandidate.Parameters.AddWithValue("@firstname", candidate.FirstName);
                    cmdCandidate.Parameters.AddWithValue("@surname", candidate.Surname);
                    cmdCandidate.Parameters.AddWithValue("@dob", candidate.DateOfBirth);
                    cmdCandidate.Parameters.AddWithValue("@address1", candidate.Address1);
                    cmdCandidate.Parameters.AddWithValue("@town", candidate.Town);
                    cmdCandidate.Parameters.AddWithValue("@country", candidate.Country);
                    cmdCandidate.Parameters.AddWithValue("@postcode", candidate.PostCode);
                    cmdCandidate.Parameters.AddWithValue("@phoneHome", candidate.PhoneHome);
                    cmdCandidate.Parameters.AddWithValue("@phoneMobile", candidate.PhoneMobile);
                    cmdCandidate.Parameters.AddWithValue("@phoneWork", candidate.PhoneWork);
                    cmdCandidate.Parameters.AddWithValue("@updatedDate", candidate.UpdatedDate);
                    cmdCandidate.Parameters.AddWithValue("@createdDate", candidate.CreatedDate);

                    // Get the auto-generated ID of the newly inserted row
                    return (int)await cmdCandidate.ExecuteScalarAsync();
                }
            }
        }

        /*
         * Adds a list of skills to a specific candidate
         * Returns the number of rows affected
         */
        public async Task<int> AddSkills(int candidateId, IEnumerable<int> skillIds)
        {
            var timeNow = DateTime.Now;
            int rowsAffected = 0;

            using (var conn = new SqlConnection(_config.ClientDbConnectionString))
            {
                conn.Open();

                foreach(var skillId in skillIds)
                {
                    using (var cmdCandidate = conn.CreateCommand())
                    {
                        cmdCandidate.CommandText = "INSERT INTO CandidateSkill (CandidateID, SkillID, CreatedDate, UpdatedDate) VALUES (@candidateId, @skillId, @createdDate, @updatedDate)";

                        cmdCandidate.Parameters.AddWithValue("@candidateId", candidateId);
                        cmdCandidate.Parameters.AddWithValue("@skillId", skillId);
                        cmdCandidate.Parameters.AddWithValue("@createdDate", timeNow);
                        cmdCandidate.Parameters.AddWithValue("@updatedDate", timeNow);

                        rowsAffected += await cmdCandidate.ExecuteNonQueryAsync();
                    }
                }
            }

            return rowsAffected;
        }

        /*
         * Updates a candidates details
         * Returns true if the row was succesfully updated
         */
        public async Task<bool> Update(int id, Candidate candidate)
        {
            using (var conn = new SqlConnection(_config.ClientDbConnectionString))
            {
                conn.Open();

                using (var cmdCandidate = conn.CreateCommand())
                {
                    cmdCandidate.CommandText = "Update Candidate SET Firstname = @firstname, Surname = @surname, DateOfBirth = @dob, Address1 = @address1, Town = @town, Country = @country, Postcode = @postcode, PhoneHome = @phoneHome, PhoneMobile = @phonemobile, PhoneWork = @phoneWork, UpdatedDate = @updatedDate WHERE id = @id";
                    cmdCandidate.Parameters.AddWithValue("@id", id);
                    cmdCandidate.Parameters.AddWithValue("@firstname", candidate.FirstName);
                    cmdCandidate.Parameters.AddWithValue("@surname", candidate.Surname);
                    cmdCandidate.Parameters.AddWithValue("@dob", candidate.DateOfBirth);
                    cmdCandidate.Parameters.AddWithValue("@address1", candidate.Address1);
                    cmdCandidate.Parameters.AddWithValue("@town", candidate.Town);
                    cmdCandidate.Parameters.AddWithValue("@country", candidate.Country);
                    cmdCandidate.Parameters.AddWithValue("@postcode", candidate.PostCode);
                    cmdCandidate.Parameters.AddWithValue("@phoneHome", candidate.PhoneHome);
                    cmdCandidate.Parameters.AddWithValue("@phoneMobile", candidate.PhoneMobile);
                    cmdCandidate.Parameters.AddWithValue("@phoneWork", candidate.PhoneWork);
                    cmdCandidate.Parameters.AddWithValue("@updatedDate", candidate.UpdatedDate);

                    int rowsAffected = await cmdCandidate.ExecuteNonQueryAsync();

                    // If no rows changed, let the calling function know
                    return rowsAffected > 0;
                }
            }
        }
    }
}
