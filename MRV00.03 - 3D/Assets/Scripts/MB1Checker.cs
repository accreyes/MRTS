using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class MB1Checker : MonoBehaviour,ITrackableEventHandler
{
    private static int count = 0; //original 0

    [SerializeField] private TextMeshProUGUI HUD;
    [SerializeField] private TextMeshProUGUI HUD2;
    [SerializeField] private GameObject initUI;
    [SerializeField] private GameObject rotateMB;
    [SerializeField] private GameObject rotateMBArrows;
    [SerializeField] private GameObject mb2;
    [SerializeField] private GameObject mb1v2;
    [SerializeField] private GameObject GPUTarget;
    [SerializeField] private GameObject RJ11Target;
    [SerializeField] private GameObject RJ45Target;
    [SerializeField] private GameObject RAMTarget;
    [SerializeField] private GameObject GPUDest;


    private bool isPartOne = false; //original false
    //private bool isUpdated = true;

    private TrackableBehaviour mTrackableBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        GPUDest.gameObject.SetActive(false);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            if (mTrackableBehaviour.name == "MB1v2")
            {
                mTrackableBehaviour.UnregisterTrackableEventHandler(this);
            }
            else
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }
        //mb2.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
        //mb2.gameObject.SetActive(false);
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
        mb2.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
        mb2.gameObject.SetActive(false);
        if(count==0)
        {
            Debug.Log("[MB1Checker][count==0]");
            mb1v2.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
            mb1v2.gameObject.GetComponent<MB1Checker>().Unregister();
            mb1v2.gameObject.SetActive(false);
            GPUDest.gameObject.SetActive(true);
            GPUTarget.GetComponent<AngleMonitorGPU>().MBFound();
            RJ11Target.GetComponent<AngleMonitorRJ11>().MBFound();
            RJ45Target.GetComponent<AngleMonitorRj45>().MBFound();
            RAMTarget.GetComponent<AngleMonitorRAM>().MBFound();
            count++;
            
        }

        if (isPartOne && this.gameObject.activeInHierarchy)
        {
            
            initUI.gameObject.SetActive(true);
            HUDText("Phase 1 is done. Rotate Motherboard 90 degrees clockwise");
            rotateMB.gameObject.SetActive(true);   //this is originally uncommented
            rotateMBArrows.gameObject.SetActive(true);  //this is originally uncommented
            mb2.gameObject.GetComponent<MB2Checker>().PartOneDone();
            mb2.gameObject.SetActive(true);
        }
        else
        {
            HUDText("Welcome to Phase 1");
        }
        /*else if (isInitDone)
        {
            Debug.Log("deactivate initUI");
            //initUI.SetActive(false);
            HUDText("Motherboard Phase 1 Found!");
        }*/
    }
    private void OnTrackingLost()
    {
        mb2.gameObject.GetComponent<DefaultTrackableEventHandler>().Reregister();
        mb2.gameObject.SetActive(true);
    }



    public void PartOneDone()
    {
        isPartOne = true;
        HUDText("Phase 1 is done. Rotate Motherboard 90 degrees clockwise");
        initUI.gameObject.SetActive(true);  //this is originally uncommented
        rotateMB.SetActive(true);
        rotateMBArrows.SetActive(true);
        mb2.gameObject.GetComponent<MB2Checker>().PartOneDone();
        mb2.gameObject.SetActive(true);
        //isUpdated = false;
    }

    private void HUDText(string s)
    {
        HUD.text = s;
        HUD2.text = s;
    }
    public void Unregister()
    {
        if (mTrackableBehaviour)
        {
            Debug.Log(mTrackableBehaviour.name + "is unregistered");
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        }
    }
    public void Reregister()
    {
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
}
