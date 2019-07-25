using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ProximityCheckerRJ11 : MonoBehaviour
{
    [SerializeField] private Camera cam;

    //debug text
    [SerializeField] private Text text;

    //cues on MB1
    [SerializeField] private GameObject MB1;
    [SerializeField] private GameObject RJ11;
    [SerializeField] private Transform RJ11Left;
    [SerializeField] private Transform RJ11Left2;
    [SerializeField] private Transform RJ11Right;
    [SerializeField] private Transform RJ11Bot;
    [SerializeField] private GameObject RJ11Green;
    [SerializeField] private Text GreenText;
    [SerializeField] private GameObject RAMdest;

    //targets
    [SerializeField] private GameObject RJ11Target;
    [SerializeField] private GameObject RJ11TargetImage;
    [SerializeField] private GameObject RJ11TargetImage2;
   
    [SerializeField] private Transform RJ11TargetLeft;
    [SerializeField] private Transform RJ11TargetRight;
    [SerializeField] private Transform RJ11TargetBotRight;
    [SerializeField] private Transform RJ11TargetBotLeft;

    [SerializeField] private GameObject RAMTargetImage;

    private Vector3 destLeft;
    private Vector3 destLeft2;
    private Vector3 destRight;
    private Vector3 destBot;
    private Vector3 targetLeft;
    private Vector3 targetRight;
    private Vector3 targetBotLeft;
    private Vector3 targetBotRight;
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
        if (!isActive && RAMTargetImage.GetComponent<Renderer>().isVisible)
        {
            isDone = true;
            Debug.Log("[ProximityCheckerGPU]RAM target image is visible");
            RJ11Target.GetComponent<AngleMonitorRJ11>().Unregister();
            RJ11Target.GetComponent<TrackableEventHandlerCustom>().Unregister();
            this.gameObject.SetActive(false);
            RAMdest.gameObject.SetActive(true);
            RJ11Target.gameObject.SetActive(false);
            MB1.gameObject.GetComponent<MB1Checker>().PartOneDone();
            Destroy(this);
        }
        else if (this.gameObject.activeInHierarchy && !isDone)
        {

            if (RJ11TargetImage.GetComponent<Renderer>().isVisible || RJ11TargetImage2.GetComponent<Renderer>().isVisible)
            //if (isActive)
            {

                //Debug.Log("RJ45found");
                destLeft = cam.WorldToScreenPoint(RJ11Left.position);
                destLeft2 = cam.WorldToScreenPoint(RJ11Left2.position);
                destRight = cam.WorldToScreenPoint(RJ11Right.position);
                destBot = cam.WorldToScreenPoint(RJ11Bot.position);

                targetLeft = cam.WorldToScreenPoint(RJ11TargetLeft.position);
                targetRight = cam.WorldToScreenPoint(RJ11TargetRight.position);
                targetBotLeft = cam.WorldToScreenPoint(RJ11TargetBotLeft.position);
                targetBotRight = cam.WorldToScreenPoint(RJ11TargetBotRight.position);
                //text.text = ("destLeft.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x + "destBot.y: " + destBot.y + "targetBotRight.y: " + targetBotRight.y);
                text.text = "destLeft2.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x;

                if (Math.Abs(destLeft.x - destRight.x) > Math.Abs(targetLeft.x - targetRight.x) && targetLeft.x > destLeft.x && targetLeft.x < destLeft2.x)
                {
                    RJ11TargetImage.gameObject.SetActive(false);
                    RJ11TargetImage2.gameObject.SetActive(true);

                    RJ11.gameObject.SetActive(false);
                    RJ11Green.gameObject.SetActive(true);

                    if (targetBotLeft.y < destBot.y && targetBotRight.y < destBot.y)
                    {
                        text.text = "RJ11 inserted";
                        //Reset();
                        GreenText.text = "Once inserted, scan the next part (RAM Card)";
                        isActive = false;
                    }
                }
                else
                {
                    RJ11.gameObject.SetActive(true);
                    RJ11Green.gameObject.SetActive(false);
                    RJ11TargetImage.gameObject.SetActive(true);
                    RJ11TargetImage2.gameObject.SetActive(false);
                }
            }
        }
        
    }
}
