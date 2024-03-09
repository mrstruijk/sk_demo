using StereoKit.Framework;

public class StepperExample : IStepper
{
    public bool Enabled { get; private set; } // Is this thing ready for use? Or are we pausing it?


    public bool Initialize()
    {
        // Do things that, well, initialize this thing we're trying to do here.
        // Like setting initial position values of objects, or loading materials. 

        // At the end we have to return true/false. If we return false, that probably means the Initialization 
        // hasn't gone well. This entire thing will not be run then. 

        // If we return true, all is well. 

        return true;
    }


    public void Step()
    {
        // Put stuff here that happens every frame. 
        // Like drawing an object unto the screen.
        // Or showing some UI. 
        // A lot of work can be done here, since StereoKit is written as 'immediate mode'
    }


    public void Shutdown()
    {
        // Cleanup. Throw stuff out. Does it spark joy? 
    }
}