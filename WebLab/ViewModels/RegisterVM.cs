using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebLab.ViewModels
{
    public class RegisterVM
    {
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        public string Taikhoan { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Matkhau { get; set; }

        [Compare("Matkhau", ErrorMessage = "Mật khẩu không khớp.")]
        public string MatkhauConfirm { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; }

        public string DiachiKH { get; set; }

        [Required(ErrorMessage = "Điện thoại là bắt buộc.")]
        public string DienthoaiKH { get; set; }

        public DateTime NgaySinh { get; set; }
    }

    public class LoginVM
    {
        [Required(ErrorMessage = "Phải nhập tên đăng nhập.")]
        public string Taikhoan { get; set; }
        [Required(ErrorMessage = "Phải nhập mật khẩu")]
        public string Matkhau { get; set; }


    }
}