using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp.Model
{
    public class UserModel
    {
        public int Id { get; set; }

        // ここではなくても必須になりますが、属性とかつけれる
        [Required]
        public string Name { get; set; } = string.Empty;

        public int? Age { get; set; }
    }
}
