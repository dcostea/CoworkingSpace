using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoworkingSpace.Models
{
    public enum DayPart
    {
        [Display(Name = "9am to 9pm")]
        NineToNine = 0,

        [Display(Name = "24/7")]
        TwentyFourBySeven = 1
    }
}