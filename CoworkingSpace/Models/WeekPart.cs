using System.ComponentModel.DataAnnotations;

namespace CoworkingSpace.Models
{
    public enum WeekPart
    {
        [Display(Name = "Working days")]
        WorkWeek = 0,

        [Display(Name = "Weekend")]
        Weekend = 1,

        [Display(Name = "Full Week")]
        FullWeek = 2
    }
}