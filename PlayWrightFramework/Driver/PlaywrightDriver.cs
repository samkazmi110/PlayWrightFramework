using Microsoft.Playwright;
using Microsoft.VisualBasic;
using PlayWrightFramework.Config;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace PlayWrightFramework.Driver
{
    public class PlaywrightDriver : IDisposable
    {
        private readonly TestSettings _testSettings;
        private readonly IPlaywrightDriverInitializer _playwrightDriverInitializer;
        private readonly AsyncTask<IPage> _page;
        private readonly AsyncTask<IBrowser> _browser;
        private readonly AsyncTask<IBrowserContext> _browserContext;
        private bool _isDisposed;

        public Task<IPage> page => _page.Value;
        public Task<IBrowser> browser => _browser.Value;
        public Task<IBrowserContext> browserContext => _browserContext.Value;


        public PlaywrightDriver(TestSettings testSettings, IPlaywrightDriverInitializer playwrightDriverInitializer) 
        {
            this._testSettings = testSettings;
            this._playwrightDriverInitializer = playwrightDriverInitializer;
            _browser = new AsyncTask<IBrowser>(InitializePlaywrightAsync);
            _browserContext = new AsyncTask<IBrowserContext>(CreateBrowserContextAsync);
            _page = new AsyncTask<IPage>(CreatePageAsync);

        }

        private async Task<IBrowser> InitializePlaywrightAsync() 
        {
            return _testSettings.DriverType switch
            {
                DriverType.Chromium => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings),
                DriverType.Chrome => await _playwrightDriverInitializer.GetChromeDriverAsync(_testSettings),
                DriverType.Edge => await _playwrightDriverInitializer.GetEdgeDriverAsync(_testSettings),
                DriverType.FireFox => await _playwrightDriverInitializer.GetFireFoxDriverAsync(_testSettings),
                DriverType.WebKit => await _playwrightDriverInitializer.GetWebKitDriverAsync(_testSettings),
                _ => await _playwrightDriverInitializer.GetChromiumDriverAsync(_testSettings)
            };
        }
        private async Task<IBrowserContext> CreateBrowserContextAsync()
        {
           return await (await _browser).NewContextAsync();
        }

        private async Task<IPage> CreatePageAsync()
        {
            return await (await _browserContext).NewPageAsync();
        }

        public void Dispose()
        {
            if (!_isDisposed) return;
            
                if (_browser.IsValueCreated)
                {
                    Task.Run(async () =>
                    {
                        await (await _browser).CloseAsync();
                        await (await _browser).DisposeAsync();

                    });
                
                }

             _isDisposed = true;
            
        }

       
    }
}
