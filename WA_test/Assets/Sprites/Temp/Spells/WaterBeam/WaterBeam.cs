using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBeam : MonoBehaviour
{
    float scrollSpeed = -5.0f;
    public Renderer rend;
    //public LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        //lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
