using StereoKit;
using StereoKit.Framework;


public class StepperExample : IStepper
{
    public bool Enabled { get; private set; }


    public bool Initialize()
    {
        return true;
    }


    public void Step()
    {

    }


    public void Shutdown()
    {

    }
}


public class DummyProgramClassWhichCallsTheStepper
{
    static void DummyMain(string[] args)
    {
        var exampleStepper = SK.GetOrCreateStepper<StepperExample>();
    }
}