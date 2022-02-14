using NUnit.Framework;
using OpenQA.Selenium;
using SystemTests.Pages;
using SystemTests.Lib;

namespace SystemTests.Tests
{
    public class Tests
    {
        private IWebDriver _driver;
  
        //1.Go to https://www.google.com/maps
        //2.Enter Dublin in the search box
        //3.Search
        //4.Verify left panel has "Dublin" as a headline text
        //5.Click Directions icon
        //6.Verify destination field is "Dublin"
        [Test]
        [TestCase("Chrome", "Dublin")]
        [TestCase("Firefox", "Dublin")]
        public void ValidateDirection(string browser, string location)
        {
            // 1
            _driver = Driver.OpenDriver(browser);
            _driver.Navigate().GoToUrl("https://www.google.com/maps");

            HomePage home = new HomePage(_driver);
            home.acceptCookies();

            // 2 # 3 # 4 
            IWebElement searchLocation = home.searchLocation(location);
            Assert.That(searchLocation.Displayed, $"Cannot validate that Left panel has {location} element text visible!");
            StringAssert.Contains("headline", searchLocation.GetAttribute("class"), $"Cannot validate that Left panel has {location} as headline text!");

            // 5 # 6
            Assert.IsTrue(home.locationVisible(location), $"Cannot validate that direction '{location}' is on destination field! ");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}