using Microsoft.Playwright;
using PlayWrightFramework.Config;

namespace PlayWrightFramework.Driver
{
    public class PlaywrightDriverInitializer : IPlaywrightDriverInitializer
    {
        public const float DEFAULT_TIMEOUT = 30f;
        public async Task<IBrowser> GetChromeDriverAsync(TestSettings testSettings)
        {
            var options = GetParameter(testSettings.args, testSettings.timeout, testSettings.headless, testSettings.slowMo);
            options.Channel = "chrome";
            return await GetBrowserAsync(DriverType.Chromium, options);
        }

        public async Task<IBrowser> GetFireFoxDriverAsync(TestSettings testSettings)
        {
            var options = GetParameter(testSettings.args, testSettings.timeout, testSettings.headless, testSettings.slowMo);
            options.Channel = "firefox";
            return await GetBrowserAsync(DriverType.FireFox, options);
        }

        public async Task<IBrowser> GetEdgeDriverAsync(TestSettings testSettings)
        {
            var options = GetParameter(testSettings.args, testSettings.timeout, testSettings.headless, testSettings.slowMo);
            options.Channel = "msedge";
            return await GetBrowserAsync(DriverType.Chromium, options);
        }

        public async Task<IBrowser> GetWebKitDriverAsync(TestSettings testSettings)
        {
            var options = GetParameter(testSettings.args, testSettings.timeout, testSettings.headless, testSettings.slowMo);
            options.Channel = string.Empty;
            return await GetBrowserAsync(DriverType.WebKit, options);
        }

        public async Task<IBrowser> GetChromiumDriverAsync(TestSettings testSettings)
        {
            var options = GetParameter(testSettings.args, testSettings.timeout, testSettings.headless, testSettings.slowMo);
            options.Channel = string.Empty;
            return await GetBrowserAsync(DriverType.Chromium, options);
        }


        private async Task<IBrowser> GetBrowserAsync(DriverType driverType, BrowserTypeLaunchOptions options)
        {
            var playwright = await Playwright.CreateAsync();
            return await playwright[driverType.ToString().ToLower()].LaunchAsync(options);

        }

        private BrowserTypeLaunchOptions GetParameter(string[]? args, float? timeout = DEFAULT_TIMEOUT, bool? headless = true, float? slowmo = 1500)
        {
            return new BrowserTypeLaunchOptions
            {
                Args = args,
                Timeout = ToMilliseconds(timeout),
                Headless = headless,
                SlowMo = slowmo
            };
        }

        private static float? ToMilliseconds(float? seconds)
        {
            return seconds * 1000;
        }
    }
}
