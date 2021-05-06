﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    /*public interface IGoods
    {
        double GetPrice(string s);
        List<string> GetBrands();
    }*/

    [Serializable]
    public abstract class Goods
    {
        abstract public string Id { get; }
        abstract public string Brand { get; }
        abstract public decimal Price { get; }
        /*
        abstract public Dictionary<string, string> BrandsDict { get; }
        abstract public Dictionary<string, double> PricesDict { get; }

        abstract public List<string> GetBrands();

        abstract public double GetPrice(string info);
        abstract public string GetId(string brand);
        */
    }
}
