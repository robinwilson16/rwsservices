using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RWSServices.Models
{
    public class EmojiCategory
    {
        public int EmojiCategoryID { get; set; }

        [Display(Name = "Emoji Category")]
        [StringLength(50, MinimumLength = 3)]
        public string Category { get; set; }

        public ICollection<Emoji> Emojis { get; set; }
    }
}
