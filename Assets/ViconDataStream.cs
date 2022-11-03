using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  ViconDataStreamSDK;

public class ViconDataStream : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
        // ViconDataStreamSDK.DotNET.Client MyClient = new ViconDataStreamSDK.DotNET.Client();
        // MyClient.Connect( "192.168.0.214:801"); //or the "hostname:port" e.g. 192.168.0.60:801
        // MyClient.EnableMarkerData();
        // MyClient.GetFrame(); //for repeated querying put in update function
        // print("pd ser");
        // Output_GetMarkerGlobalTranslation Output = MyClient.GetMarkerGlobalTranslation( "oculos", "" );
        // //for subject "Alice" and pelvis marker "LASI"
        // float xPos = (float)Output.Translation[0]/1000f; //marker global x-position in meters
        // float yPos = (float)Output.Translation[1]/1000f; //marker global y-position in meters
        // float zPos = (float)Output.Translation[2]/1000f; //marker global z-position in meters
        
    }
}
