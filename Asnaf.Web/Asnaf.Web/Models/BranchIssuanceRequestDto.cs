using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Asnaf.Web.Models
{
    public class BranchIssuanceRequestDto
    {
        [Display(Name = "رمز اتصال به وب سرویس")]
        [Required(ErrorMessage = "رمز اتصال به وب سرویس را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "شناسه شرکت")]
        [Required(ErrorMessage = "شناسه شرکت را وارد نمایید")]
        public string CompanyId { get; set; }

        [Display(Name = "استان")] public string Province { get; set; }

        [Display(Name = "شهر")] public string City { get; set; }

        [Display(Name = "نام و نام خانوادگی مسئول")]
        [Required(ErrorMessage = "نام و نام خانوادگی مسئول را وارد نمایید")]
        public string ManagerName { get; set; }

        [Display(Name = "تلفن همراه")]
        [Required(ErrorMessage = "تلفن همراه را وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "کد ملی")]
        [Required(ErrorMessage = "کد ملی را وارد نمایید")]
        public string NationalId { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "کد پستی را وارد نمایید")]
        public string PostalCode { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "آدرس را وارد نمایید")]
        public string Address { get; set; }

        [Display(Name = "کپی کارت ملی")] 
        public IFormFile NationalCardCopy { get; set; }

        [Display(Name = "کپی شناسنامه")]
        public IFormFile IdentificationCopy { get; set; }

        [Display(Name = "فرم تعهد مسئول شعبه")]
        public IFormFile ObligationForm { get; set; }

        [Display(Name = "فرم درخواست تاسيس شعبه")]
        public IFormFile EstablishForm { get; set; }

        [Display(Name = "قرارداد اجاره نامه")]
        public IFormFile RentalContract { get; set; }

        [Display(Name = "روزنامه رسمی ثبت شرکت")]
        public IFormFile OfficialNewspaper { get; set; }
    }
}