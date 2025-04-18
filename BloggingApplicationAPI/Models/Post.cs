using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloggingApplicationAPI.Models
{
    public class Post
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")] 
        [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")] 
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required.")] 
        [MinLength(10, ErrorMessage = "Content must be at least 10 characters long.")] 
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
