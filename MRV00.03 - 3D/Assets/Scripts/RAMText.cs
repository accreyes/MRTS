using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RAMText : MonoBehaviour
{
    [SerializeField] private Text displayedMessage;
    //private string message;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeInHierarchy)
        {
            if(timer <= 1.5f)
            {
                
                displayedMessage.text = "Open port locks (highlighted with red circles)";
            }
            else if(timer<=3f)
            {
                displayedMessage.text = "Insert card pins into the slot ";
                
            }
            else
            {
                timer = 0;
            }
            timer += Time.deltaTime;

        }
    }
}
