using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RWSServices.Models
{
    public class Blog
    {
        public int BlogID { get; set; }

        [Display(Name = "Blog Title")]
        [Required]
        [StringLength(200)]
        public string BlogTitle { get; set; }

        [Display(Name = "Main Image")]
        [StringLength(200)]
        public string BlogImage { get; set; }

        [Display(Name = "Blog Content")]
        [Required]
        public string BlogContent { get; set; }

        [Display(Name = "Created Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public ApplicationUser ApplicationUserCreatedBy { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
        [ForeignKey("UpdatedBy")]
        public ApplicationUser ApplicationUserUpdatedBy { get; set; }

        public string URL
        {
            get
            {
                string blogURL = BlogTitle;
                string pattern = @"[^A-Za-z0-9-]+";
                string replacement = "-";
                Regex rgx = new Regex(pattern);
                blogURL = rgx.Replace(blogURL, replacement);
                blogURL = blogURL.ToLower();
                blogURL = blogURL.Replace("--", "-");
                blogURL = blogURL.TrimEnd('-');

                return blogURL;
            }
        }
    }
}
