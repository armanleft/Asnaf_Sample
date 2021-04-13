using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Asnaf.Web.Models
{
    public class ChangeAddressRequestDto
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

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "کد پستی را وارد نمایید")]
        public string PostalCode { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "آدرس را وارد نمایید")]
        public string Address { get; set; }

        [Display(Name = "فرم درخواست تغيير آدرس شعبه")]
        public IFormFile ChangeAddressForm { get; set; }

        [Display(Name = "قرارداد اجاره نامه")]
        public IFormFile RentalContract { get; set; }

        [Display(Name = "روزنامه رسمی ثبت شرکت")]
        public IFormFile OfficialNewspaper { get; set; }
    }
}