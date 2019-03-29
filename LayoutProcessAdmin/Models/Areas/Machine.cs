using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayoutProcessAdmin.Models.Areas
{
    [Table("Tbl_Machines")]
    public partial class Machine
    {
        [Key]
        public int int_IdMach { get; set; }

        [Required]
        [StringLength(35)]
        public string chr_Key { get; set; }

        [StringLength(50)]
        public string chr_Description { get; set; }

        [StringLength(20)]
        public string chr_Icon { get; set; }

        [StringLength(10)]
        public string chr_Color { get; set; }

        [Required]
        [StringLength(1)]
        public string chr_Status { get; set; }

        public bool bit_Active { get; set; }

        [StringLength(100)]
        public string chr_Watcher { get; set; }

        [StringLength(15)]
        public string chr_Supervisors { get; set; }

        public DateTime? online { get; set; }

        public int? int_IdPartDef { get; set; }

        [StringLength(500)]
        public string int_IdTurnDef { get; set; }

        [StringLength(10)]
        public string chr_CtrlAdd { get; set; }

        [StringLength(100)]
        public string chr_RotateLocal { get; set; }

        public int? int_Number { get; set; }

        [StringLength(1)]
        public string chr_TypeOper { get; set; }

        [StringLength(800)]
        public string chr_AndonRefresh { get; set; }

        public double? flt_PcsActualToolkit { get; set; }

        public double? flt_PcsChangeToolkit { get; set; }

        [StringLength(30)]
        public string chr_Reference { get; set; }

        public List<Area> Areas { get; set; }
    }
}
