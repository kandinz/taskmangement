using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement_API.Models
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string? LastName { get; set; }
        [StringLength(255)]
        public string? FirstName { get; set; }
        public string Email { get; set; }
        public byte Role { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Password { get; set; }
        public string Salt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
        public byte Active { get; set; }
        public Guid CreateUser { get; set; }
        public Guid? ModifyUser { get; set; }
    }
}
