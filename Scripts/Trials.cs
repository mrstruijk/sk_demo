using System;
using StereoKit;
using StereoKit.Framework;

public class Trials : IStepper
{
    #region Variables and such

    const float InterTrialIntervalDuration = 3f; // Seconds
    const float ImageDistanceFromFace = 0.5f; // Meters. Technically not properly named. But convenient for explanation. 
    const float RequiredMoveDistance = 0.3f; // Meters

    Model _itiImageObject;
    Material _itiMaterial;
    Pose _itiImagePose;

    Model _trialImageObject;
    Material[] _trialImages;
    Pose _trialImagePose;

    float _itiCountDownTimer;
    float _trialDuration;

    float _initialDistanceFromImage;
    bool _storedInitialDistance;
    float _currentDistanceFromImage;

    Random _randomValues;
    DataVault _dataVault;

    public bool Enabled { get; set; }

    #endregion


    #region This Region holds all initialization logic


    public bool Initialize()
    {
        #region Inter Trial Interval

        _itiMaterial = Material.PBR.Copy(); // Create duplicate of an existing material
        _itiMaterial[MatParamName.DiffuseTex] = Tex.FromFile("focus.gif"); // Assign an image file ('Texture') as the primary color of the material

        _itiImageObject = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), _itiMaterial); // Generate a cube with a certain size. Add the material we just made
        _itiImagePose = new Pose(0, 0, -ImageDistanceFromFace); // The image will be drawn in front of us. Yes that's a minus. It's weird. It's there. 

        _itiCountDownTimer = InterTrialIntervalDuration; // Store the duration of the ITI in our countdown timer. 

        #endregion

        #region Trial

        _trialImages = new[] { // Here we're really doing the same as above, but we're creating an entire 'list' of materials, and using a separate little method to create them for us from a bunch of textures
            CreateMaterialFromTexture("dog_1.png"),
            CreateMaterialFromTexture("dog_2.png"),
            CreateMaterialFromTexture("dog_3.png"),
            CreateMaterialFromTexture("cat_1.png"),
            CreateMaterialFromTexture("cat_2.png"),
            CreateMaterialFromTexture("cat_3.png")
        };

        _trialImageObject = Model.FromMesh(Mesh.GeneratePlane(Vec2.One * 0.5f), _trialImages[0]); // Generate a flat square surface ('Plane'), and give it one of the materials we just made
        _trialImagePose = new Pose(0, 0, -ImageDistanceFromFace, Quat.LookDir(0, 1, 0)); // The trial image will be drawn in front of us. Yes I know, still minus. 

        #endregion

        #region DontLookHere!

        _randomValues = new Random(); // Instantiate a class called Random. This is part of the relatively standard 'System' namespace in of C#.

        _dataVault = new DataVault(); // Our little helper class that will store our data

        return Enabled = true;

        #endregion
    }


    Material CreateMaterialFromTexture(string fileName)
    {
        var mat = Material.PBR.Copy(); // Copy an existing material
        mat[MatParamName.DiffuseTex] = Tex.FromFile(fileName); // Assign a texture to that material
        mat.Id = fileName; // For being able to retrieve the name of the Material, for instance for logging.
        return mat; // Gimme
    }


    #endregion


    #region Step runs every frame

    /// <summary>
    /// This runs every frame, automatically, through the magic of the IStepper
    /// </summary>
    public void Step()
    {
        #region Inter Trial Interval

        _itiCountDownTimer -= Time.Stepf; // This keeps track of how long we're already in 'focus mode'. It does this by subtracting the time elapsed since the previous frame.

        if (_itiCountDownTimer >= 0) // 3, 2, 1, GO! However, if we're not at 0 yet, don't go. Then we're still in focus mode.
        {
            _itiImageObject.Draw(_itiImagePose.ToMatrix()); // Every frame we're in focus mode, draw our focus object

            return; // Stop right there. Go no further. We're still in focus mode.
        }

        #endregion

        #region Trial

        if (_storedInitialDistance == false) // If we haven't yet stored the initial distance of our face from the trial image, please do so below. 
        {
            _initialDistanceFromImage = Vec3.Distance(Input.Head.position, _trialImagePose.position); // Store the distance between face and image, on the very first frame that the trial image is visible

            _storedInitialDistance = true; // Huzzah! 
        }

        _currentDistanceFromImage = Vec3.Distance(Input.Head.position, _trialImagePose.position); // Continuously store the distance between our face and the trial image. Every frame. Again and again.

        if (_initialDistanceFromImage - _currentDistanceFromImage <= RequiredMoveDistance) // We're still in this trial. We need to move more :)!
        {
            _trialImageObject.Draw(_trialImagePose.ToMatrix()); // Every frame, draw our trial image

            _trialDuration += Time.Stepf; // Store (cumulatively) how many seconds this trial takes. Here we're adding seconds.

            return; // We haven't moved enough yet, the code can go no further than this
        }

        #endregion

        #region Finalizing: store data, prep for next trial, resetting values

        string[] data = { // Combining all useful data in one neat little packet
            _trialImageObject.Visuals[0].Material.Id, // We stored the IDs earlier, this identical to the file name such as "dog_1.png"
            _trialDuration.ToString()
        };

        // _dataVault.StoreData(data); // DATA BABY! Hard numbers and such. 

        _trialImageObject.Visuals[0].Material = _trialImages[_randomValues.Next(_trialImages.Length)]; // Get a random 'image' from our 'list', and use it as the image of our next trial.

        _storedInitialDistance = false;
        _trialDuration = 0f;
        _itiCountDownTimer = InterTrialIntervalDuration; // Resetting this timer causes the 'focus period' to start all over again. It also makes sure that this last section only happens once after each trial.

        #endregion
    }

    #endregion


    #region Shutdown

    /// <summary>
    /// We're not really using this ATM, but it's required by the IStepper interface
    /// </summary>
    public void Shutdown()
    {
        Enabled = false;
    }

    #endregion
}