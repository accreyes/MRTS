using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RAMText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayedMessage;
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
                
                displayedMessage.text = "Open port locks";
            }
            else if(timer<=3f)
            {
                displayedMessage.text = "Insert RAM Card";
                
            }
            else
            {
                timer = 0;
            }
            timer += Time.deltaTime;

        }
    }
}
