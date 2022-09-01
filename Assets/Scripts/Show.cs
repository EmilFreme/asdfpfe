using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Show : MonoBehaviour
{
    public GameObject skull;
    public GameObject tumor;


    public void ShowAll(){
        if(!skull.activeSelf || !tumor.activeSelf){
            skull.SetActive(true);
            tumor.SetActive(true);     
        }else{
            skull.SetActive(false);
            tumor.SetActive(false); 
        }
    }

    public void ShowTumor(){
        skull.SetActive(false);
        tumor.SetActive(true);
    }

    public void ShowSkull(){
        skull.SetActive(true);
        tumor.SetActive(false);
    }
}
