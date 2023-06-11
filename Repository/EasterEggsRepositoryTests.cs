using Microsoft.VisualStudio.TestTools.UnitTesting;
using CharlottesStockApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChocolateLib;

namespace CharlottesStockApi.Repository.Tests
{
    [TestClass()]
    public class EasterEggsRepositoryTests
    {
        EasterEggsRepository _repo = new EasterEggsRepository();
        [TestMethod()]
        public void GetTest()
        {
            List<EasterEgg> eggs = _repo.Get();
            Assert.IsNotNull(eggs);
        }

        [TestMethod()]
        public void GetByProductNoTest()
        {
            EasterEgg GoodEgg = _repo.GetByProductNo(8023);
            Assert.IsTrue(GoodEgg.ChocolateType == "lys");
            EasterEgg BadEgg = _repo.GetByProductNo(8065);
            Assert.IsNull(BadEgg);        
        }

        [TestMethod()]
        public void GetLowStockTest()
        {
            List<EasterEgg> eggs = _repo.GetLowStock(4000);
            int length = eggs.Count;
            Assert.IsTrue(length == 5);
        }

        
    }
}