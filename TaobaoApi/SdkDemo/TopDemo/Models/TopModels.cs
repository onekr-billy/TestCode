using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TopDemo.Models
{
    public class AuthModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "AppKey")]
        public string AppKey { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "AppSecret")]
        public string AppSecret { get; set; }

        [Display(Name="记住我")]
        public bool RememberMe { get; set; }
    }
}