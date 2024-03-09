using System;
using StereoKit;
using StereoKit.Framework;

public class Surroundings : IStepper
{
	#region Variables and such

	Tex[] _skyboxes;

	PassthroughFBExt _passthroughExtension;
	Tex _passthroughTexture;
	Pose _buttonPose;

	Model _model;
	Pose _modelPose;
	float _modelScale;

	Random _randomValues;

	bool _initialized;
	bool _hasPassthrough;
	public bool Enabled
	{
		get
		{
			return _initialized == true && _hasPassthrough == true;
		}
	}

	#endregion


	#region Initialisation

	public bool Initialize()
	{
		_passthroughTexture = Renderer.SkyTex; // Store the original skybox for later use

		_skyboxes = new[] { // Give me a 'list' 
			Tex.FromCubemapEquirectangular("skybox_1.hdr"), // Add all these lovely .hdr skyboxes to it
			Tex.FromCubemapEquirectangular("skybox_2.hdr"), // You can find them on PolyHaven!
			Tex.FromCubemapEquirectangular("skybox_3.hdr"), // Store all your assets in the Assets folder
			Tex.FromCubemapEquirectangular("skybox_4.hdr"),
			Tex.FromCubemapEquirectangular("skybox_5.hdr")
		};

		_buttonPose = new Pose(-0.5f, 0f, -0.1f, Quat.LookDir(1, 0, 0)); // This is where my button should be

		_model = Model.FromFile("DamagedHelmet.glb"); // SK works great with .glb and .gltf --> have a look at the Smithsonian's object library! 
		_modelPose = new Pose(0, 0.5f, -0.3f); // Put the thing half a meter above my head, and 30cm in front of my face. 
		_modelScale = 0.1f; // This is really model-dependent. Some are a little small and need scaling up. This particular model is about 10x larger than I'd like.

		_randomValues = new Random(); // More random is more better

		return _initialized = true;
	}


	/// <summary>
	/// This is here because it needs to be called from the Program::Main() method, *before* the rest of StereoKit is initialised.
	/// Therefore I need to GetOrCreateStepper this IStepper and the PassthroughFBExt IStepper first, and then use this method to provide
	/// the resulting thing this way. It's a little hacky, but I wanted to have all code dealig with "the visual surroundings" in one class. This class.
	/// </summary>
	/// <param name="passthroughExt"></param>
	public void ProvidePassthrough(PassthroughFBExt passthroughExt)
	{
		_passthroughExtension = passthroughExt;
		_hasPassthrough = true;
	}

	#endregion


	#region Step every frame

	/// <summary>
	/// This gets called every frame by the magic of the IStepper
	/// </summary>
	public void Step()
	{
		DrawPassthroughUIToggler();
		DrawModel();
	}


	/// <summary>
	/// Clicky click the button.
	/// VR. MR. VR. MR. VR. MR.
	/// </summary>
	void DrawPassthroughUIToggler()
	{
		UI.WindowBegin("Passthrough Toggler", ref _buttonPose); // Gimme a window...
		if (UI.Button("Passthrough")) // ... which has a button on it. If I pressed this button... 
		{
			if (_passthroughExtension.Enabled == true) // ... and passthrough was on when I pressed the button...
			{
				_passthroughExtension.Shutdown(); // ... then I want to use the passthrough's IStepper's "Shutdown" method to stop the passthrough functionality...
				Renderer.SkyTex = _skyboxes[_randomValues.Next(_skyboxes.Length)]; // ... and use a random skybox instead! We're in VR! Note the reflections!!!
			}
			else if (_passthroughExtension.Enabled == false) // If I was already in VR when I pressed the button... 
			{
				_passthroughExtension.Initialize(); // ... then I want to use the IStepper's "Initialize" function to start passthrough...
				Renderer.SkyTex = _passthroughTexture; // ... and I want to set the skybox texture to a neutral one, because otherwise you keep seeing the reflections of the previous skybox!
			}
		}
		UI.WindowEnd(); // This ends here.
	}


	/// <summary>
	/// This draws the model that we loaded from file earlier, and allows you to grab it!
	/// </summary>
	void DrawModel()
	{
		var scaledBounds = _model.Bounds * _modelScale; // Making sure that the 'grabbable area' of our model is the same size as the visuals of our model
		UI.Handle("Model handle", ref _modelPose, scaledBounds);
		_model.Draw(_modelPose.ToMatrix(_modelScale));
	}

	#endregion


	#region Shutdown

	/// <summary>
	/// We're not really using it, but a Shutdown method is required by the IStepper interface. Not very ISP :P! 
	/// </summary>
	public void Shutdown()
	{
		_initialized = false;
		_hasPassthrough = false;
	}

	#endregion
}