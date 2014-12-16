using System;
using System.Drawing;

using Foundation;
using UIKit;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			string translatedNumber = "";

			TranslateButton.TouchUpInside += (object sender, EventArgs e) => {
				// Convert the phone number with text to a number
				// using PhonewordTranslator
				translatedNumber = Core.PhonewordTranslator.ToNumber(PhoneNumberText.Text);

				// Dismiss the keyboard if text field was tapped
				PhoneNumberText.ResignFirstResponder();

				if (translatedNumber == "") {
					callButton.SetTitle ("Call", UIControlState.Normal);
					callButton.Enabled = false;
				} else {
					callButton.SetTitle ("Call " + translatedNumber, UIControlState.Normal);
					callButton.Enabled = true;
				}
			};

			callButton.TouchUpInside += (object sender, EventArgs e) => {
				var url = new NSUrl("tel:" + translatedNumber);

				// Use URL handler with tel: prefix to invoke Apple's Phone app,
				// otherwhise show an alert dialog

				if (!UIApplication.SharedApplication.OpenUrl(url)) {
					var av = new UIAlertView("Not Supported",
						"Scheme 'tel:' is not supported on this device",
						null,
						"OK",
						null);

					av.Show();
				}
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion
	}
}

