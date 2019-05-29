using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AngleCheck : MonoBehaviour
{
    [SerializeField] private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //target.transform.Rotate(0, 10, 0);
            Debug.Log("x angl: " + transform.localEulerAngles.x);
            Debug.Log("y angl: " + transform.localEulerAngles.y);
            Debug.Log("z angl: " + transform.localEulerAngles.z);
        }
    }
}
