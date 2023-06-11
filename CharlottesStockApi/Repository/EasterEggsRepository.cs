using ChocolateLib;
using System.Security.Cryptography.X509Certificates;

namespace CharlottesStockApi.Repository
{
    public class EasterEggsRepository
    {
        private readonly List<EasterEgg> Data;
        public EasterEggsRepository() 
        {
            Data = new List<EasterEgg>
            {
                new EasterEgg{ProductNo = 8011, ChocolateType = "mørk", Price = 28, InStock = 5012},
                new EasterEgg{ProductNo = 8012, ChocolateType = "mørk", Price = 32, InStock = 3420},
                new EasterEgg{ProductNo = 8013, ChocolateType = "mørk", Price = 46, InStock = 1180},
                new EasterEgg{ProductNo = 8022, ChocolateType = "lys", Price = 31, InStock = 2870},
                new EasterEgg{ProductNo = 8023, ChocolateType = "lys", Price = 41, InStock = 1067},
                new EasterEgg{ProductNo = 8032, ChocolateType = "hvid", Price = 34, InStock = 2017},
            };
        }
        public List<EasterEgg> Get() 
        { 
            List<EasterEgg> data = Data;
            return data;
        }
        public EasterEgg GetByProductNo(int productNo)
        {
            try
            {
                foreach (EasterEgg e in Data)
                {
                    if (e.ProductNo == productNo)
                    {
                        return e;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong", ex);
            }
        }
        public List<EasterEgg> GetLowStock(int stockLevel) 
        {
            List<EasterEgg> stockList = new List<EasterEgg>();
            foreach (EasterEgg e in Data) 
            {
                if (e.InStock < stockLevel)
                {
                    stockList.Add(e);
                }
            }
            return stockList;
        }
        public EasterEgg Update(EasterEgg update) 
        {
            EasterEgg easterEgg = GetByProductNo(update.ProductNo);
            if (easterEgg == null) return null;
            easterEgg.ProductNo = update.ProductNo;
            easterEgg.ChocolateType = update.ChocolateType;
            easterEgg.Price = update.Price;
            easterEgg.InStock = update.InStock;
            return easterEgg;
        }
    }
}
