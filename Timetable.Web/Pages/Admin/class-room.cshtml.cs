using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Timetable.Application.Services.DataIO.Room;
using Timetable.Domain.Enums;
using Timetable.RazorWeb.Authorization;
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
            try
            {
                _roomService.deleteRoomById(new Guid(roomId));
            }
            catch (DbUpdateException ex)
            {
                TempData["ExceptionMessage"] = "لا يمكن حذف هذا العنصر لأن بيانات أخرى مرتبطة معه إذا كنت تريد حذف القاعة بالفعل قم بحذف كل البيانات المرتبطة بهذه القاعة وبعد ذلك قم بحذفها";
                return RedirectToPage("/Error");
            }
            catch (Exception e)
            {
                TempData["ExceptionMessage"] = "حدث خطأ ما الرجاء المحاولة لاحقاً";
                return RedirectToPage("/Error");
            }
            return RedirectToPage();
        }
        #endregion
    }
}
