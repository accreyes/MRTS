using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class MB2Checker : MonoBehaviour, ITrackableEventHandler
{
    private TrackableBehaviour mTrackableBehaviour;
    [SerializeField] private GameObject initUI;
    [SerializeField] private GameObject rotateMB;
    [SerializeField] private GameObject rotateMBArrows;
    [SerializeField] private GameObject mb1;
    [SerializeField] private TextMeshProUGUI HUD;
    [SerializeField] private TextMeshProUGUI HUD2;
    [SerializeField] private GameObject GPUTarget;
    [SerializeField] private GameObject RJ11Target;
    [SerializeField] private GameObject RJ45Target;
    [SerializeField] private GameObject RAMTarget;

    //private int count = 0;
    private bool isPartOne = false; //original false


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
        Debug.Log("[MB2][OnTrackingFound]");
        //GPUTarget.GetComponent<AngleMonitorGPU>().MBFound();
        //RJ11Target.GetComponent<AngleMonitorRJ11>().MBFound();
        //RJ45Target.GetComponent<AngleMonitorRj45>().MBFound();
        //RAMTarget.GetComponent<AngleMonitorRAM>().MBFound();

        
        if (isPartOne)
        {
            this.gameObject.SetActive(true);
            HUDText("Welcome to Phase 2");
            mb1.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
            mb1.gameObject.SetActive(false);
            rotateMB.gameObject.SetActive(false);
            rotateMBArrows.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(false);
            HUDText("Motherboard Phase 1 is not yet finished. Rotate the motherboard back to its previous orientation!");
        }
        
    }
    private void OnTrackingLost()
    {
        if (isPartOne)
        {
            //mb1.gameObject.GetComponent<DefaultTrackableEventHandler>().Reregister();
            //mb1.gameObject.SetActive(true);
        }
    }

    public void PartOneDone()
    {
        Debug.Log("[MB2][PartOneDone]");
        isPartOne = true;
    }
    private void HUDText(string s)
    {
        HUD.text = s;
        HUD2.text = s;
    }
}
