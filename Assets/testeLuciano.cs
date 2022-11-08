using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testeLuciano : MonoBehaviour
{

    public float q1, q2, q3, q4;
    private Quaternion q;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.localRotation = new Quaternion(q1, q2, q3, q4);
        
        q = transform.localRotation ;

        print(q);
        
    }
}
