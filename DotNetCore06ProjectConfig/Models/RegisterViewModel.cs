using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using DotNetCore06ProjectConfig.Data.Entity;
using DotNetCore06ProjectConfig.Models.MasterData;

namespace DotNetCore06ProjectConfig.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "please enter your full name")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "maximum 12 char and minimum 3 char")]
        [DisplayName("Full Name")]
        public string? FullName { get; set; }


        [Required(ErrorMessage = "please select your image")]
        [DisplayName("Image")]
        public IFormFile? Img { get; set; }

        [Required(ErrorMessage = "please enter your phone number")]
        [StringLength(20)]
        public string? Phone { get; set; }

        //[Required(ErrorMessage = "please select your gender")]
        //public int? genderId { get; set; }

        //[Required(ErrorMessage = "please enter your email address")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        //[Required(ErrorMessage = "please enter your full Address")]
        [StringLength(200)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "please enter a password")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? ApplicationUserId { get; set; }
        public IEnumerable<ApplicationUser>? AllUsers { get; set; } 
        public IEnumerable<Gender>? Genders { get; set; } 

    }
}
