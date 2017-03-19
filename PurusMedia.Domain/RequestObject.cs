using System;
using System.ComponentModel.DataAnnotations;

namespace PurusMedia.Domain
{
    public class RequestObject
    {
        [Required(ErrorMessage ="Bad Request")]
        [StringLength(16, ErrorMessage = "Bad Request")]
        public string CreditCardNumber { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        [Range(0,double.MaxValue, ErrorMessage ="Bad Request")]
        public decimal? Amount { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        public string CardHolder { get; set; }
        [Required(ErrorMessage = "Bad Request")]
        public DateTime ExpirationDate { get; set; }
        [StringLength(3, ErrorMessage = "Bad Request")]
        public string SecurityCode { get; set; }
    }
}
