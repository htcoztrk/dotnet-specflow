using OpenQA.Selenium;
using System;
using System.Text.RegularExpressions;


namespace Intertech.TestAutomation.Framework.InfrastructureLayer.Services
{
    public static class LocatorService
    {
        
        public static By ConvertStringToBy(String input)
        {
            if (Regex.IsMatch(input, "^(xpath=|/)(.*)"))
            {
                return By.XPath(new Regex("^xpath=").Replace(input, ""));
            }
            else if (Regex.IsMatch(input, "^id=(.*)"))
            {
                return By.Id(input.Substring("id=".Length));
            }
            else if (Regex.IsMatch(input, "^name=(.*)"))
            {
                return By.Name(input.Substring("name=".Length));
            }
            else if (Regex.IsMatch(input, "^css=(.*)"))
            {
                return By.CssSelector(input.Substring("css=".Length));
            }
            else if (Regex.IsMatch(input, "^class=(.*)"))
            {
                return By.ClassName(input.Substring("class=".Length));
            }
            else
            {
                return By.Id(input);
            }
        }
    }
}
