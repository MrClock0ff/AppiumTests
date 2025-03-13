using CoreLocation;

namespace AppiumTests.iOS;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate, ICLLocationManagerDelegate
{
	public override UIWindow? Window { get; set; }

	public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow(UIScreen.MainScreen.Bounds);

		// create a UIViewController with a single UIButton
		var btn = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };
		btn.SetTitle("Show Location Popup", UIControlState.Normal);
		btn.TouchUpInside += Btn_OnTouchUpInside;
		var vc = new UIViewController();
		vc.View!.AddSubview(btn);
		NSLayoutConstraint.ActivateConstraints([
			btn.CenterXAnchor.ConstraintEqualTo(vc.View.CenterXAnchor),
			btn.CenterYAnchor.ConstraintEqualTo(vc.View.CenterYAnchor),
		]);
		Window.RootViewController = vc;

		// make the window visible
		Window.MakeKeyAndVisible();

		return true;
	}

	private void Btn_OnTouchUpInside(object? sender, EventArgs e)
	{
		var cllManager = new CLLocationManager
		{
			Delegate = this,
			DesiredAccuracy = CLLocation.AccurracyBestForNavigation,
			ActivityType = CLActivityType.AutomotiveNavigation
		};

		switch (cllManager.AuthorizationStatus)
		{
			case CLAuthorizationStatus.NotDetermined:
				cllManager.RequestAlwaysAuthorization();
				break;
			default:
				break;
		}
	}
}