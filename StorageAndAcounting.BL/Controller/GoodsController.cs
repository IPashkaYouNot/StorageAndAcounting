using StorageAndAcounting.BL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Controller
{
    public enum Items
    {
        Wine = 01,
        Cheese = 02,
        Clothes = 03
    }
    public class GoodsController
    {

        static public List<string> GetBrands(Items item)
        {
            return GoodsPrices[item];
        }

        static public int GetAmount(Items item)
        {
            return GoodsPrices[item].Count;
        }

        public static Goods CreateItem(Items item, string brand)
        {
            switch (item)
            {
                case Items.Wine:
                    return new Wines(brand);
                case Items.Cheese:
                    return new Cheeese(brand);
                case Items.Clothes:
                    return new Clothes(brand);
                default:
                    throw new ArgumentException("Item was not found.");
            }
        }

        static private Dictionary<Items, List<string>> GoodsPrices { get; set; }

        static GoodsController()
        {
            SetValues();
        }

        static public string GetBrand(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentNullException("Id is empty or null.");
            int _id = int.Parse(new String(new char[] { id[0], id[1] }));
            string num = string.Empty;
            for (int i = 2; i < id.Length; i++)
            {
                num += id[i];
            }

            int _number = int.Parse(num);
            int p = 1;
            foreach (var item in GoodsPrices[(Items)_id])
            {
                if (p == _number) return item;
                p++;
            }
            throw new ArgumentException("Brand was not found.");
        }

        private static void SetValues()
        {
            GoodsPrices = new Dictionary<Items, List<string>>();
            using(var sr = new StreamReader("Brands.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] row = sr.ReadLine().Split();
                    int id = int.Parse(new String(new char[] { row[row.Length - 1][0], row[row.Length - 1][1] }));

                    if (!GoodsPrices.TryGetValue((Items)id, out _)) GoodsPrices.Add((Items)id, new List<string>());

                    string brand = row[0];

                    for (int i = 1; i < row.Length-1; i++)
                    {
                        brand += " " + row[i];
                    }

                    GoodsPrices[(Items)id].Add(brand);

                }

            }
        }
    }
}
