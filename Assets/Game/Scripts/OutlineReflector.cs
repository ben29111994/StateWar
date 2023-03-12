using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineReflector : MonoBehaviour
{
    public Renderer target;
    Renderer thisOutline;
    // Start is called before the first frame update
    void Start()
    {
        thisOutline= GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        thisOutline.material.mainTexture = target.material.mainTexture;
    }
}
