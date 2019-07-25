using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class MB1Init : MonoBehaviour
{
    [SerializeField] private GameObject RJ45;
    [SerializeField] private GameObject RJ11;
    [SerializeField] private GameObject RAM;
    [SerializeField] private GameObject GPU;
    [SerializeField] private GameObject mb1v2;
    private bool isStarted = false;
    // Start is called before the first frame update
    void Start()
    {
 
        GPU.gameObject.SetActive(false);
        RJ11.gameObject.SetActive(false);
        RJ45.gameObject.SetActive(false);
        RAM.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isStarted)
        {
            mb1v2.gameObject.GetComponent<DefaultTrackableEventHandler>().Unregister();
            mb1v2.gameObject.SetActive(false);
            //mb1v2.gameObject.GetComponent<ObjectTargetBehaviour>().enabled = false;
            isStarted = true;
        }
    }
}
