using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetChecker : MonoBehaviour, ITrackableEventHandler
{
    [SerializeField] private GameObject GPUCollider;
    [SerializeField] private GameObject RJ45Collider;
    [SerializeField] private GameObject RJ11Collider;
    [SerializeField] private GameObject RAMCollider;
    private Triggers gpuTrigger;
    private Triggers rj45Trigger;
    private Triggers rj11Trigger;
    private Triggers ramTrigger;

    private TrackableBehaviour mTrackableBehaviour;
    private StateManager sm = TrackerManager.Instance.GetStateManager();
    // Start is called before the first frame update
    void Start()
    {
        gpuTrigger = GPUCollider.GetComponent<Triggers>();
        rj45Trigger = RJ45Collider.GetComponent<Triggers>();
        rj11Trigger = RJ11Collider.GetComponent<Triggers>();
        ramTrigger = RAMCollider.GetComponent<Triggers>();

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
        
        gpuTrigger.SetTriggerOff();
        rj45Trigger.SetTriggerOff();
        rj11Trigger.SetTriggerOff();
        ramTrigger.SetTriggerOff();
        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost 123");
    }


}
