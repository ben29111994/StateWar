using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject Target;
    public string Sender;
    public Color32 CL;
    public Color32 InsideCL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position , Target.transform.position , 5 *Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.tag != this.gameObject.transform.tag && other.transform.name == "Unit")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
