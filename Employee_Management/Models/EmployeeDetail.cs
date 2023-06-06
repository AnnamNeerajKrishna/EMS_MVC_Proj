using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management.Models
{
    [Table("Employee_Details")]
    public partial class EmployeeDetail
    {
        [Key]
        [Column("Emp_Id")]
        public int EmpId { get; set; }
        [Column("Emp_FirstName")]
        [StringLength(50)]
        [Unicode(false)]
        public string EmpFirstName { get; set; } = null!;
        [Column("Emp_LastName")]
        [StringLength(50)]
        [Unicode(false)]
        public string? EmpLastName { get; set; }
        [Column("Emp_Email_ID")]
        [StringLength(50)]
        [Unicode(false)]
        public string EmpEmailId { get; set; } = null!;
        [Column("Emp_Role")]
        [StringLength(50)]
        [Unicode(false)]
        public string EmpRole { get; set; } = null!;
        [StringLength(50)]
        [Unicode(false)]
        public string? Experience { get; set; }
    }
}
