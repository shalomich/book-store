using System.ComponentModel.DataAnnotations;

namespace BookStore.WebApi.Areas.Store.ViewModels.Basket
{
    public record OrderForm
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
