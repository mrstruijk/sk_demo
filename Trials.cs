using System;
using StereoKit;
using StereoKit.Framework;

public class Trials : IStepper
{
    #region Variables and such

    Model _focusImageObject;
    Material _focusMaterial;
    Pose _focusImagePose;

    Model _trialImageObject;
    Material[] _trialMaterials;
    Pose _trialImagePose;
    Tex _currentTrialImage;

    const float FocusPeriodDuration = 3f; // Seconds
    const float ImageDistanceFromFace = 0.5f; // Meters. Technically not properly named. But convenient for explanation. 
    const float RequiredMoveDistance = 0.3f; // Meters

    float _focusCountDownTimer;
    float _trialDuration;

    float _initialDistanceFromImage;
    bool _storedInitialDistance;
    float _currentDistanceFromImage;

    Random _randomValues;
    DataVault _dataVault;

    public bool Enabled { get; set; }

    #endregion


    #region Initialize

    /// <summary>
    /// This gets called one time, automatically, before the app fully starts up. 
    /// Similar to the Awake() or the Start() methods in Unity.
    /// </summary>
    /// <returns></returns>
    public bool Initialize()
    {
        _focusMaterial = Material.PBR.Copy(); // Create duplicate of an existing material
        _focusMaterial[MatParamName.DiffuseTex] = Tex.FromFile("focus.gif"); // Assign an image file ('Texture') as the primary color of the material

        _focusImageObject = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), _focusMaterial); // Generate a cube with a certain size. Add the material we just made
        _focusImagePose = new Pose(0, 0, -ImageDistanceFromFace); // The image will be drawn in front of us. Yes that's a minus. It's weird. It's there. 

        _trialMaterials = new[] { // Here we're really doing the same as above, but we're creating an entire 'list' of materials, and using a separate little method to create them for us from a bunch of textures
            CreateMaterialFromTexture("dog_1.png"),
            CreateMaterialFromTexture("dog_2.png"),
            CreateMaterialFromTexture("dog_3.png"),
            CreateMaterialFromTexture("cat_1.png"),
            CreateMaterialFromTexture("cat_2.png"),
            CreateMaterialFromTexture("cat_3.png")
        };

        _trialImageObject = Model.FromMesh(Mesh.GeneratePlane(Vec2.One * 0.5f), _trialMaterials[0]); // Generate a flat square surface ('Plane'), and give it one of the materials we just made
        _trialImagePose = new Pose(0, 0, -ImageDistanceFromFace, Quat.LookDir(0, 1, 0)); // The trial image will be drawn in front of us. Yes I know, still minus. 

        _randomValues = new Random(); // Instantiate a class called Random. This is part of the relatively standard 'System' namespace in of C#.
        _dataVault = new DataVault(); // Our little helper class that will store our data

        _focusCountDownTimer = FocusPeriodDuration; // Store amount of seconds we're in 'focus mode' in our countdown timer. 

        return Enabled = true;
    }


    Material CreateMaterialFromTexture(string fileName)
    {
        var mat = Material.PBR.Copy(); // Copy an existing material
        mat[MatParamName.DiffuseTex] = Tex.FromFile(fileName); // Assign a texture to that material
        mat.Id = fileName; // For being able to retrieve the name of the Material, for instance for logging.
        return mat; // Gimme
    }

    #endregion


    #region Step

    /// <summary>
    /// This runs every frame, automatically, through the magic of the IStepper
    /// </summary>
    public void Step()
    {
        #region Focus period

        _focusCountDownTimer -= Time.Stepf; // This keeps track of how long we're already in 'focus mode'. It does this by subtracting the time elapsed since the previous frame.

        if (_focusCountDownTimer >= 0) // 3, 2, 1, GO! However, if we're not at 0 yet, don't go. Then we're still in focus mode.
        {
            _focusImageObject.Draw(_focusImagePose.ToMatrix()); // Every frame we're in focus mode, draw our focus object

            return; // Stop right there. Go no further. We're still in focus mode.
        }

        #endregion

        #region Trial period

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

        _dataVault.StoreData(data); // DATA BABY! Hard numbers and such. 

        _trialImageObject.Visuals[0].Material = _trialMaterials[_randomValues.Next(_trialMaterials.Length)]; // Get a random 'image' from our 'list', and use it as the image of our next trial.

        _storedInitialDistance = false;
        _trialDuration = 0f;
        _focusCountDownTimer = FocusPeriodDuration; // Resetting this timer causes the 'focus period' to start all over again. It also makes sure that this last section only happens once after each trial.

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
