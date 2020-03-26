using Firefish.Application.Models.Annotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Firefish.Application.Models.Input
{
    public class CandidateDetails
    {
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [SqlDateAttribute(ErrorMessage = "Date of birth is not a valid year.")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100)]
        public string Address1 { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Town { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string Country { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(20)]
        public string PostCode { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string PhoneHome { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string PhoneMobile { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(50)]
        public string PhoneWork { get; set; }
    }
}
