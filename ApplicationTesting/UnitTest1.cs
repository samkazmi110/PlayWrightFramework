using Microsoft.Playwright;
using PlayWrightFramework.Config;
using PlayWrightFramework.Driver;

namespace ApplicationTesting
{
    [Parallelizable(ParallelScope.All)]
    public class Tests 
    {

        private PlaywrightDriver _playwrightDriver;
        private PlaywrightDriverInitializer _initializer;
        private TestSettings testSettings;
        private IPage page;
        
        [SetUp]
        public async Task Setup()
        {
            testSettings = ConfigReader.ReadConfig();
            _initializer = new PlaywrightDriverInitializer();
            _playwrightDriver = new PlaywrightDriver(testSettings, _initializer);
            page = await _playwrightDriver.page;
            await page.GotoAsync(testSettings.ApplicationUrl);
        }

        [Test]
        public async Task Test1()
        {
         
            await page.ClickAsync("text=Login");


        }

        [Test]
        public async Task LoginTest()
        {

            await page.ClickAsync("text=Login");
            await page.GetByLabel("Username").FillAsync("admin");
            await page.GetByLabel("Password").FillAsync("password");


        }

       

        //[TearDown] 
        //public async Task TearDown() 
        //{
        //    var browser = await _playwrightDriver.browser;
        //    await browser.CloseAsync();
        //    await browser.DisposeAsync();
        //}
    }
}