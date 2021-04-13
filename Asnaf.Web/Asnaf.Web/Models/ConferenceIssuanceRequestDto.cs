using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Asnaf.Web.Models
{
    public class ConferenceIssuanceRequestDto
    {
        [Display(Name = "رمز اتصال به وب سرویس")]
        [Required(ErrorMessage = "رمز اتصال به وب سرویس را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "شناسه شرکت")]
        [Required(ErrorMessage = "شناسه شرکت را وارد نمایید")]
        public string CompanyId { get; set; }

        [Display(Name = "عنوان همایش")]
        [Required(ErrorMessage = "عنوان همایش را وارد نمایید")]
        public string Title { get; set; }

        [Display(Name = "تاریخ برگزاری")]
        [Required(ErrorMessage = "تاریخ برگزاری را وارد نمایید")]
        public string Date { get; set; }

        [Display(Name = "آدرس محل برگزاری")]
        [Required(ErrorMessage = "آدرس محل برگزاری را وارد نمایید")]
        public string Location { get; set; }

        [Display(Name = "محتوای متنی همایش")]
        [Required(ErrorMessage = "محتوای متنی همایش را وارد نمایید")]
        public string Text { get; set; }

        [Display(Name = "محتوای اول تصویری همایش")]
        public IFormFile Image1 { get; set; }

        [Display(Name = "محتوای دوم تصویری همایش")]
        public IFormFile Image2 { get; set; }

        [Display(Name = "محتوای سوم تصویری همایش")]
        public IFormFile Image3 { get; set; }

        [Display(Name = "مدارک همایش )تقاضانامه,تعهدنامه،محتوای همایش(")]
        public IFormFile Commitment1 { get; set; }

        [Display(Name = "مدارک همایش )تقاضانامه,تعهدنامه،محتوای همایش(")]
        public IFormFile Commitment2 { get; set; }

        [Display(Name = "مدارک همایش )تقاضانامه,تعهدنامه،محتوای همایش(")]
        public IFormFile Commitment3 { get; set; }
    }
}