using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement_API.Models
{
    public partial class Task
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public string Content { get; set; }
        public Guid? UserAssign { get; set; }
        public bool? IsDelete { get; set; }
        public bool? IsComplete { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeadLine { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }
        public Guid CreateUser { get; set; }
        public Guid? ModifyUser { get; set; }
    }
}
