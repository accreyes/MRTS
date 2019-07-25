using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ProximityCheckerGPU : MonoBehaviour
{
    //Camera
    [SerializeField] private Camera cam;

    //debug text
    [SerializeField] private Text text;

    //cues on MB1
    [SerializeField] private GameObject GPU;
    [SerializeField] private Transform GPULeft;
    [SerializeField] private Transform GPURight;
    [SerializeField] private Transform GPUBot;
    [SerializeField] private GameObject GPUGreen;
    [SerializeField] private Text GreenText;
    [SerializeField] private GameObject RJ45dest;
    [SerializeField] private GameObject MBOther;
    [SerializeField] private GameObject MBinit;

    //targets
    [SerializeField] private GameObject GPUTarget;
    [SerializeField] private GameObject GPUTargetImage;
    [SerializeField] private GameObject GPUTargetImage2;
    [SerializeField] private Transform GPUTargetLeft;
    [SerializeField] private Transform GPUTargetRight;
    [SerializeField] private Transform GPUTargetBotRight;
    [SerializeField] private Transform GPUTargetBotLeft;

    [SerializeField] private GameObject RJ45TargetImage;


    private Vector3 destLeft;
    private Vector3 destRight;
    private Vector3 destBot;
    private Vector3 targetLeft;
    private Vector3 targetRight;
    private Vector3 targetBotLeft;
    private Vector3 targetBotRight;
    private TrackableBehaviour GPUTracker;

    private bool isActive;
    private bool isDone;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        isDone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive && RJ45TargetImage.GetComponent<Renderer>().isVisible)
        {
            isDone = true;
            Debug.Log("[ProximityCheckerGPU]RJ45 target image is visible");
            //Debug.Log("[MB1CHECKER][ONTrackingFound]GPU target unregister");
            GPUTarget.GetComponent<AngleMonitorGPU>().Unregister();
            GPUTarget.GetComponent<TrackableEventHandlerCustom>().Unregister();
            this.gameObject.SetActive(false);
            RJ45dest.gameObject.SetActive(true);
            GPUTarget.gameObject.SetActive(false);

            MBinit.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
            MBinit.gameObject.GetComponent<MB1Checker>().Unregister();
            Destroy(MBinit.gameObject.GetComponent<MB1Checker>());
            MBinit.gameObject.SetActive(false);
            MBOther.gameObject.GetComponent<DefaultTrackableEventHandler>().Reregister();
            MBOther.gameObject.GetComponent<MB1Checker>().Reregister();
            MBOther.gameObject.SetActive(true);
            Destroy(this);
        }
        else if (this.gameObject.activeInHierarchy && !isDone)
        {
            if (GPUTargetImage.GetComponent<Renderer>().isVisible || GPUTargetImage2.GetComponent<Renderer>().isVisible)
            //if (GPUTargetImage2.GetComponent<Renderer>().isVisible)
            {
                //Debug.Log("GPUfound");
                destLeft = cam.WorldToScreenPoint(GPULeft.position);
                destRight = cam.WorldToScreenPoint(GPURight.position);
                destBot = cam.WorldToScreenPoint(GPUBot.position);

                targetLeft = cam.WorldToScreenPoint(GPUTargetLeft.position);
                targetRight = cam.WorldToScreenPoint(GPUTargetRight.position);
                targetBotLeft = cam.WorldToScreenPoint(GPUTargetBotLeft.position);
                targetBotRight = cam.WorldToScreenPoint(GPUTargetBotRight.position);

                text.text = ("destLeft.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x + "destBot.y: " + destBot.y + "targetBotRight.y: " + targetBotRight.y);
                if (Math.Abs(destLeft.x - destRight.x) > Math.Abs(targetLeft.x - targetRight.x) && targetLeft.x > destLeft.x)
                {
                    GPUTargetImage.gameObject.SetActive(false);
                    GPUTargetImage2.gameObject.SetActive(true);

                    GPU.gameObject.SetActive(false);
                    GPUGreen.gameObject.SetActive(true);
                    if (targetBotLeft.y < destBot.y && targetBotRight.y < destBot.y)
                    {
                        text.text = "GPU inserted";
                        //Reset();
                        isActive = false;
                        GreenText.text = "Once inserted, scan the next part (LAN Card)";
                    }
                }
                else
                {
                    //isActive = true;
                    GPU.gameObject.SetActive(true);
                    GPUGreen.gameObject.SetActive(false);
                    GPUTargetImage.gameObject.SetActive(true);
                    GPUTargetImage2.gameObject.SetActive(false);
                }
            }
        }
        

    }

    private void Reset()
    {
        GPU.SetActive(false);
        GPUGreen.gameObject.SetActive(false);

    }
}
