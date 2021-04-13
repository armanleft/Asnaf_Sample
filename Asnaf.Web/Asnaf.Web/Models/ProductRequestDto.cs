using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Asnaf.Web.Models
{
    public class ProductRequestDto
    {
        [Display(Name = "رمز اتصال به وب سرویس")]
        [Required(ErrorMessage = "رمز اتصال به وب سرویس را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "شناسه شرکت")]
        [Required(ErrorMessage = "شناسه شرکت را وارد نمایید")]
        public string CompanyId { get; set; }

        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "نام کالا را وارد نمایید")]
        public string Title { get; set; }

        [Display(Name = "ایران کد")]
        [Required(ErrorMessage = "ایران کد را وارد نمایید")]
        public string IranCode { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "کاربر را وارد نمایید")]
        public string UserName { get; set; }

        [Display(Name = "آدرس پست الکترونيک کاربر")]
        [Required(ErrorMessage = "آدرس پست الکترونيک کاربر را وارد نمایید")]
        public string UserEmail { get; set; }

        [Display(Name = "تاریخ ثبت")]
        [Required(ErrorMessage = "تاریخ ثبت را وارد نمایید")]
        public string RegistrationDate { get; set; }

        [Display(Name = "مدارک اول مالکيت نام تجاری")]
        public IFormFile BrandOwnerdoc1 { get; set; }

        [Display(Name = "مدارک دوم مالکيت نام تجاری")]
        public IFormFile BrandOwnerdoc2 { get; set; }

        [Display(Name = "مدارک سوم مالکيت نام تجاری")]
        public IFormFile BrandOwnerdoc3 { get; set; }

        [Display(Name = "پروانه بهره برداری")]
        public IFormFile BusinessLicense { get; set; }

        [Display(Name = "مجوز اول بهداشتی و استاندارد")]
        public IFormFile HealthLicensing1 { get; set; }

        [Display(Name = "مجوز دوم بهداشتی و استاندارد")]
        public IFormFile HealthLicensing2 { get; set; }

        [Display(Name = "مجوز سوم بهداشتی و استاندارد")]
        public IFormFile HealthLicensing3 { get; set; }

        [Display(Name = "جدول برآورد هزینه")]
        public IFormFile CostEstimation { get; set; }

        [Display(Name = "تعهد نامه ضوابط قيمت گذاری محصول")]
        public IFormFile Commitment { get; set; }

        [Display(Name = "کاتالوگ محصولات مصرفی")]
        public IFormFile Catalogue { get; set; }

        [Display(Name = "پيش فاکتور اول واحدهای توليدی مختلف")]
        public IFormFile Factor1 { get; set; }

        [Display(Name = "پيش فاکتور دوم واحدهای توليدی مختلف")]
        public IFormFile Factor2 { get; set; }

        [Display(Name = "سایر مدارک )قرارداد ظرفيت خالی و درصد توليد قراردادی(")]
        public IFormFile OtherDocument { get; set; }
    }
}