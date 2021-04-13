using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Asnaf.Web.Models
{
    public class CancelBranchRequestDto
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

        [Display(Name = "فرم درخواست لغو شعبه")]
        public IFormFile CancelForm { get; set; }
    }
}