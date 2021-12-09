using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.API.Models
{
    public class ToDoViewModel
    {
        [Key]
        public int ToDoId { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public bool Done { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public virtual CategoryViewModel Category { get; set; }
    }
    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "**MAX 25 Characters**")]
        public string CategoryName { get; set; }
        [StringLength(50, ErrorMessage = "**MAX 50 Characters**")]
        public string CategoryDescription { get; set; }
    }
}