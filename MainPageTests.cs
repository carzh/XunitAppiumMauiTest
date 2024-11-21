using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using Xunit;

namespace XunitAppiumMauiTest.UITests;

// Add a CollectionDefinition together with a ICollectionFixture
// to ensure that setting up the Appium server only runs once
// xUnit does not have a built-in concept of a fixture that only runs once for the whole test set.
[CollectionDefinition("UITests")]
public sealed class UITestsCollectionDefinition : ICollectionFixture<AppiumSetup>
{

}

// Add all tests to the same collection as above so that the Appium server is only setup once
[Collection("UITests")]
public abstract class BaseTest
{
    protected AppiumDriver App => AppiumSetup.App;

    // This could also be an extension method to AppiumDriver if you prefer
    protected AppiumElement FindUIElement(string id)
    {
        if (App is WindowsDriver)
        {
            return App.FindElement(MobileBy.AccessibilityId(id));
        }

        return App.FindElement(MobileBy.Id(id));
    }
}

// This is an example of tests that do not need anything platform specific
public class MainPageTests : BaseTest
{
    [Fact]
    public void AppLaunches()
    {
        App.GetScreenshot().SaveAsFile($"{nameof(AppLaunches)}.png");
    }

    [Fact]
    public async void ClickCounterTest()
    {
        // Arrange
        // Find elements with the value of the AutomationId property
        var element = FindUIElement("CounterBtn");

        // Act
        element.Click();
        await Task.Delay(500); // Wait for the click to register and show up on the screenshot

        // Assert
        App.GetScreenshot().SaveAsFile($"{nameof(ClickCounterTest)}.png");
        Assert.Equal("Clicked 1 time", element.Text);
    }
}