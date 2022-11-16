using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CenterBounds : MonoBehaviour
{
    public MeshFilter meshFilter;
    public GameObject bolinhas1;
    public GameObject bolinhas2;
    public GameObject bolinhas3;
    public GameObject bolinhas4;

    public GameObject cranio;
    public GameObject tumor;
    public GameObject pivotTry;
    public GameObject spheretTTT;
    public GameObject unidos;

    float media_x;
    float media_y;
    float media_z;

    void Start(){
        media_x = (bolinhas1.transform.position.x + bolinhas2.transform.position.x + bolinhas3.transform.position.x + bolinhas4.transform.position.x)/4 ;
        media_y = (bolinhas1.transform.position.y + bolinhas2.transform.position.y + bolinhas3.transform.position.y + bolinhas4.transform.position.y)/4 ;
        media_z = (bolinhas1.transform.position.z + bolinhas2.transform.position.z + bolinhas3.transform.position.z + bolinhas4.transform.position.z)/4 ;

        // Vector3 centro = meshFilter.mesh.bounds.center;
        // centro.x = - centro.x;
        // centro.y = - centro.y;
        // centro.z = - centro.z;

        pivotTry.transform.position = new Vector3(media_x, media_y, media_z);
        unidos.transform.parent = pivotTry.transform;
        // unidos.transform.parent = spheretTTT.transform;
        // cranio.transform.parent = pivotTry.transform;
        // tumor.transform.parent = pivotTry.transform;

        // cranio.transform.position= new Vector3(( media_x)* 0.001f,( media_y)* 0.001f ,( media_z)* 0.001f);
        // tumor.transform.position= new Vector3(( media_x)* 0.001f,( media_y)* 0.001f ,( media_z)* 0.001f);

        // cranio.transform.position= new Vector3((centro.x + media_x)* 0.001f,(centro.y + media_y)* 0.001f ,(centro.z + media_z)* 0.001f);
        // tumor.transform.position= new Vector3((centro.x + media_x)* 0.001f,(centro.y + media_y)* 0.001f ,(centro.z + media_z)* 0.001f);

    }
    
}
