﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System;

public class AngleCheck : MonoBehaviour
{
    [SerializeField] private GameObject mb1;
    [SerializeField] private GameObject mb2;
    [SerializeField] private Text debug;
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //target.transform.Rotate(0, 10, 0);
        if (mb1.activeInHierarchy)
        {
           
            debug.text = "MB1 => x angle: " + (float)(Math.Truncate((double)mb1.transform.localEulerAngles.x * 100.0) / 100.0) + " y angle: " + (float)(Math.Truncate((double)mb1.transform.localEulerAngles.y * 100.0) / 100.0) +
            " z angle: " + (float)(Math.Truncate((double)mb1.transform.localEulerAngles.z * 100.0) / 100.0);

        }
        else if(mb2.activeInHierarchy)
        {
            
            debug.text = "MB2 => x angle: " + (int)mb2.transform.localEulerAngles.x + " y angle: " + (int)mb2.transform.localEulerAngles.y +
            " z angle: " + (int)mb2.transform.localEulerAngles.z;
            
        }
        else
        {
            
        }
            //Debug.Log("x angl: " + transform.localEulerAngles.x);
            //Debug.Log("y angl: " + transform.localEulerAngles.y);
            //Debug.Log("z angl: " + transform.localEulerAngles.z);
        //}
    }
}
