using TestAutomation.Framework.DomainLayer.Models.ValueObjects;
using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace TestAutomation.Framework.InfrastructureLayer.Utils
{
    public static class LocatorConverter
    {

        public static By ToBy(Locator locator)
        {
            if (locator == null || string.IsNullOrEmpty(locator.By))
             {
                throw new
                    ArgumentNullException(
                    "Element bulunamadı. Locator isimlendirmesi kontrol edilmeli. Mevcut locator tanımı : "
                    + locator);
            }
            else if (Regex.IsMatch(locator.By, "^(xpath=|/)(.*)"))
            {
                return By.XPath(new Regex("^xpath=").Replace(locator.By, ""));
            }
            else if (Regex.IsMatch(locator.By, "^id=(.*)")) {
                return By.Id(locator.By.Substring("id=".Length));
            }
            else if (Regex.IsMatch(locator.By, "^name=(.*)")) {
                return By.Name(locator.By.Substring("name=".Length));
            }
            else if (Regex.IsMatch(locator.By, "^css=(.*)")) {
                return By.CssSelector(locator.By.Substring("css=".Length));
            }
            else if (Regex.IsMatch(locator.By, "^class=(.*)")) {
                return By.ClassName(locator.By.Substring("class=".Length));
            }
            else if (Regex.IsMatch(locator.By, "^tagName=(.*)")) {
                return By.TagName(locator.By.Substring("tagName=".Length));
            }
            else if (Regex.IsMatch(locator.By, "^aid=(.*)")) {
                return ByWindowsAutomation.AccessibilityId(locator.By.Substring("aid=".Length));
            }
            else if (!locator.By.Contains("=")) {
                return By.Id(locator.By);
            }
            else
            {
                throw new
                    InvalidOperationException(
                    "Element bulunamadı. Locator isimlendirmesi kontrol edilmeli. Mevcut locator tanımı : "
                    + locator.By);   
            }         
        }
    }
}
