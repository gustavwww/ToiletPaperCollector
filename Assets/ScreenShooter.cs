#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
 
public class ScreenShooter {
    [MenuItem("Screenshot/Grab")]
    public static void Grab() {
        ScreenCapture.CaptureScreenshot("Screenshot.png", 1);
    }
}
#endif
