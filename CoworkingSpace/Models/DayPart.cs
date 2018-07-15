using System.ComponentModel;

namespace CoworkingSpace.Models
{
    public enum DayPart
    {
        [Description("9am to 9 pm")]
        NineToNine = 0,

        [Description("24/7")]
        TwentyFourBySeven = 1
    }
}