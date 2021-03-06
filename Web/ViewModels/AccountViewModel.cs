﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Web.Mvc;
using Domain.Identity;
using Resources;

namespace Web.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(ResourceType = typeof(Account), Name = "VerifyCodeViewModel_Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof(Account), Name = "VerifyCodeViewModel_RememberBrowser")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_UserNameRequired")]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_UserName")]
        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(12, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        public string Username { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
        //    ErrorMessageResourceName = "ViewModel_EmailRequired")]
        //[Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Password")]
        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(12, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]

        public string Password { get; set; }

        [Display(ResourceType = typeof(Account), Name = "ViewModel_RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        // [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Resources.Domain),
        //ErrorMessageResourceName = "PersonName")]
        // [Display(ResourceType = typeof(Resources.Domain), Name = nameof(UserInt.PersonName))]
        // public string PersonName { get; set; }

        [Remote("UserAlreadyExistsAsync", "Account", ErrorMessageResourceName = "UserNameExists", ErrorMessageResourceType = typeof(Resources.Common))]
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(ResourceType = typeof(Resources.Domain), Name = nameof(UserInt.UserName))]
        public string Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Account), ErrorMessageResourceName = "ViewModel_EmailInvalid")]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }
        [Display(ResourceType = typeof(Account), Name = "HaveYouPlayedBefore")]
        public bool HaveYouPlayedBefore { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_ConfirmPassword")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof(Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress]
        [Display(ResourceType = typeof(Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }
}