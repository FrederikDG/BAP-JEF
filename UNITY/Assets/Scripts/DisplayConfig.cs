using UnityEngine;

public class MultiDisplaySetup : MonoBehaviour
{
    void Start()
    {
        if (Display.displays.Length > 1) Display.displays[1].Activate();
        if (Display.displays.Length > 2) Display.displays[2].Activate();
        if (Display.displays.Length > 3) Display.displays[3].Activate();
    }
}
