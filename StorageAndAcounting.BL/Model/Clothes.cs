using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    public class Clothes : Goods
    {
        public static Dictionary<string, decimal> PricesByBrands { get; }

        static Dictionary<string, string> IdsByBrands { get; }

        static Clothes()
        {
            GetInfo(out Dictionary<string, decimal> tempPrices, out Dictionary<string, string> tempIds);
            PricesByBrands = tempPrices;
            IdsByBrands = tempIds;
        }

        static private void GetInfo(out Dictionary<string, decimal> prices, out Dictionary<string, string> ids)
        {
            prices = new Dictionary<string, decimal>();
            ids = new Dictionary<string, string>();

            using (var sr = new StreamReader("ClothesInfo.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] row = sr.ReadLine().Split();
                    if (row.Length < 3) throw new ArgumentException("Wrong clothes file.");
                    if (!decimal.TryParse(row[row.Length - 2], out decimal price)) throw new ArgumentException("Wrong clothes file input.");
                    string id = row[row.Length - 1];
                    string brand = row[0];
                    for (int i = 1; i < row.Length - 2; i++)
                    {
                        brand += " " + row[i];
                    }
                    prices.Add(brand, price);
                    ids.Add(brand, id);
                }
            }

        }

        public override string Id { get; }

        public override string Brand { get; }

        public override decimal Price { get; }

        public Clothes(string brand)
        {
            if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentNullException("Brand is NULL.");

            if (!IdsByBrands.TryGetValue(brand, out string id)) throw new ArgumentException("Product was not found.");

            Id = id;
            Brand = brand;
            Price = PricesByBrands[brand];
        }
    }
}
