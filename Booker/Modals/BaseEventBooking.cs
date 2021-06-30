using Booker.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Booker.Modals
{
    public abstract class BaseEventBooking
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        [DateInFuture]
        [DateWithoutTime]
        public DateTime DateTime { get; set; }
    }
}