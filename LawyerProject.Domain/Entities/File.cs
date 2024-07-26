using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Entities
{
    public class File : BaseEntity
    {


        public string FileName { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Storage { get; set; } = string.Empty;
        [NotMapped] //file entitylerinde EntityFramework tarafından(evet tarafından) migration oluşturulurken mapped edilmesini istemiyorum
        public override DateTime? UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }
    }
}
