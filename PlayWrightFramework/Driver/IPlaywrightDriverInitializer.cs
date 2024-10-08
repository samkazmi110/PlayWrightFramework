using Microsoft.Playwright;
using PlayWrightFramework.Config;

namespace PlayWrightFramework.Driver
{
    public interface IPlaywrightDriverInitializer
    {
        Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetFireFoxDriverAsync(TestSettings testSettings);
        Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings);
    }
}