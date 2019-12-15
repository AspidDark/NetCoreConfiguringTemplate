using System;
using System.ComponentModel.DataAnnotations;

namespace TB.Domain
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }
    }
}
