using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    public abstract class Goods
    {
        abstract public string Id { get; }
        abstract public string Brand { get; }
        abstract public decimal Price { get; }
    }
}
