using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ConfirmButton : MonoBehaviour,IVirtualButtonEventHandler
{
    [SerializeField] private GameObject GPU;
    [SerializeField] private GameObject RJ45;
    [SerializeField] private GameObject RAM;
    [SerializeField] private GameObject RJ11;
    [SerializeField] private GameObject Red;
    [SerializeField] private GameObject Green;
    private bool isGPU =false;
    private bool isRJ45 =false;
    private bool isRJ11 =false;
    private bool isRAM =false;
    private bool isRed =false;
    private bool isGreen = false;

    private bool isCorrect = false;
    private int state = 0;

    // Start is called before the first frame update
    void Start()
    {
       this.gameObject.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        

        Debug.Log("virtual button: on button pressed");
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("virtual button: on button released");
     
       
    }

    public void setCorrectTrue()
    {
        isCorrect = true;
    }
    public void setCorrectFalse()
    {
        isCorrect = false;
    }


    /*if(isGPU)
        {
            if(isRJ45)
            {
                
                if(isRJ11)
                {
                    Debug.Log("isRJ11 = true");
                    if (isRAM)
                    {
                        Debug.Log("isRAM = true");
                    }
                    else
                    {
                        RAM.SetActive(true);
                        RJ11.SetActive(false);
                        isRAM = true;
                    }
                }
                else
                {
                    RJ11.SetActive(true);
                    RJ45.SetActive(false);
                    isRJ11 = true;
                }

            }
            else
            {
                //GPU.SetActive(false);
                RJ45.SetActive(true);
                isRJ45 = true;
            }
        }
        else
        {
            GPU.SetActive(false);
            if (isRed)
            {
                Red.gameObject.SetActive(false);
                if (isGreen)
                {
                    Green.gameObject.SetActive(false);
                    isGPU = true;

                }
                else
                {
                    isGreen = true;
                    Green.gameObject.SetActive(true);
                }
            }
            else
            {
                Red.gameObject.SetActive(true);
                isRed = true;
            }
        }*/
}
