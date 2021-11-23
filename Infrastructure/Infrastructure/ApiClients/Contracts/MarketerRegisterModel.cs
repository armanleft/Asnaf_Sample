using System;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ApiClients.Contracts
{
    public class MarketerRegisterModel
    {
        #region Properties

        //کد ملی
        [Required]
        public string NationalCode { get; set; }

        // نام
        [Required]
        public string FirstName { get; set; }

        //نام خانوادگی
        [Required]
        public string LastName { get; set; }

        //تاریخ تولد
        [Required]
        public string BirthDate { get; set; }

        // نام پدر
        public string FatherName { get; set; } = "";

        //تلفن تماس
        public string Phone1 { get; set; } = "";

        //تلفن تماس
        public string Phone2 { get; set; } = "";

        //ایمیل
        public string Email { get; set; } = "";

        //شماره شناسنامه
        public string IdNo { get; set; } = "";

        //میزان تحصیلات
        public string Education { get; set; } = "";

        //آدرس
        public string Address { get; set; } = "";

        //کدپستی
        public string PostalCode { get; set; } = "";

        //شماره ملی معرف
        public string ParentNationalCode { get; set; } = "";

        #endregion

        #region Constructors

        public MarketerRegisterModel(string nationalCode, string firstName, string lastName, DateTime birthDate)
        {
            NationalCode = nationalCode;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate.ToString("yyyy/MM/dd");
        }

        #endregion
    }
}