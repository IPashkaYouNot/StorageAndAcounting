using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAndAcounting.BL.Model
{
    [Serializable]
    public class Wines : Goods
    {
        // TODO: add static constructor, that initializes all brands
        public override string Id { get; }

        public override string Brand { get; }

        public override decimal Price { get; }

        // TODO: replace with constructor only with brand and price variables
        public Wines(string id, string brand, decimal price)
        {
            // TODO: add validation
            Id = id;
            Brand = brand;
            Price = price;
        }

        // TODO: add method that checks if brand is correct
        // TODO: add method that initialize id by brand

        /*

        /// <summary>
        /// Info about wine: id and brand.
        /// </summary>
        public override Dictionary<string, string> BrandsDict { get; }

        /// <summary>
        /// Info about wine: id and price.
        /// </summary>
        public override Dictionary<string, double> PricesDict { get; }

        public Wines()
        {
            Dictionary<string, string> brands;
            Dictionary<string, double> prices;
            GetInfo(out brands, out prices);
            BrandsDict = brands;
            PricesDict = prices;
        }

        /// <summary>
        /// Reads all brands from file.
        /// </summary>
        /// <returns> Array of brands with their prices. </returns>
        /// <exception cref="ArgumentException"> Thrown if information in file is wrong. </exception>
        private void GetInfo(out Dictionary<string, string> Brands, out Dictionary<string, double> Prices)
        {
            Brands = new Dictionary<string, string>();
            Prices = new Dictionary<string, double>();
            using(var sr = new StreamReader("VineBrands.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] input = sr.ReadLine().Split();
                    double price;
                    if (input.Length < 3 || !double.TryParse(input[input.Length - 2], out price)) throw new ArgumentException("Error while reading file. Check VineBrands.txt.");
                    string id = input[input.Length - 1];
                    string brand = input[0];
                    for (int i = 1; i < input.Length-2; i++)
                    {
                        brand += " " + input[i];
                    }
                    Brands.Add(id, brand);
                    Prices.Add(id, price);

                }
            }
        }


        /// <param name="brand"> Brand. </param>
        /// <returns> Price of input brand. </returns>
        public override double GetPrice(string brand)
        {
            // TODO: add validation
            return PricesDict[GetId(brand)];
        }


        /// <returns> All brands of Wines. </returns>
        public override List<string> GetBrands()
        {
            return new List<string>(BrandsDict.Values);
        }

        public override string GetId(string brand)
        {
            // TODO: add validation
            return BrandsDict.FirstOrDefault(x => x.Value == brand).Key;
        }
        */
    }
}
