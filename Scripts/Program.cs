using StereoKit;
using StereoKit.Framework;


class Program
{
	static void Main(string[] args)
	{
		var trials = SK.GetOrCreateStepper<Trials>(); // Trials and tribulations

		// var surroundings = SK.GetOrCreateStepper<Surroundings>(); // Gotta look good.
		// surroundings.ProvidePassthrough(SK.GetOrCreateStepper<PassthroughFBExt>()); // We're needing some good FB extension! 

		var settings = new SKSettings // Initialize StereoKit
		{
			appName = "sk_demo",
			assetsFolder = "Assets",
		};


		if (!SK.Initialize(settings)) // Don't continue unless SK Initialization has been done!
		{
			return;
		}


		SK.Run(() =>
		{
			// RUN LIKE THE WIND
		});
	}
}