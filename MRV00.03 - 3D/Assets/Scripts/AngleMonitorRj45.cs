using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Vuforia;

public class AngleMonitorRj45 : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject indicator;
    private TrackableBehaviour mTrackableBehaviour;
    private bool isActive = false;
    [SerializeField] private GameObject initUI;
    [SerializeField] private GameObject mb1;
    [SerializeField] private GameObject mb2;
    [SerializeField] private Transform top;
    [SerializeField] private Transform bot;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private Camera cam;
    private bool isMBFound = false;

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

        if (isMBFound && (mb1.activeInHierarchy || mb2.activeInHierarchy))
        {
            if (isActive)
            {
                //if (this.gameObject.transform.localEulerAngles.y < 160 || this.gameObject.transform.localEulerAngles.y > 220)
                if (topPos.y < botPos.y || Math.Abs(leftPos.y - rightPos.y) > 70)
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
        initUI.gameObject.GetComponent<InitScript>().RJ45Found();
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
