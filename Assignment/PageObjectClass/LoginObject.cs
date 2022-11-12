using Assignment.CommonMethods;
using Assignment.PageObjectClass;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Assignment
{
    public class LoginObject
    {
        public LoginObject()
        {
            PageFactory.InitElements(GlobalVariables.Driver, this);
        }

        [FindsBy(How = How.Id, Using = "user-name")]
        public IWebElement UserName { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement Password { get; set; }

        [FindsBy(How = How.Id, Using = "login-button")]
        public IWebElement LoginBtn { get; set; }

        public ProductObject LauncApplication(string username, string pasword)
        {
            UserName.SendKeys(username);
            Password.SendKeys(pasword);
            LoginBtn.Click();

            return new ProductObject();
        }
        public string GetTitle()
        {
            return GlobalVariables.Driver.Url;
        }
    }
}
