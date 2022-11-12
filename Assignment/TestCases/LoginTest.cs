using Assignment.CommonMethods;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Threading;

namespace Assignment
{
    [TestClass]
    public class LoginTest
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {
            var fileName = Path.GetFullPath("TestCaseData\\TestData.xlsx");
            GlobalVariables.Driver = new ChromeDriver();
            GlobalVariables.Driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            GlobalVariables.Driver.Manage().Window.Maximize();
            Thread.Sleep(100);
            ExcelUtility.PopulateInCollection(fileName);
        }

        [TestMethod]
        public void TestCase_LoginToAppliccationAndSelectItems()
        {
            var login = new LoginObject();
            var productPage = login.LauncApplication(ExcelUtility.ReadData(1, "UserName"), ExcelUtility.ReadData(1, "Password"));
            productPage.ClickOnRandomProducts();
            // The assert is based on the pre-condition that the shopping cart is empty for the logged in user
            Assert.AreEqual("3", productPage.GetShoppingCartItems(), "3 items not selected");
        }

        [TestCleanup()]
        public void MyTestCleanUp()
        {
            GlobalVariables.Driver.Quit();
        }
    }
}
