using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

public class AngleMonitorGPU : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject GPUdest;
    [SerializeField] private GameObject GPUCanvasBlue;
    [SerializeField] private GameObject GPUGreen;

    [SerializeField] private GameObject indicator;
    [SerializeField] private GameObject indicatorCW;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bot;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Camera cam;
    private bool isMBFound = false;
    private Vector3 topPos;
    private Vector3 botPos;
    private Vector3 leftPos;
    private Vector3 rightPos;
    private TrackableBehaviour mTrackableBehaviour;
    private bool isActive = false;
    private bool isPresent = true;

    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

        topPos = cam.WorldToScreenPoint(top.position);
        botPos = cam.WorldToScreenPoint(bot.position);
        leftPos = cam.WorldToScreenPoint(left.position);
        rightPos = cam.WorldToScreenPoint(right.position);
        //Debug.Log("topPos is :" + topPos.x + " botPos is: "+ botPos.x + " leftPos is :" + leftPos.y + " rightPos is: " + rightPos.y);

        if (isMBFound)
        {
            if(GPUGreen.GetComponent<Renderer>().isVisible)
            {
                GPUCanvasBlue.gameObject.SetActive(false);
            }
            else if(GPUdest.gameObject.activeInHierarchy)
            {
                GPUCanvasBlue.gameObject.SetActive(true);
            }
            else
            {
                GPUCanvasBlue.gameObject.SetActive(false);
            }

            if (isActive)
            {

                //if (this.gameObject.transform.localEulerAngles.y < 75 || this.gameObject.transform.localEulerAngles.y > 99)
                if (topPos.y < botPos.y || Math.Abs(leftPos.y-rightPos.y)>300)
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
            newStatus == TrackableBehaviour.Status.TRACKED) //||
            //newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
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
        //var tb2 = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();
        //TrackerManager.Instance.GetTracker<PositionalDeviceTracker>().Stop();
        //tb2.Stop();
        //initUI.gameObject.GetComponent<InitScript>().GPUFound();

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
