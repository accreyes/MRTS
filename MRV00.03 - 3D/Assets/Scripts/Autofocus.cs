using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Autofocus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
    }

    private void OnVuforiaStarted()
    {
        Debug.Log("[OnVuforiaStarted][OnVuforiaStarted][OnVuforiaStarted][OnVuforiaStarted]");
        bool focusModeSet = CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        if (!focusModeSet)
        {
            Debug.Log("Failed to set focus mode (unsupported mode).");
        }

        //CameraDevice.Instance.SetFlashTorchMode(true); //turns on flashlight 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
