using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.BiDi.Modules.Input;
using OpenQA.Selenium.DevTools.V132.Input;
using OpenQA.Selenium.Interactions;
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;
using TouchPoint = OpenQA.Selenium.DevTools.V131.Input.TouchPoint;

namespace AppiumTests.Automation;

public class Tests
{
	private const string DeviceName = "iPhone 15 Pro Max iOS 17.5";
	private const string DevicePlatformVersion = "17.5";
	private const string DeviceUid = "3A0915CB-48FE-4657-957F-E4F15542105F";
	private const string AppPath = "/Users/danielklokov/Programming/Repos/AppiumTests/AppiumTests.iOS/bin/Debug/net8.0-ios/iossimulator-arm64/AppiumTests.iOS.app";
	private const string WdaPath = "/Users/danielklokov/Library/Developer/Xcode/DerivedData/WebDriverAgent-ddibmffcftnvrcfikoigguraxuzz/Build/Products/Debug-iphoneos/WebDriverAgentRunner-Runner.app";
	private const string ServiceUrl = "http://127.0.0.1:4723";

	private IOSDriver? _driver;

	[SetUp]
	public void Setup()
	{
		AppiumOptions driverOptions = new AppiumOptions
		{
			PlatformName = "ios",
			AutomationName = AutomationName.iOSXcuiTest,
			DeviceName = DeviceName,
			PlatformVersion = DevicePlatformVersion,
			App = AppPath
		};

		driverOptions.AddAdditionalAppiumOption(MobileCapabilityType.Udid, DeviceUid);
		driverOptions.AddAdditionalAppiumOption(MobileCapabilityType.NoReset, true);
		driverOptions.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, false);
		//driverOptions.AddAdditionalAppiumOption(IOSMobileCapabilityType.AutoDismissAlerts, true);
		//driverOptions.AddAdditionalAppiumOption(IOSMobileCapabilityType.AutoAcceptAlerts, true);

		driverOptions.AddAdditionalAppiumOption("usePreinstalledWDA", false);
		driverOptions.AddAdditionalAppiumOption("prebuiltWDAPath", WdaPath);
		driverOptions.AddAdditionalAppiumOption("printPageSourceOnFindFailure", false);

		_driver = new IOSDriver(new Uri($"{ServiceUrl}", UriKind.Absolute), driverOptions, TimeSpan.FromSeconds(60));
		_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
	}

	[TearDown]
	public void TearDown()
	{
		_driver?.Quit();
		_driver?.Dispose();
	}

	[Test]
	public void RunApp_Test()
	{
		// AppiumElement? btn = _driver?.FindElement(By.Id("Show Location Popup"));
		// btn?.Click();
		
		Task.Delay(TimeSpan.FromSeconds(5)).Wait();

		Size screenSize = _driver?.Manage().Window.Size ?? Size.Empty;
		PointerInputDevice inputDevice = new PointerInputDevice(PointerKind.Touch);

		ActionSequence btnTouch = new ActionSequence(inputDevice);
		btnTouch.AddAction(inputDevice.CreatePointerMove(CoordinateOrigin.Viewport, screenSize.Width / 2, screenSize.Height / 2, TimeSpan.FromSeconds(1)));
		btnTouch.AddAction(inputDevice.CreatePointerDown(PointerButton.TouchContact));
		btnTouch.AddAction(inputDevice.CreatePointerUp(PointerButton.TouchContact));

		_driver?.PerformActions([btnTouch]);

		Task.Delay(TimeSpan.FromSeconds(5)).Wait();

		ActionSequence dialogBtnTouch = new ActionSequence(inputDevice);
		dialogBtnTouch.AddAction(inputDevice.CreatePointerMove(CoordinateOrigin.Viewport, screenSize.Width / 2, (screenSize.Height / 3) * 2, TimeSpan.FromSeconds(1)));
		dialogBtnTouch.AddAction(inputDevice.CreatePointerDown(PointerButton.TouchContact));
		dialogBtnTouch.AddAction(inputDevice.CreatePointerUp(PointerButton.TouchContact));

		_driver?.PerformActions([dialogBtnTouch]);

		// AppiumElement? alertBtn = _driver?.FindElement(By.Id("Allow Once"));
		// alertBtn?.Click();

		Assert.Pass();
	}
}