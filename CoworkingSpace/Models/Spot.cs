using System.ComponentModel.DataAnnotations;

namespace CoworkingSpace.Models
{
    public enum Spot
    {
        [Display(Name = "Hot Desk")]
        HotDesk = 0,

        [Display(Name = "Dedicated Desk")]
        DedicatedDesk = 1,

        [Display(Name = "Private Office")]
        PrivateOffice = 2
    }
}