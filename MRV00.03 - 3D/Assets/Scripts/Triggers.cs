using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Vuforia;

public class Triggers : MonoBehaviour
{
    [SerializeField] private GameObject GPU;
    [SerializeField] private GameObject RJ45;
    [SerializeField] private GameObject RJ11;
    [SerializeField] private GameObject RAM;


    [SerializeField] private GameObject RedGPU;
    [SerializeField] private GameObject GreenGPU;
    [SerializeField] private GameObject RedRJ45;
    [SerializeField] private GameObject GreenRJ45;
    [SerializeField] private GameObject RedRJ11;
    [SerializeField] private GameObject GreenRJ11;
    [SerializeField] private GameObject RedRAM;
    [SerializeField] private GameObject GreenRAM;

    [SerializeField] private GameObject GPUTrigger;
    [SerializeField] private GameObject RJ45Trigger;
    [SerializeField] private GameObject RJ11Trigger;
    [SerializeField] private GameObject RAMTrigger;

    [SerializeField] private GameObject mb1;


    private bool isPresent = false;
    private bool isTriggered = false;
    private float timer = 0f;
    private string currentPart = "";
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(this.name == "RAMTrigger (FRONT)")
        {
            currentPart = "RAM";
        }
        //RJ45Trigger.SetActive(false);
        //RJ11Trigger.SetActive(false);
        // gameObject.activeInHierarchy  check if object is active
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isTriggered)
        {
            
            //Debug.Log("GPU state: "+ GPU.activeInHierarchy);
            //////////////////////////// AFTER TRACKABLE LOOP //////////////////////////////////////////
            //if (GPU.activeInHierarchy && isPresent)
            if (currentPart =="GPU" && isPresent)
            {
                Reset();
                GreenGPU.SetActive(true);
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    timer = 0;
                    Reset();
                    RJ45.SetActive(true);
                    GPUTrigger.SetActive(false);
                    RJ45Trigger.SetActive(true);
                    isTriggered = false;
                    Debug.Log("i am inside the timer");
                    currentPart = "RJ45";
                    this.gameObject.SetActive(false);
                }
            }
            else if (currentPart == "GPU" && !isPresent)
            {
                Debug.Log("inside GPURed");
                Reset();
                RedGPU.SetActive(true);
                isTriggered = false;

            }
            else if (currentPart == "RJ45" && isPresent)
            {
                Reset();
                GreenRJ45.SetActive(true);
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    timer = 0;
                    Reset();
                    RJ11.SetActive(true);
                    RJ11Trigger.SetActive(true);
                    RJ45Trigger.SetActive(false);
                    isTriggered = false;
                    Debug.Log("i am inside the timer");
                    currentPart = "RJ11";
                }

            }
            else if (currentPart == "RJ45" && !isPresent)
            {
                Debug.Log("isTriggered [else] inside update");
                Reset();
                RedRJ45.SetActive(true);
                isTriggered = false; //testing (missing RJ45 asset)
            }
            else if (currentPart == "RJ11" && isPresent)
            {
                Reset();
                GreenRJ11.SetActive(true);
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    timer = 0;
                    Reset();
                    RAM.SetActive(true);
                    RJ11Trigger.SetActive(false);
                    RAMTrigger.SetActive(true);
                    isTriggered = false;
                    mb1.gameObject.GetComponent<MB1Checker>().PartOneDone();
                    //mb1.SetActive(false);
                    Debug.Log("i am inside the timer");
                    currentPart = "RAM";
                }

            }
            else if (currentPart == "RJ11" && !isPresent)
            {
                Debug.Log("isTriggered [else] inside update");
                Reset();
                RedRJ11.SetActive(true);
                isTriggered = false;
            }
            else if (currentPart == "RAM" && isPresent)
            {
                Reset();
                GreenRAM.SetActive(true);
                timer += Time.deltaTime;
                if (timer >= 2f)
                {
                    timer = 0;
                    Reset();
                    //RAM.SetActive(true);
                    RAMTrigger.SetActive(false);
                    
                    isTriggered = false;
                    Debug.Log("i am inside the timer");
                    currentPart = "";
                }

            }
            else if (currentPart == "RAM" && !isPresent)
            {
                Debug.Log("isTriggered [else] inside update");
                Reset();
                RedRAM.SetActive(true);
                isTriggered = false;
            }


        }
    }

    public void OnTriggerEnter(Collider other)
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        Debug.Log("[Triggers][OnTriggerEnter]");
        count = 0;
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if (GPU.activeInHierarchy)
            {
                currentPart = "GPU";
                if (tb.TrackableName == "pebbles1")
                {

                    isPresent = true;
                    Debug.Log("pebbles 1 found under GPU");
                    break;
                }
                else
                {
                    isPresent = false;
                }
            }
            else if (RJ45.activeInHierarchy)
            {
                currentPart = "RJ45";
                if (tb.TrackableName == "pebbles2")
                {

                    isPresent = true;
                    Debug.Log("pebbles 2 found under GPU");
                    break;
                }
                else
                {
                    isPresent = false;
                }
            }
            else if (RJ11.activeInHierarchy)
            {
                currentPart = "RJ11";
                if (tb.TrackableName == "fishy1")
                {

                    isPresent = true;
                    Debug.Log("fishy 1 found under GPU");
                    break;
                }
                else
                {
                    isPresent = false;
                }
            }
            else if (RAM.activeInHierarchy)
            {
                currentPart = "RAM";
                if (tb.TrackableName == "mosaic2") //put target here
                {

                    isPresent = true;
                    Debug.Log("target mosaic2");
                    break;
                }
                else
                {
                    isPresent = false;
                }
            }

        }
        isTriggered = true;
        
    }

    public void OnTriggerExit(Collider other)
    {
        SetTriggerOff();

        Debug.Log("something has exited");
    }

    private void Reset()
    {
        GPU.SetActive(false);
        RJ11.SetActive(false);
        RJ45.SetActive(false);
        RAM.SetActive(false);
        GreenGPU.SetActive(false);
        RedGPU.SetActive(false);
        GreenRJ45.SetActive(false);
        RedRJ45.SetActive(false);
        GreenRJ11.SetActive(false);
        RedRJ11.SetActive(false);
        GreenRAM.SetActive(false);
        RedRAM.SetActive(false);
        //timer = 0;
        //GreenRAM.SetActive(false);
        //RedRAM.SetActive(false);
    }

    public void SetTriggerOff()
    {
        Debug.Log("[Triggers][SetTriggersOff] current part: " + currentPart);
        isTriggered = false;
        if (currentPart == "GPU")
        {
            Debug.Log("[SetTriggerOff] before reset current part: " + currentPart);
            Reset();
            GPU.SetActive(true);
            Debug.Log("[SetTriggerOff] after reset current part: " + currentPart);
        }
        else if (currentPart == "RJ45")
        {
            Reset();
            RJ45.SetActive(true);
        }
        else if (currentPart == "RJ11")
        {
            Reset();
            RJ11.SetActive(true);
        }
        else if (currentPart == "RAM")
        {
            Reset();
            RAM.SetActive(true);
        }
        else
        {
            Debug.Log("[Triggers][SetTriggersOff] inside else " );
            //Reset();
            //GPU.SetActive(true);
        }

        timer = 0;
    }
}


/////////////////////////// NOTES /////////////////////////////
/// manual timer
/*DateTime saveNow = DateTime.UtcNow;
                double i = 0;
                while (i <= 5000)
                {
                    TimeSpan temp = DateTime.UtcNow - saveNow;
                    i = temp.TotalMilliseconds;
                }*/
