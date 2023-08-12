﻿namespace Timetable.Domain.Enums.EntitiesEnums
{
    public enum RoomTypeEnum
    {
        [Display(Name = "قاعة نظري")]
        TheoryRoom,
        [Display(Name = "قاعة عملي")]
        LapRoom,
        [Display(Name = "قاعة نظري وعملي")]
        MixedRoom
    }
}
