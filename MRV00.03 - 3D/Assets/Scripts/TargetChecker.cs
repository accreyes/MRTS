using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetChecker : MonoBehaviour, ITrackableEventHandler
{
    //[SerializeField] private GameObject GPU;
    private TrackableBehaviour mTrackableBehaviour;
    private StateManager sm = TrackerManager.Instance.GetStateManager();
    // Start is called before the first frame update
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
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
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found 123");
    }
    private void OnTrackingLost()
    {
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost 123");
    }


}
