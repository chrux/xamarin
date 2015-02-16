using System;
using Foundation;
using UIKit;
using System.Collections.Generic;

namespace Phoneword_iOS
{
	public partial class Phoneword_iOSViewController : UIViewController
	{
		string translatedNumber = "";

		public List<String> PhoneNumbers { get; set; }

		public Phoneword_iOSViewController (IntPtr handle) : base (handle)
		{
			//initialize list of phone numbers called for Call History screen
			PhoneNumbers = new List<String> ();
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
				//Store the phone number that we're dialing in PhoneNumbers
				PhoneNumbers.Add (translatedNumber);

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

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			// set the View Controller that’s powering the screen we’re
			// transitioning to

			var callHistoryContoller = segue.DestinationViewController as CallHistoryController;

			//set the Table View Controller’s list of phone numbers to the
			// list of dialed phone numbers

			if (callHistoryContoller != null) {
				callHistoryContoller.PhoneNumbers = PhoneNumbers;
			}
		}
	}
}

