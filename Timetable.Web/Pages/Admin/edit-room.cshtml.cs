using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Application.Services.DataIO.Teacher;
using Timetable.RazorWeb.ViewModels.InputModels;

namespace Timetable.RazorWeb.Pages.Admin
{
    public class edit_roomModel : PageModel
    {
        #region Fields
        private readonly IRoomService _roomService;
        private readonly IValidator<TeacherInputModel> _validator;
        #endregion

        #region Input Data
        public string roomID = null!;

        [Display(Name = "اسم القاعة")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "اسم القاعة مطلوب")]
        [BindProperty]
        public string RoomName { get; set; } = string.Empty;

        [Display(Name = "نوع القاعة")]
        [BindProperty]
        public RoomTypeEnum selectedRoomType { get; set; } = RoomTypeEnum.TheoryRoom;
        #endregion

        #region Output Data
        public List<RoomTypeEnum> RoomTypes { get; private set; } = new List<RoomTypeEnum>();
        #endregion

        #region Constructor
        public edit_roomModel(IRoomService roomService, IValidator<TeacherInputModel> validator)
        {
            _validator = validator;
            _roomService = roomService;
        }
        #endregion
        public async Task OnGet(string roomId)
        {
            Guid RoomId;
            if (!Guid.TryParse(roomId, out RoomId))
            {
                throw new NotImplementedException(message: roomId + " is not a valid guid");
            }
            var room = _roomService.getRoomById(RoomId);
            roomID = roomId;
            RoomName = room.Name;
            selectedRoomType = room.type;

            foreach (RoomTypeEnum type in Enum.GetValues(typeof(RoomTypeEnum)))
            {
                RoomTypes.Add(type);
            }
        }

        public async Task<IActionResult> OnPost(string roomId)
        {
            Guid RoomId;
            if (!Guid.TryParse(roomId, out RoomId))
            {
                throw new NotImplementedException(message: roomId + " is not a valid guid");
            }
            Room room = _roomService.getRoomById(RoomId);
            room.type = selectedRoomType;
            room.Name = RoomName;
            _roomService.UpdateRoom(room);
            return RedirectToPage("./class-room");
        }
    }
}
