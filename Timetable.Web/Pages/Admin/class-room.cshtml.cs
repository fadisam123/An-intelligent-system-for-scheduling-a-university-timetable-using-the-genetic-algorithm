using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Course;
using Timetable.Application.Services.DataIO.Room;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class class_roomModel : PageModel
    {
        #region Fields
        private readonly IRoomService _roomService;
        private readonly IValidator<TeacherInputModel> _validator;
        #endregion

        #region Input Data
        [Display(Name = "اسم القاعة")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "اسم القاعة مطلوب")]
        //[Remote(action: "IsRoomNameInUse", controller: "RemoteValidators")]
        [BindProperty]
        public string RoomName { get; set; } = string.Empty;
        [Display(Name = "نوع القاعة")]
        [BindProperty]
        public RoomTypeEnum selectedRoomType { get; set; } = RoomTypeEnum.TheoryRoom;
        #endregion

        #region Output Data
        public List<Room> Rooms { get; private set; } = new List<Room>();
        public List<RoomTypeEnum> RoomTypes { get; private set; } = new List<RoomTypeEnum>();
        #endregion

        public class_roomModel(IRoomService roomService, IValidator<TeacherInputModel> validator)
        {
            _roomService = roomService;
            _validator = validator;
        }

        #region Handler Methods
        public void OnGet()
        {
            Rooms = _roomService.getAllRooms().ToList();

            foreach (RoomTypeEnum type in Enum.GetValues(typeof(RoomTypeEnum)))
            {
                RoomTypes.Add(type);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            Room room = new Room { Name = RoomName, type = selectedRoomType };
            await _roomService.createRoomAsync(room);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string roomId)
        {

            return RedirectToPage();
        }
        #endregion
    }
}
