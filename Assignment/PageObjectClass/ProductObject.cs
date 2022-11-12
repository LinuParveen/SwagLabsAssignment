using Assignment.CommonMethods;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;

namespace Assignment.PageObjectClass
{
    public class ProductObject
    {
        public ProductObject()
        {
            PageFactory.InitElements(GlobalVariables.Driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//div[@class=\"inventory_item\"]/div/a")]
        public IList<IWebElement> Products { get; set; }

        [FindsBy(How = How.Id, Using = "back-to-products")]
        public IWebElement BackToProductBtn { get; set; }

        [FindsBy(How = How.ClassName, Using = "btn_inventory")]
        public IWebElement AddToCart { get; set; }

        [FindsBy(How = How.ClassName, Using = "shopping_cart_badge")]
        public IWebElement ShoppingCart { get; set; }
        List<int> GetRandomNumbers(int numbersRequired, int maxLimit)
        {
            var randomNumbers = new List<int>();
            var rnd = new Random();
            int randomValue;

            while (randomNumbers.Count < numbersRequired)
            {
                randomValue = rnd.Next(1, maxLimit);
                if (!randomNumbers.Contains(randomValue))
                {
                    randomNumbers.Add(randomValue);
                }
            }

            return randomNumbers;
        }

        public void ClickOnRandomProducts()
        {
            var randomNumbers = GetRandomNumbers(3,Products.Count);
              
            foreach (var i in randomNumbers)
            {
                Products[i].Click();
                AddToCart.Click();
                BackToProductBtn.Click();
            }
        }

        public string  GetShoppingCartItems()
        {
            return ShoppingCart.Text;
        }
    }
}
