﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Entities.Request
{
    public class ItemRequest
    {
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "{0}  is required")]
        [StringLength(128, ErrorMessage = "{0} can not be more than {1} characters")]
        public string? ItemName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
