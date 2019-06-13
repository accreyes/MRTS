using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class MB1Checker : MonoBehaviour,ITrackableEventHandler
{
    private int count = 0;
    [SerializeField] private GameObject initUI;
    [SerializeField] private TextMeshProUGUI HUD;
    [SerializeField] private TextMeshProUGUI HUD2;
    [SerializeField] private GameObject rotateMB;
    [SerializeField] private GameObject rotateMBArrows;
    [SerializeField] private GameObject mb2;
    [SerializeField] private GameObject GPUTarget;
    [SerializeField] private GameObject RJ11Target;
    [SerializeField] private GameObject RJ45Target;
    [SerializeField] private GameObject RAMTarget;

    private bool isInitDone = false;
    private bool isPartOne = false;
    private bool isUpdated = true;

    private TrackableBehaviour mTrackableBehaviour;
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
        if(this.gameObject.activeInHierarchy && count ==0)
        {
            count++;
            this.gameObject.SetActive(false);
            Debug.Log("mb1 is disabled");
        }
        /*if (isPartOne && isUpdated)
        {
            HUDText("Part 1 is done. Rotate Motherboard 90 degrees clockwise");
            rotateMB.SetActive(true);
            rotateMBArrows.SetActive(true);
            mb2.gameObject.GetComponent<MB2Checker>().PartOneDone();
            mb2.gameObject.SetActive(true);
            isUpdated = false;
        }*/

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
        GPUTarget.GetComponent<AngleMonitorGPU>().MBFound();
        RJ11Target.GetComponent<AngleMonitorRJ11>().MBFound();
        RJ45Target.GetComponent<AngleMonitorRj45>().MBFound();
        RAMTarget.GetComponent<AngleMonitorRAM>().MBFound();

        if (isPartOne && this.gameObject.activeInHierarchy)
        {
            HUDText("Part 1 is done. Rotate Motherboard 90 degrees clockwise");
            rotateMB.gameObject.SetActive(true);
            rotateMBArrows.gameObject.SetActive(true);
            mb2.gameObject.GetComponent<MB2Checker>().PartOneDone();
            mb2.gameObject.SetActive(true);
        }
        else if (isInitDone)
        {
            Debug.Log("deactivate initUI");
            //initUI.SetActive(false);
            HUDText("Motherboard Phase 1 Found!");
        }
    }
    private void OnTrackingLost()
    {
        
    }

    public void InitDone()
    {
        isInitDone = true;
    }

    public void PartOneDone()
    {
        isPartOne = true;
        HUDText("Part 1 is done. Rotate Motherboard 90 degrees clockwise");
        rotateMB.SetActive(true);
        rotateMBArrows.SetActive(true);
        mb2.gameObject.GetComponent<MB2Checker>().PartOneDone();
        mb2.gameObject.SetActive(true);
        isUpdated = false;
    }

    private void HUDText(string s)
    {
        HUD.text = s;
        HUD2.text = s;
    }
}
