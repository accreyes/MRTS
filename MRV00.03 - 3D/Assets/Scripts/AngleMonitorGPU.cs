﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

public class AngleMonitorGPU : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject indicator;
    private TrackableBehaviour mTrackableBehaviour;
    private bool isActive =false;
    private bool isPresent = true;
    [SerializeField] private GameObject initUI;
    [SerializeField] private GameObject mb1;
    [SerializeField] private GameObject mb2;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bot;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Camera cam;
    private bool isMBFound = false;

    int count = 0;
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
        //Debug.Log("topPos is :" + topPos.y + " botPos is: "+ botPos.y + " leftPos is :" + leftPos.y + " rightPos is: " + rightPos.y);

        if (isMBFound && (mb1.activeInHierarchy || mb2.activeInHierarchy))
        {
            if (isActive)
            {

                //if (this.gameObject.transform.localEulerAngles.y < 75 || this.gameObject.transform.localEulerAngles.y > 99)
                if (topPos.y < botPos.y || Math.Abs(leftPos.y-rightPos.y)>70)
                {
                    indicator.SetActive(true);
                }
                else
                {
                    indicator.SetActive(false);
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
        initUI.gameObject.GetComponent<InitScript>().GPUFound();
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
}
