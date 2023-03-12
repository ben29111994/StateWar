using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPosition : MonoBehaviour
{
    public string MyTag;
    public GameObject MyArea;
    public string MyAreaName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        //if(other.transform.name != "Unit" && other.transform.name != MyAreaName)
        //{
        //    MyArea.GetComponent<Area>().Attack = 1;
        //    MyArea.GetComponent<Area>().Target = other.gameObject;
            
        //}
    }
}
