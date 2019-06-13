using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using TMPro;

public class InitScript : MonoBehaviour
{
    private bool isGPUFound;
    private bool isRJ45Found;
    private bool isRJ11Found;
    private bool isRAMFound;
    private int counter=0;
    [SerializeField] GameObject mb1;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI mainTextRight;
    
    // Start is called before the first frame update
    void Start()
    {
        bool boolean = false;
        isGPUFound = boolean;
        isRJ45Found = boolean;
        isRJ11Found = boolean;
        isRAMFound = boolean;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRJ11Found && isRJ45Found && isRAMFound && isGPUFound)
        {
            mb1.gameObject.SetActive(true);
            Destroy(this);
        }
    }
    public void GPUFound()
    {
        if(isRJ11Found && isRJ45Found && isRAMFound && isGPUFound)
        {

        }
        else if (isRJ11Found && isRJ45Found && isRAMFound)
        {
            UpdateMainTexts("You have scanned all four parts. Return the Phone Card to it's previous place and then look at the motherboard.");
            mb1.gameObject.GetComponent<MB1Checker>().InitDone();
            isGPUFound = true;

        }
        else if (!isGPUFound)
        {
            isGPUFound = true;
            counter++;  
                UpdateMainTexts("You have scanned the GPU Card. Return the GPU Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");

        }
        else
        {            

                UpdateMainTexts("You have already scanned this part. Return the GPU Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
            
        }
    }
    public void RJ45Found()
    {
        if (isRJ11Found && isRJ45Found && isRAMFound && isGPUFound)
        { }
        else if (isRJ11Found && isRAMFound && isGPUFound)
        {
            UpdateMainTexts("You have scanned all four parts. Return the Phone Card to it's previous place and then look at the motherboard.");
            mb1.gameObject.GetComponent<MB1Checker>().InitDone();
            isRJ45Found = true;
        }
        else if (!isRJ45Found)
        {
            isRJ45Found = true;
            counter++;
            
                UpdateMainTexts("You have scanned the LAN Card. Return the LAN Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
            
        }
        else
        {
            

                UpdateMainTexts("You have already scanned this part. Return the LAN Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
            
        }
    }
    public void RJ11Found()
    {
        if (isRJ11Found && isRJ45Found && isRAMFound && isGPUFound)
        { }
        else if (isRAMFound && isRJ45Found && isGPUFound)
        {
            UpdateMainTexts("You have scanned all four parts. Return the Phone Card to it's previous place and then look at the motherboard.");
            mb1.gameObject.GetComponent<MB1Checker>().InitDone();
            isRJ11Found = true;
        }
        else if (!isRJ11Found)
        {
            isRJ11Found = true;
            counter++;


            UpdateMainTexts("You have scanned the Phone Card. Return the Phone Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");

        }
        else
        {
            
            
            UpdateMainTexts("You have already scanned this part. Return the Phone Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
            
        }
    }
    public void RAMFound()
    {
        if (isRJ11Found && isRJ45Found && isRAMFound && isGPUFound)
        { }
        else if (isRJ11Found && isRJ45Found && isGPUFound)
        {
            UpdateMainTexts("You have scanned all four parts. Return the RAM Card to it's previous place and then look at the motherboard.");
            mb1.gameObject.GetComponent<MB1Checker>().InitDone();
            isRAMFound = true;
        }
        else if (!isRAMFound)
        {
            isRAMFound = true;
            counter++;
            UpdateMainTexts("You have scanned the RAM Card. Return the RAM Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
        }
        else
        {
          
                UpdateMainTexts("You have already scanned this part. Return the RAM Card to it's previous place and then pick up another part to scan. Parts found: " + counter + "/4");
            
        }
    }

    public void UpdateMainTexts(string s)
    {
        mainText.text = s;
        mainTextRight.text = s;
    }


}
