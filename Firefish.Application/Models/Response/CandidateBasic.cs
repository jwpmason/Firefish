using System;

namespace Firefish.Application.Models.Response
{
    public class CandidateBasic
    {
        public CandidateBasic()
        {

        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AddDate { get; set; }
    }

}
