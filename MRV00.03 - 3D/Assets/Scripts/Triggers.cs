using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Triggers : MonoBehaviour
{
    [SerializeField] private GameObject GPU;
    [SerializeField] private GameObject RJ45;
    [SerializeField] private GameObject RAM;
    [SerializeField] private GameObject RJ11;
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Green;

    [SerializeField] GameObject demoCube;
    // Start is called before the first frame update
    void Start()
    {
        
       // gameObject.activeInHierarchy  check if object is active
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        StateManager sm = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetActiveTrackableBehaviours();
        foreach (TrackableBehaviour tb in activeTrackables)
        {
            if(GPU.activeInHierarchy)
            {
                if (tb.TrackableName == "pebbles1")
                {

                    Reset();
                    Green.SetActive(true);
                    
                    Debug.Log("Correct");
                    break;
                }
                else
                {
                    Reset();
                    Red.SetActive(true);
                    
                    Debug.Log("Trackable: " + tb.TrackableName);
                }
            }
            else if(RJ11.activeInHierarchy)
            {

            }
            else
            {

            }
            
        }
        Debug.Log("something has entered");
    }

    public void OnTriggerExit(Collider other)
    {
        Reset();
        GPU.SetActive(true);
        Debug.Log("something has exited");
    }

    private void Reset()
    {
        GPU.SetActive(false);
        RJ11.SetActive(false);
        RJ45.SetActive(false);
        RAM.SetActive(false);
        Green.SetActive(false);
        Red.SetActive(false);
    }
}
