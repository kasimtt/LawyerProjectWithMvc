using LawyerProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Entities
{
    public class BaseEntity
    {
        public int ObjectId { get; set; }
        public DateTime CreatedDate { get; set; }
        virtual public DateTime? UpdatedDate { get; set; }
        public DataState DataState { get; set; } = DataState.Active;
        //BaseEntity ozelliklerini configurasyonlara eklemeyi unutma panpa

    }
}
