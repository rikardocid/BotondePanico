using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GFDSystems.Vigitech.DAL.Entities.MySQL
{
    [Table("GroupInfo")]
    public class GroupInfo
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("GroupName"), StringLength(50), Required]
        public string GroupName { get; set; }
        [Column("GroupFatherID"), Required]
        public int GroupFatherID { get; set; }
        [Column("Remark"), StringLength(250)]
        public string Remark { get; set; }
    }
}
