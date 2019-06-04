using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AngleMonitorRJ11 : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject indicator;
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
        if (isActive)
        {
            if (this.gameObject.transform.localEulerAngles.y < 160 || this.gameObject.transform.localEulerAngles.y > 220)
            {
                indicator.SetActive(true);
            }
            else
            {
                indicator.SetActive(false);
            }
            Debug.Log("y angle " + this.gameObject.transform.localEulerAngles.y);
        }
    }

    public void OnTrackableStateChanged(
                                       TrackableBehaviour.Status previousStatus,
                                       TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
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
        isActive = true;
    }
    private void OnTrackingLost()
    {
        isActive = false;
    }
}
