using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;

namespace TypingTest
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver("./");
            driver.Url = "https://www.typingtest.com/test.html?minutes=5&textfile=aesop.txt";
            driver.Navigate();
            Actions builder = new Actions(driver);
            var element = driver.FindElement(By.ClassName("test-text-area"));
            var children = element.FindElements(By.XPath(".//*"));
            List<string> words = new List<string>();

            foreach (var child in children)
            {
                words.Add(child.GetAttribute("innerText"));
            }

            foreach (var word in words)
            {
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == '↩')
                    {
                        continue;
                    }
                    builder = builder.SendKeys(word[i].ToString());
                }
                builder = builder.SendKeys(Keys.Space);
            }

            while (driver.FindElement(By.ClassName("test-timer")).GetAttribute("innerText") != "0:00")
            {
                builder.Build().Perform();
            }
        }
    }
}
