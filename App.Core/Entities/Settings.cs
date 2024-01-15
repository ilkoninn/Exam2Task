using App.Core.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Core.Entities
{
    public class Settings : BaseAuditableEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
