﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace XunitAppiumMauiTest.UITests;
public partial class AppiumSetup
{
    private static AppiumDriver? driver;

    public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

    public AppiumSetup()
    {
        // If you started an Appium server manually, make sure to comment out the next line
        // This line starts a local Appium server for you as part of the test run
        AppiumServerHelper.StartAppiumLocalServer();

        var androidOptions = new AppiumOptions
        {
            // Specify windows as the driver, typically don't need to change this
            AutomationName = "UIAutomator2",
            // Always Windows for Windows
            PlatformName = "Android",
            //PlatformVersion = "17.0",
            //// The identifier of the deployed application to test
            //App = "ORT.CSharp.Tests.MAUI",
        };

        // Note there are many more options that you can use to influence the app under test according to your needs

        driver = new AndroidDriver(androidOptions);
    }

    public void Dispose()
    {
        driver?.Quit();

        // If an Appium server was started locally above, make sure we clean it up here
        AppiumServerHelper.DisposeAppiumLocalServer();
    }
}
