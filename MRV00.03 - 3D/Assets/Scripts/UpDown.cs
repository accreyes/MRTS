using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    private float time;
    private bool up = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(time>=1)
        {
            if(up==true)
            {
                transform.Translate(new Vector3(0, -0.01f, 0));
                up = false;
            }
            else
            {
                transform.Translate(new Vector3(0, 0.01f, 0));
                up = true;
            }
            time = 0;
        }
        else
        {
            time = time + Time.deltaTime;
        }
    }
}
