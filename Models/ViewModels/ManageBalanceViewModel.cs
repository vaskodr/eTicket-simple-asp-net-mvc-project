using System.ComponentModel.DataAnnotations;

namespace finalTicket.Models.ViewModels;

public class ManageBalanceViewModel
{
    [Display(Name = "Current Balance")]
    public decimal CurrentBalance { get; set; }

    [Required(ErrorMessage = "Please enter an amount to add.")]
    [Range(1, 10000, ErrorMessage = "Please enter a value between 1 and 10,000.")]
    [Display(Name = "Add Amount")]
    public decimal AddAmount { get; set; }
}