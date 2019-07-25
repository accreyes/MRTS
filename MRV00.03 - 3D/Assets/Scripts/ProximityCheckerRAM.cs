using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class ProximityCheckerRAM : MonoBehaviour
{
    [SerializeField] private Camera cam;

    //debug text
    [SerializeField] private Text text;

    //cues on MB1

    [SerializeField] private GameObject RAM;
    [SerializeField] private Transform RAMLeft;
    [SerializeField] private Transform RAMLeft2;
    [SerializeField] private Transform RAMRight;
    [SerializeField] private Transform RAMBot;
    [SerializeField] private GameObject RAMGreen;
    [SerializeField] private Text GreenText;
    //[SerializeField] private GameObject RAMdest;

    //targets
    [SerializeField] private GameObject RAMTarget;
    [SerializeField] private GameObject RAMTargetImage;
    [SerializeField] private GameObject RAMTargetImage2;
    [SerializeField] private Transform RAMTargetLeft;
    [SerializeField] private Transform RAMTargetRight;
    [SerializeField] private Transform RAMTargetBotRight;
    [SerializeField] private Transform RAMTargetBotLeft;

    //[SerializeField] private GameObject RAMTargetImage;

    private Vector3 destLeft;
    private Vector3 destLeft2;
    private Vector3 destRight;
    private Vector3 destBot;
    private Vector3 targetLeft;
    private Vector3 targetRight;
    private Vector3 targetBotLeft;
    private Vector3 targetBotRight;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy && isActive)
        {

            if (RAMTargetImage.GetComponent<Renderer>().isVisible || RAMTargetImage2.GetComponent<Renderer>().isVisible)
            //if (isActive)
            {

                //Debug.Log("RJ45found");
                destLeft = cam.WorldToScreenPoint(RAMLeft.position);
                destLeft2 = cam.WorldToScreenPoint(RAMLeft2.position);
                destRight = cam.WorldToScreenPoint(RAMRight.position);
                destBot = cam.WorldToScreenPoint(RAMBot.position);

                targetLeft = cam.WorldToScreenPoint(RAMTargetLeft.position);
                targetRight = cam.WorldToScreenPoint(RAMTargetRight.position);
                targetBotLeft = cam.WorldToScreenPoint(RAMTargetBotLeft.position);
                targetBotRight = cam.WorldToScreenPoint(RAMTargetBotRight.position);
                //text.text = ("destLeft.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x + "destBot.y: " + destBot.y + "targetBotRight.y: " + targetBotRight.y);
                text.text = "destLeft2.x: " + destLeft.x + " targetLeft.x: " + targetLeft.x;

                if (Math.Abs(destLeft.x - destRight.x) > Math.Abs(targetLeft.x - targetRight.x) && targetLeft.x > destLeft.x && targetLeft.x < destLeft2.x)
                {
                    RAMTargetImage.gameObject.SetActive(false);
                    RAMTargetImage2.gameObject.SetActive(true);

                    RAM.gameObject.SetActive(false);
                    RAMGreen.gameObject.SetActive(true);
                    if (targetBotLeft.y < destBot.y && targetBotRight.y < destBot.y)
                    {
                        text.text = "RAM inserted";
                        //Reset();
                        //isActive = false;
                        GreenText.text = "Once inserted, inform the researcher";
                    }
                }
                else
                {
                    RAMTargetImage.gameObject.SetActive(true);
                    RAMTargetImage2.gameObject.SetActive(false);
                    RAM.gameObject.SetActive(true);
                    RAMGreen.gameObject.SetActive(false);
                }
            }
        }
        else if (!isActive)
        {
            
        }
    }
}
