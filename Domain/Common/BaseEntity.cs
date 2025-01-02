using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity { }
    public class BaseEntity<T> : BaseEntity
    {
        public T? Id { get; set; }
    }
}
