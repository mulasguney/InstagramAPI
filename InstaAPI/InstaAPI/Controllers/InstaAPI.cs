using InstaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace InstaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class InstaApi : ControllerBase
{
    private readonly List<GetInfo> Information = new List<GetInfo>();


    [HttpPost]
    public List<string?> Post(GetInfo info)
    {
        Information.Add(info);
        ChromeOptions options = new ChromeOptions();
        options.AddArgument("--headless=new");
        IWebDriver driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://instagram.com");
        Thread.Sleep(3000);
        IWebElement userName = driver.FindElement(By.Name("username"));
        IWebElement password = driver.FindElement(By.Name("password"));
        IWebElement loginButton = driver.FindElement(By.CssSelector("._acan._acap._acas._aj1-"));
        userName.SendKeys(info.UserName);
        password.SendKeys(info.Password);
        loginButton.Click();
        Thread.Sleep(4000);
        driver.Navigate().GoToUrl($"https://www.instagram.com/" + info.AccountName + "/");
        Thread.Sleep(4000);
        IWebElement openPost = driver.FindElement(By.CssSelector("._aagu"));
        openPost.Click();
        Thread.Sleep(1000);
        IWebElement nextPost = driver.FindElement(By.CssSelector("._aaqg._aaqh"));
        var listOfComment = new List<string?>();

        try
        {
            for (var i = 1; i <= 1000; i++)
            {
                IWebElement scrollDownBtn = driver.FindElement(By.CssSelector(".x9f619.xjbqb8w.x78zum5.x168nmei" +
                                                                              ".x13lgxp2.x5pf9jr.xo71vjh.xdj266r" +
                                                                              ".xat24cr.x1n2onr6.x1plvlek.xryxfnj" +
                                                                              ".x1c4vz4f.x2lah0s.xdt5ytf.xqjyukv" +
                                                                              ".x1qjc9v5.x1oa3qoh.xl56j7k"));
                scrollDownBtn.Click();
                Thread.Sleep(1000);
            }
        }
        catch (Exception)
        {
            // ignored
        }


        finally
        {
            IReadOnlyCollection<IWebElement> comment =
                driver.FindElements(By.CssSelector("._aacl._aaco._aacu._aacx._aad7._aade"));

            foreach (var getcomment in comment)
            {
                if (getcomment.Text.Contains(info.SpecificWord!))
                {
                    listOfComment.Add(getcomment.Text);
                }
            }
        }

        Thread.Sleep(2000);


        try
        {
            for (var a = 1; a < info.PostNumber; a++)
            {
                nextPost.Click();
                Thread.Sleep(1000);
                try
                {
                    var scrollDownBtn = driver.FindElement(By.CssSelector(".x9f619.xjbqb8w.x78zum5.x168nmei" +
                                                                          ".x13lgxp2.x5pf9jr.xo71vjh.xdj266r" +
                                                                          ".xat24cr.x1n2onr6.x1plvlek.xryxfnj" +
                                                                          ".x1c4vz4f.x2lah0s.xdt5ytf.xqjyukv" +
                                                                          ".x1qjc9v5.x1oa3qoh.xl56j7k"));
                    for (var i = 1; i <= 100; i++)
                    {
                        scrollDownBtn.Click();
                        Thread.Sleep(750);
                    }
                }
                catch (Exception)
                {
                    //ignored
                }
                finally
                {
                    IReadOnlyCollection<IWebElement> comment =
                        driver.FindElements(By.CssSelector("._aacl._aaco._aacu._aacx._aad7._aade"));
                    foreach (var cmnt in comment)
                    {
                        if (cmnt.Text.Contains(info.SpecificWord!))
                        {
                            listOfComment.Add(cmnt.Text);
                        }
                    }
                }

                Thread.Sleep(1000);
            }
        }
        catch (Exception)
        {
            // ignored
        }


        return listOfComment;
    }
}