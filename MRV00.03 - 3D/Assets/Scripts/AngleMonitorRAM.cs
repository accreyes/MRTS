using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

public class AngleMonitorRAM : MonoBehaviour,ITrackableEventHandler
{
    [SerializeField] private GameObject RAMdest;
    [SerializeField] private GameObject RAMCanvasBlue;
    [SerializeField] private GameObject RAMCanvasRed;
    [SerializeField] private GameObject RAMGreen;

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject indicatorCW;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bot;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Camera cam;
    private bool isMBFound = false;
    private TrackableBehaviour mTrackableBehaviour;
    private bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 topPos = cam.WorldToScreenPoint(top.position);
        Vector3 botPos = cam.WorldToScreenPoint(bot.position);
        Vector3 leftPos = cam.WorldToScreenPoint(left.position);
        Vector3 rightPos = cam.WorldToScreenPoint(right.position);
        //Debug.Log("topPos is :" + topPos.y + " botPos is: " + botPos.y + " leftPos is :" + leftPos.y + " rightPos is: " + rightPos.y);

        if (isMBFound)
        {
            if (RAMGreen.GetComponent<Renderer>().isVisible)
            {
                RAMCanvasBlue.gameObject.SetActive(false);
                RAMCanvasRed.gameObject.SetActive(false);
            }
            else if (RAMdest.gameObject.activeInHierarchy)
            {
                //RAMCanvasBlue.gameObject.SetActive(true);
                RAMCanvasRed.gameObject.SetActive(false);
            }
            else
            {
                RAMCanvasBlue.gameObject.SetActive(false);
                RAMCanvasRed.gameObject.SetActive(true);
            }

            if (isActive)
            {
                //if (this.gameObject.transform.localEulerAngles.y < 160 || this.gameObject.transform.localEulerAngles.y > 220)
                if (topPos.y < botPos.y || Math.Abs(leftPos.y - rightPos.y) > 300)
                {
                    if (topPos.x > botPos.x)
                    {
                        indicator.SetActive(true);
                        indicatorCW.SetActive(false);
                    }

                    else
                    {
                        indicatorCW.SetActive(true);
                        indicator.SetActive(false);
                    }
                }
                else
                {
                    indicator.SetActive(false);
                    indicatorCW.SetActive(false);
                }
                //Debug.Log("y angle " + this.gameObject.transform.localEulerAngles.y);
            }
        }
    }

    public void OnTrackableStateChanged(
                                       TrackableBehaviour.Status previousStatus,
                                       TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    private void OnTrackingFound()
    {
        //initUI.gameObject.GetComponent<InitScript>().RAMFound();
        isActive = true;
    }
    private void OnTrackingLost()
    {
        isActive = false;
    }

    public void MBFound()
    {
        isMBFound = true;
    }
    public void Unregister()
    {
        mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
}
