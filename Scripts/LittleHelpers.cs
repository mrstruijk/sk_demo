using StereoKit;


public class LittleHelpers
{
    static void DebugFinger(string text)
    {
        var fingerPose = Input.Hand(Handed.Right)[FingerId.Index, JointId.Tip].Pose;
        Text.Add(text, fingerPose.ToMatrix());
    }
}