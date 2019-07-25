using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProximityCheckerRJ45 : MonoBehaviour
{   //Camera
    [SerializeField] private Camera cam;

    //debug text
    [SerializeField] private Text text;

    //cues on MB1
    [SerializeField] private GameObject RJ45;
    [SerializeField] private Transform RJ45Left;
    [SerializeField] private Transform RJ45Left2;
    [SerializeField] private Transform RJ45Right;
    [SerializeField] private Transform RJ45Bot;
    [SerializeField] private GameObject RJ45Green;
    [SerializeField] private Text GreenText;
    [SerializeField] private GameObject RJ11dest;

    //targets
    [SerializeField] private GameObject RJ45Target;
    [SerializeField] private GameObject RJ45TargetImage;
    [SerializeField] private GameObject RJ45TargetImage2;
    [SerializeField] private Transform RJ45TargetLeft;
    [SerializeField] private Transform RJ45TargetRight;
    [SerializeField] private Transform RJ45TargetBotRight;
    [SerializeField] private Transform RJ45TargetBotLeft;

    [SerializeField] private GameObject RJ11TargetImage;

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
        if (!isActive && RJ11TargetImage.GetComponent<Renderer>().isVisible)
        {
            isDone = true;
            Debug.Log("[ProximityCheckerGPU]RJ11 target image is visible");
            RJ45Target.GetComponent<AngleMonitorRj45>().Unregister();
            RJ45Target.GetComponent<TrackableEventHandlerCustom>().Unregister();
            this.gameObject.SetActive(false);
            RJ11dest.gameObject.SetActive(true);
            RJ45Target.gameObject.SetActive(false);

            Destroy(this);
        }
        else if (this.gameObject.activeInHierarchy && !isDone)
        {

            if (RJ45TargetImage.GetComponent<Renderer>().isVisible|| RJ45TargetImage2.GetComponent<Renderer>().isVisible)
            //if (isActive)
            {

                //Debug.Log("RJ45found");
                destLeft = cam.WorldToScreenPoint(RJ45Left.position);
                destLeft2 = cam.WorldToScreenPoint(RJ45Left2.position);
                destRight = cam.WorldToScreenPoint(RJ45Right.position);
                destBot = cam.WorldToScreenPoint(RJ45Bot.position);

                targetLeft = cam.WorldToScreenPoint(RJ45TargetLeft.position);
                targetRight = cam.WorldToScreenPoint(RJ45TargetRight.position);
                targetBotLeft = cam.WorldToScreenPoint(RJ45TargetBotLeft.position);
                targetBotRight = cam.WorldToScreenPoint(RJ45TargetBotRight.position);
                //text.text = ("destLeft.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x + "destBot.y: " + destBot.y + "targetBotRight.y: " + targetBotRight.y);
                text.text = "destLeft2.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x;

                if (Math.Abs(destLeft.x - destRight.x) > Math.Abs(targetLeft.x - targetRight.x) && targetLeft.x > destLeft.x && targetLeft.x < destLeft2.x)
                {
                    RJ45TargetImage.gameObject.SetActive(false);
                    RJ45TargetImage2.gameObject.SetActive(true);

                    RJ45.gameObject.SetActive(false);
                    RJ45Green.gameObject.SetActive(true);
                    if (targetBotLeft.y < destBot.y && targetBotRight.y < destBot.y )
                    {
                        text.text = "RJ45 inserted";
                        //Reset();
                        isActive = false;
                        GreenText.text = "Once inserted, scan the next part (Phone Card)";
                    }
                }
                else
                {
                    RJ45.gameObject.SetActive(true);
                    RJ45Green.gameObject.SetActive(false);
                    RJ45TargetImage.gameObject.SetActive(true);
                    RJ45TargetImage2.gameObject.SetActive(false);
                }
            }
        }
        
    }
}
