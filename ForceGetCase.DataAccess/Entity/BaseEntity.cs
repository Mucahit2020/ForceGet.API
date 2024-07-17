using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.DataAccess.Entity
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Modes: BaseEntity { }
    public class MovementTypes : BaseEntity { }
    public class Incoterms : BaseEntity { }
    public class Countries : BaseEntity { }
    public class PackageTypes : BaseEntity { }
    public class Unit1 : BaseEntity { }
    public class Unit2 : BaseEntity { }
    public class Currency : BaseEntity { }

}
