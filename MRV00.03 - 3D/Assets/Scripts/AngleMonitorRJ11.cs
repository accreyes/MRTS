using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

public class AngleMonitorRJ11 : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject RJ11dest;
    [SerializeField] private GameObject RJ11CanvasBlue;
    [SerializeField] private GameObject RJ11CanvasRed;
    [SerializeField] private GameObject RJ11Green;
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
        topPos = cam.WorldToScreenPoint(top.position);
        botPos = cam.WorldToScreenPoint(bot.position);
        leftPos = cam.WorldToScreenPoint(left.position);
        rightPos = cam.WorldToScreenPoint(right.position);
        //Debug.Log("RJ11 topPos is :" + topPos.y + " botPos is: " + botPos.y + " leftPos is :" + leftPos.y + " rightPos is: " + rightPos.y);

        if (isMBFound)
        {
            if (RJ11Green.GetComponent<Renderer>().isVisible)
            {
                RJ11CanvasBlue.gameObject.SetActive(false);
                RJ11CanvasRed.gameObject.SetActive(false);
            }
            else if (RJ11dest.gameObject.activeInHierarchy)
            {
                RJ11CanvasBlue.gameObject.SetActive(true);
                RJ11CanvasRed.gameObject.SetActive(false);
            }
            else
            {
                RJ11CanvasBlue.gameObject.SetActive(false);
                RJ11CanvasRed.gameObject.SetActive(true);
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
        //initUI.gameObject.GetComponent<InitScript>().RJ11Found();
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
