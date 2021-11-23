using Asnaf.Web.Models;
using Infrastructure.ApiClients;
using Infrastructure.ApiClients.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asnaf.Web.Controllers
{
    public class AsnafController : Controller
    {
        #region Fields

        private readonly IAsnafBranchesApiClient _asnafBranchesApiClient;
        private readonly IAsnafConferenceApiClient _asnafConferenceApiClient;
        private readonly IAsnafProductsApiClient _asnafProductsApiClient;
        private readonly IMarketingApiClient _marketingApiClient;

        #endregion

        #region Constructors

        public AsnafController(IAsnafBranchesApiClient asnafBranchesApiClient,
            IAsnafConferenceApiClient asnafConferenceApiClient,
            IAsnafProductsApiClient asnafProductsApiClient,
            IMarketingApiClient marketingApiClient)
        {
            _asnafBranchesApiClient = asnafBranchesApiClient;
            _asnafConferenceApiClient = asnafConferenceApiClient;
            _asnafProductsApiClient = asnafProductsApiClient;
            _marketingApiClient = marketingApiClient;
        }

        #endregion

        #region Public Methods

        [HttpGet]
        public async Task<ActionResult> BranchIssuance()
        {
            var provincesAndCities = (await _asnafBranchesApiClient.GetProvinceCityListAsync()).Response;

            ViewBag.Provinces = provincesAndCities?
                                    .Select(c => new SelectListItem
                                    {
                                        Text = c.ProvinceTitle,
                                        Value = c.ProvinceId.ToString()
                                    }).AsEnumerable()
                                ?? new List<SelectListItem>().AsEnumerable();


            ViewBag.cities = new List<SelectListItem>().AsEnumerable();

            return View(new BranchIssuanceRequestDto());
        }

        [HttpGet]
        public async Task<ActionResult> GetCities(string parentId)
        {
            ViewBag.Cities = (await _asnafBranchesApiClient.GetProvinceCityListAsync()).Response
                .First(a => a.ProvinceId == parentId).ProvinceCity
                .Select(c => new SelectListItem
                {
                    Text = c.CityTitle,
                    Value = c.CityId.ToString()
                }).AsEnumerable();

            return PartialView("_CitiesSelectedList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> BranchIssuance(BranchIssuanceRequestDto input)
        {
            #region Return View Bags

            var provincesAndCities = (await _asnafBranchesApiClient.GetProvinceCityListAsync()).Response;
            ViewBag.Provinces = provincesAndCities
                .Select(c => new SelectListItem
                {
                    Text = c.ProvinceTitle,
                    Value = c.ProvinceId.ToString(),
                    Selected = c.ProvinceId == input.Province
                }).AsEnumerable();

            ViewBag.Cities = provincesAndCities
                .First(a => a.ProvinceId == input.Province).ProvinceCity
                .Select(c => new SelectListItem
                {
                    Text = c.CityTitle,
                    Value = c.CityId.ToString(),
                    Selected = c.CityId == input.City
                }).AsEnumerable();

            #endregion

            #region Check Validations

            if (string.IsNullOrEmpty(input.Province))
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.Province), "استان را انتخاب نمایید");

            if (string.IsNullOrEmpty(input.City))
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.City), "شهر را انتخاب نمایید");

            if (input.NationalCardCopy == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.NationalCardCopy),
                    "عکس کارت ملی را وارد نمایید");

            if (input.IdentificationCopy == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.IdentificationCopy),
                    "کپی شناسنامه را وارد نمایید");

            if (input.ObligationForm == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.ObligationForm),
                    "فرم تعهد مسئول شعبه را وارد نمایید");

            if (input.EstablishForm == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.EstablishForm),
                    "فرم درخواست تاسيس شعبه را وارد نمایید");

            if (input.RentalContract == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.RentalContract),
                    "قرارداد اجاره نامه را وارد نمایید");

            if (input.OfficialNewspaper == null)
                ModelState.AddModelError(nameof(BranchIssuanceRequestDto.OfficialNewspaper),
                    "روزنامه رسمی ثبت شرکت را وارد نمایید");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new BranchIssuanceRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                Province = input.Province,
                City = input.City,
                ManagerName = input.ManagerName,
                Mobile = input.Mobile,
                NationalId = input.NationalId,
                PostalCode = input.PostalCode,
                Address = input.Address
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            await using (var ms = new MemoryStream())
            {
                input.NationalCardCopy.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.NationalCardCopy = Convert.ToBase64String(fileBytes);
                request.NationalCardCopyFileName = input.NationalCardCopy.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.IdentificationCopy.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.IdentificationCopy = Convert.ToBase64String(fileBytes);
                request.IdentificationCopyFileName = input.IdentificationCopy.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.ObligationForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.ObligationForm = Convert.ToBase64String(fileBytes);
                request.ObligationFormFileName = input.ObligationForm.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.EstablishForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.EstablishForm = Convert.ToBase64String(fileBytes);
                request.EstablishFormFileName = input.EstablishForm.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.RentalContract.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.RentalContract = Convert.ToBase64String(fileBytes);
                request.RentalContractFileName = input.RentalContract.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.OfficialNewspaper.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.OfficialNewspaper = Convert.ToBase64String(fileBytes);
                request.OfficialNewspaperFileName = input.OfficialNewspaper.FileName;
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafBranchesApiClient.BranchIssuanceAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        [HttpGet]
        public async Task<ActionResult> ChangeManager()
        {
            return View(new ChangeManagerRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeManager(ChangeManagerRequestDto input)
        {
            #region Check Validations

            if (input.NationalCardCopy == null)
                ModelState.AddModelError(nameof(ChangeManagerRequestDto.NationalCardCopy),
                    "عکس کارت ملی را وارد نمایید");

            if (input.IdentificationCopy == null)
                ModelState.AddModelError(nameof(ChangeManagerRequestDto.IdentificationCopy),
                    "کپی شناسنامه را وارد نمایید");

            if (input.EstablishForm == null)
                ModelState.AddModelError(nameof(ChangeManagerRequestDto.EstablishForm),
                    "فرم درخواست تاسيس شعبه را وارد نمایید");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new ChangeManagerRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                BranchCode = input.BranchCode,
                ManagerName = input.ManagerName,
                ManagerNationalCode = input.ManagerNationalCode,
                ManagerMobile = input.ManagerMobile
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            await using (var ms = new MemoryStream())
            {
                input.NationalCardCopy.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.NationalCardCopy = Convert.ToBase64String(fileBytes);
                request.NationalCardCopyFileName = input.NationalCardCopy.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.IdentificationCopy.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.IdentificationCopy = Convert.ToBase64String(fileBytes);
                request.IdentificationCopyFileName = input.IdentificationCopy.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.EstablishForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.EstablishForm = Convert.ToBase64String(fileBytes);
                request.EstablishFormFileName = input.EstablishForm.FileName;
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafBranchesApiClient.ChangeManagerAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        [HttpGet]
        public async Task<ActionResult> ChangeAddress()
        {
            return View(new ChangeAddressRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeAddress(ChangeAddressRequestDto input)
        {
            #region Check Validations

            if (input.ChangeAddressForm == null)
                ModelState.AddModelError(nameof(ChangeAddressRequestDto.ChangeAddressForm),
                    "فرم درخواست تغيير آدرس شعبه را وارد نمایید");

            if (input.RentalContract == null)
                ModelState.AddModelError(nameof(ChangeAddressRequestDto.RentalContract),
                    "قرارداد اجاره نامه را وارد نمایید");

            if (input.OfficialNewspaper == null)
                ModelState.AddModelError(nameof(ChangeAddressRequestDto.OfficialNewspaper),
                    "روزنامه رسمی ثبت شرکت را وارد نمایید");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new ChangeAddressRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                BranchCode = input.BranchCode,
                PostalCode = input.PostalCode,
                Address = input.Address,
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            await using (var ms = new MemoryStream())
            {
                input.ChangeAddressForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.ChangeAddressForm = Convert.ToBase64String(fileBytes);
                request.ChangeAddressFormFileName = input.ChangeAddressForm.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.RentalContract.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.RentalContract = Convert.ToBase64String(fileBytes);
                request.RentalContractFileName = input.RentalContract.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.OfficialNewspaper.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.OfficialNewspaper = Convert.ToBase64String(fileBytes);
                request.OfficialNewspaperFileName = input.OfficialNewspaper.FileName;
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafBranchesApiClient.ChangeAddressAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        [HttpGet]
        public async Task<ActionResult> CancelBranch()
        {
            return View(new CancelBranchRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelBranch(CancelBranchRequestDto input)
        {
            #region Check Validations

            if (input.CancelForm == null)
                ModelState.AddModelError(nameof(CancelBranchRequestDto.CancelForm),
                    "فرم درخواست لغو شعبه را وارد نمایید");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new CancelBranchRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                BranchCode = input.BranchCode,
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            await using (var ms = new MemoryStream())
            {
                input.CancelForm.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.CancelForm = Convert.ToBase64String(fileBytes);
                request.CancelFormFileName = input.CancelForm.FileName;
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafBranchesApiClient.CancelBranchAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        [HttpGet]
        public async Task<ActionResult> ConferenceIssuance()
        {
            return View(new ConferenceIssuanceRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConferenceIssuance(ConferenceIssuanceRequestDto input)
        {
            #region Check Validations

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new ConferenceIssuanceRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                Title = input.Title,
                Date = input.Date,
                Location = input.Location,
                Text = input.Text,
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            if (input.Image1 != null)
            {
                await using var ms = new MemoryStream();
                input.Image1.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Image = Convert.ToBase64String(fileBytes);
                request.ImageFileName = input.Image1.FileName;
            }

            if (input.Image2 != null)
            {
                await using var ms = new MemoryStream();
                input.Image2.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Image = $"{request.Image}|{Convert.ToBase64String(fileBytes)}";
                request.ImageFileName = $"{request.ImageFileName}|{input.Image2.FileName}";
            }


            if (input.Image3 != null)
            {
                await using var ms = new MemoryStream();
                input.Image3.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Image = $"{request.Image}|{Convert.ToBase64String(fileBytes)}";
                request.ImageFileName = $"{request.ImageFileName}|{input.Image3.FileName}";
            }

            if (input.Commitment1 != null)
            {
                await using var ms = new MemoryStream();
                input.Commitment1.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Commitment = Convert.ToBase64String(fileBytes);
                request.CommitmentFileName = input.Commitment1.FileName;
            }

            if (input.Commitment2 != null)
            {
                await using var ms = new MemoryStream();
                input.Commitment2.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Commitment = $"{request.Commitment}|{Convert.ToBase64String(fileBytes)}";
                request.CommitmentFileName = $"{request.CommitmentFileName}|{input.Commitment2.FileName}";
            }


            if (input.Commitment3 != null)
            {
                await using var ms = new MemoryStream();
                input.Commitment3.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Commitment = $"{request.Commitment}|{Convert.ToBase64String(fileBytes)}";
                request.CommitmentFileName = $"{request.CommitmentFileName}|{input.Commitment3.FileName}";
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafConferenceApiClient.ConferenceIssuanceAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        [HttpGet]
        public async Task<ActionResult> Product()
        {
            return View(new ProductRequestDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Product(ProductRequestDto input)
        {
            #region Check Validations

            if (input.BrandOwnerdoc1 == null)
                ModelState.AddModelError(nameof(ProductRequestDto.BrandOwnerdoc1),
                    "حداقل یک مدرک مالکيت نام تجاری را وارد نمایید");

            if (input.BusinessLicense == null)
                ModelState.AddModelError(nameof(ProductRequestDto.BusinessLicense),
                    "پروانه بهره برداری را وارد نمایید");

            if (input.HealthLicensing1 == null)
                ModelState.AddModelError(nameof(ProductRequestDto.HealthLicensing1),
                    "حداقل یک مجوز بهداشتی و استاندارد را وارد نمایید");

            if (input.CostEstimation == null)
                ModelState.AddModelError(nameof(ProductRequestDto.CostEstimation), "جدول برآورد هزینه را وارد نمایید");

            if (input.Commitment == null)
                ModelState.AddModelError(nameof(ProductRequestDto.Commitment),
                    "تعهد نامه ضوابط قيمت گذاری محصول را وارد نمایید");

            if (input.Catalogue == null)
                ModelState.AddModelError(nameof(ProductRequestDto.Catalogue), "کاتالوگ محصولات مصرفی را وارد نمایید");

            if (input.Factor1 == null)
                ModelState.AddModelError(nameof(ProductRequestDto.Factor1),
                    "پيش فاکتور اول واحدهای توليدی مختلف را وارد نمایید");

            if (input.OtherDocument == null)
                ModelState.AddModelError(nameof(ProductRequestDto.OtherDocument),
                    "قرارداد ظرفيت خالی یا درصد توليد قراردادی را وارد نمایید");

            if (!ModelState.IsValid)
            {
                ViewBag.Result = false;
                ViewBag.Message = "لطفا اطلاعات ضروری را وارد نمایید";
                return View(input);
            }

            #endregion

            #region Create Request

            var request = new ProductRequest
            {
                Password = input.Password,
                CompanyId = input.CompanyId,
                Title = input.Title,
                IranCode = input.IranCode,
                UserName = input.UserName,
                UserEmail = input.UserEmail,
                RegistrationDate = input.RegistrationDate
            };

            #endregion

            #region Convert Uploaded Photos To Base64 And Add To Request

            await using (var ms = new MemoryStream())
            {
                input.BrandOwnerdoc1.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.BrandOwnerdoc = Convert.ToBase64String(fileBytes);
                request.BrandOwnerdocFileName = input.BrandOwnerdoc1.FileName;
            }

            if (input.BrandOwnerdoc2 != null)
            {
                await using var ms = new MemoryStream();
                input.BrandOwnerdoc2.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.BrandOwnerdoc = $"{request.BrandOwnerdoc}|{Convert.ToBase64String(fileBytes)}";
                request.BrandOwnerdocFileName = $"{request.BrandOwnerdocFileName}|{input.BrandOwnerdoc2.FileName}";
            }

            if (input.BrandOwnerdoc3 != null)
            {
                await using var ms = new MemoryStream();
                input.BrandOwnerdoc3.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.BrandOwnerdoc = $"{request.BrandOwnerdoc}|{Convert.ToBase64String(fileBytes)}";
                request.BrandOwnerdocFileName = $"{request.BrandOwnerdocFileName}|{input.BrandOwnerdoc3.FileName}";
            }

            await using (var ms = new MemoryStream())
            {
                input.BusinessLicense.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.BusinessLicense = Convert.ToBase64String(fileBytes);
                request.BusinessLicenseFileName = input.BusinessLicense.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.HealthLicensing1.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.HealthLicensing = Convert.ToBase64String(fileBytes);
                request.HealthLicensingFileName = input.HealthLicensing1.FileName;
            }

            if (input.HealthLicensing2 != null)
            {
                await using var ms = new MemoryStream();
                input.HealthLicensing2.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.HealthLicensing = $"{request.HealthLicensing}|{Convert.ToBase64String(fileBytes)}";
                request.HealthLicensingFileName =
                    $"{request.HealthLicensingFileName}|{input.HealthLicensing2.FileName}";
            }

            if (input.HealthLicensing3 != null)
            {
                await using var ms = new MemoryStream();
                input.HealthLicensing3.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.HealthLicensing = $"{request.HealthLicensing}|{Convert.ToBase64String(fileBytes)}";
                request.HealthLicensingFileName =
                    $"{request.HealthLicensingFileName}|{input.HealthLicensing3.FileName}";
            }

            await using (var ms = new MemoryStream())
            {
                input.CostEstimation.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.CostEstimation = Convert.ToBase64String(fileBytes);
                request.CostEstimationFileName = input.CostEstimation.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.Commitment.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Commitment = Convert.ToBase64String(fileBytes);
                request.CommitmentFileName = input.Commitment.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.Catalogue.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Catalogue = Convert.ToBase64String(fileBytes);
                request.CatalogueFileName = input.Catalogue.FileName;
            }

            await using (var ms = new MemoryStream())
            {
                input.Factor1.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Factor = Convert.ToBase64String(fileBytes);
                request.FactorFileName = input.Factor1.FileName;
            }

            if (input.Factor2 != null)
            {
                await using var ms = new MemoryStream();
                input.Factor2.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.Factor = $"{request.Factor}|{Convert.ToBase64String(fileBytes)}";
                request.FactorFileName = $"{request.FactorFileName}|{input.Factor2.FileName}";
            }

            await using (var ms = new MemoryStream())
            {
                input.OtherDocument.CopyTo(ms);
                var fileBytes = ms.ToArray();
                request.OtherDocument = Convert.ToBase64String(fileBytes);
                request.OtherDocumentFileName = input.OtherDocument.FileName;
            }

            #endregion

            #region Call Webservice And Return Result

            var result = await _asnafProductsApiClient.ProductAsync(request);

            if (result.Status == 1)
            {
                ViewBag.Result = true;
                ViewBag.Message = $"درخواست شما با موفقیت ثبت شد، شماره پیگیری {result.RequestId}";
            }
            else
            {
                ViewBag.Result = false;
                ViewBag.Message = result.Message;
            }

            return View(input);

            #endregion
        }

        /// <summary>
        /// ثبت بازاریاب در وب سرویس بازاریابی
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterAsync(MarketerRegisterModel request)
        {
            // return _marketingApiClient.RegisterAsync(request);
            return null;
        }

        #endregion
    }
}