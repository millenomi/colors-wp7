# About this sample

As noted in [Ars Technica's ultra-in-depth review by Peter Bright][1], Windows Phone 7 has an annoying oversight in its navigation model: if you navigate away from an application, and then return to it, the application will most often not have retained state -- you will be back at its main page (usually a hub).

This repository contains an example of a possible solution to that problem that third-party apps can easily implement:

* If the application is started (ie not tombstoned), and

* the last time the application was left there was any work open, then

	* from the main page, navigate to a page that reopens work in the exact same state left.

You can try this out with this application, "Colors" -- this app allows you to open (and admire!) a single color at any one time from its selection list. If you leave the app with a color open, and reopen it, it will return to that color with a message telling you that you can see your other colors by hitting Back.

You can try it out like this:

* Install and open Colors.

* Pick any color.

* On the color screen, press Start.

* Reopen Colors.

**What this app is:** a minimal proof-of-concept of the above interaction pattern.

**What this app is NOT:** a well-coded, idiomatic app. It's my second Windows Phone 7 app, and I'm learning along the way. (Do feel free to criticize any part of it -- [contact me](http://infinite-labs.net/me) or open a bug on this project.)

[1]: http://arstechnica.com/microsoft/reviews/2010/10/windows-phone-7-the-ars-review.ars/6