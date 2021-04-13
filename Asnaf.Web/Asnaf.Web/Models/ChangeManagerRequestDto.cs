using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Asnaf.Web.Models
{
    public class ChangeManagerRequestDto
    {
        [Display(Name = "رمز اتصال به وب سرویس")]
        [Required(ErrorMessage = "رمز اتصال به وب سرویس را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "شناسه شرکت")]
        [Required(ErrorMessage = "شناسه شرکت را وارد نمایید")]
        public string CompanyId { get; set; }

        [Display(Name = "شناسه شعبه")]
        [Required(ErrorMessage = "شناسه شعبه را وارد نمایید")]
        public string BranchCode { get; set; }

        [Display(Name = "نام و نام خانوادگی مسئول")]
        [Required(ErrorMessage = "نام و نام خانوادگی مسئول را وارد نمایید")]
        public string ManagerName { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "کد ملی را وارد نمایید")]
        public string ManagerNationalCode { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "تلفن همراه را وارد نمایید")]
        public string ManagerMobile { get; set; }

        [Display(Name = "کپی کارت ملی")]
        public IFormFile NationalCardCopy { get; set; }

        [Display(Name = "کپی شناسنامه")] 
        public IFormFile IdentificationCopy { get; set; }

        [Display(Name = "فرم درخواست تاسيس شعبه")]
        public IFormFile EstablishForm { get; set; }
    }
}