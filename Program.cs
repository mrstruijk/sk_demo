using System;
using System.Collections.Generic;
using System.IO;
using StereoKit;
using StereoKit.Framework;


class Program
{
	static void Main(string[] args)
	{
		var passthrough = SK.GetOrCreateStepper<PassthroughFBExt>();
		var passthroughTogglePose = new Pose(-0.5f, 0f, -0.1f, Quat.LookDir(1, 0, 0));

		// Initialize StereoKit
		var settings = new SKSettings
		{
			appName = "sk_demo",
			assetsFolder = "Assets",
			blendPreference = DisplayBlend.Additive
		};

		if (!SK.Initialize(settings))
		{
			return;
		}

		var materials = new[] {
			InitNewMaterial("dog_1.png"),
			InitNewMaterial("dog_2.png"),
			InitNewMaterial("dog_3.png"),
			InitNewMaterial("cat_1.png"),
			InitNewMaterial("cat_2.png"),
			InitNewMaterial("cat_3.png")
		};

		var skyboxes = new[] {
			Tex.FromCubemapEquirectangular("skybox_1.hdr"),
			Tex.FromCubemapEquirectangular("skybox_2.hdr"),
			Tex.FromCubemapEquirectangular("skybox_3.hdr"),
			Tex.FromCubemapEquirectangular("skybox_4.hdr"),
			Tex.FromCubemapEquirectangular("skybox_5.hdr")
		};

		var focus = InitNewMaterial("focus.gif");

		// Create assets used by the app
		var cubePose = new Pose(0, 0, -0.5f);
		var cube = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), focus);

		var planePose = new Pose(0, 0, -0.5f, Quat.LookDir(0, 1, 0));
		var planeMesh = Mesh.GeneratePlane(Vec2.One * 0.5f);
		var plane = Model.FromMesh(planeMesh, materials[0]);

		var modelPose = new Pose(0, 0.5f, -0.3f);
		var model = Model.FromFile("DamagedHelmet.glb");

		var floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
		var floorMaterial = new Material("floor.hlsl")
		{
			Transparency = Transparency.Blend
		};

		var focusTimer = 0f;
		var moveTime = 0f;


		var initialDistance = 0f;
		var initialDistanceSet = false;
		var currentDistance = 0f;

		const float MOVE_DISTANCE = 0.30f; // Meters
		const float FOCUS_TIME = 3f; // Seconds

		var passThroughTexture = Renderer.SkyTex;

		var rand = new Random();
		var dataVault = new DataVault();

		// Core application loop
		SK.Run(() =>
		{
			if (SK.System.displayType == Display.Opaque)
			{
				// Mesh.Cube.Draw(floorMaterial, floorTransform);
			}

			if (focusTimer <= FOCUS_TIME)
			{
				initialDistanceSet = false;

				cube.Draw(cubePose.ToMatrix());
			}
			else
			{
				currentDistance = Vec3.Distance(Input.Head.position, cubePose.position);

				if (initialDistanceSet == false)
				{
					initialDistance = currentDistance;
					initialDistanceSet = true;
				}

				if (initialDistance - currentDistance > MOVE_DISTANCE) // Approach
				{
					dataVault.StoreData(moveTime);

					focusTimer = 0f;
					moveTime = 0f;

					plane.Visuals[0].Material = materials[rand.Next(materials.Length)];
				}
				else
				{
					plane.Draw(planePose.ToMatrix());
					moveTime += Time.Stepf;
				}
			}

			focusTimer += Time.Stepf;

			DebugFinger(Math.Round(moveTime, 1).ToString());

			UI.WindowBegin("Passthrough Toggler", ref passthroughTogglePose);
			if (UI.Button("Passthrough"))
			{
				if (passthrough.Enabled)
				{
					passthrough.Shutdown();
					Renderer.SkyTex = skyboxes[rand.Next(skyboxes.Length)];
				}
				else
				{
					passthrough.Initialize();
					Renderer.SkyTex = passThroughTexture; 
				}
			}
			UI.WindowEnd();

			UI.Handle("Model handle", ref modelPose, model.Bounds * 0.1f);
			model.Draw(modelPose.ToMatrix(0.1f));
		});
	}



	static void DebugFinger(string text)
	{
		var fingerPose = Input.Hand(Handed.Right)[FingerId.Index, JointId.Tip].Pose;
		Text.Add(text, fingerPose.ToMatrix());
	}


	static Material InitNewMaterial(string fileName)
	{
		// Start by just making a duplicate of an existing one. This creates a new
		// Material that we're free to change as much as we like.
		var mat = Material.PBR.Copy();

		// Assign an image file as the primary color of the surface.
		mat[MatParamName.DiffuseTex] = Tex.FromFile(fileName);

		return mat;
	}
}