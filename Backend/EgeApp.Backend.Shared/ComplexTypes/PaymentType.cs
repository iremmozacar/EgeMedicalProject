using System.ComponentModel.DataAnnotations;

namespace EgeApp.Backend.Shared.ComplexTypes
{
    //PaymentType.CreditCard
    public enum PaymentType
    {
        [Display(Name = "Kredi Kartı")]
        CreditCard = 0,
        [Display(Name = "Eft/Havale")]
        Eft = 1
    }
}
