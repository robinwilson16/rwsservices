using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Models
{
    public class Emoji
    {
        public int EmojiID { get; set; }

        [Display(Name = "Emoji")]
        [Required]
        [StringLength(8)]
        public string EmojiCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public int EmojiCategoryID { get; set; }
        [ForeignKey("EmojiCategoryID")]
        public EmojiCategory EmojiCategory { get; set; }
    }
}
