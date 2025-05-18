using UnityEngine;

public class ScreenSleepTimeOut : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
