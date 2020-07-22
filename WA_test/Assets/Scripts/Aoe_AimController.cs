using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aoe_AimController : MonoBehaviour
{
    public Sprite indicatorSprite;
    public Color indicatorColor;
    public float maxDistanceFromBody = 1;
    public float minDistanceFromBody = 10;
    public GameObject planeObject;

    //private objects
    private SpriteRenderer m_spriteRenderer;
    //find the indicator child
    private GameObject indicator;
    //skill activation stat
    private bool aoeON;
    //make new plane object for mouse raycast
    Plane plane;

    // Start is called before the first frame update
    void Start()
    {
        indicator = GameObject.Find("indicator Sprite");
        //get the sprite renderer
        m_spriteRenderer = indicator.GetComponent<SpriteRenderer>();

        //setup the sprite to the renderer
        m_spriteRenderer.sprite = indicatorSprite;

        //setup the desired color to the renderer
        m_spriteRenderer.color = indicatorColor;

        //reset inital stat to false
        aoeON = false;

        //inital new mouse plane at y=0;
        plane = new Plane(Vector3.up, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        //activate the skill
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            aoeON = true;
            indicator.SetActive(true);
        }

        // if the skill is active, enable the indicator
        if (aoeON)
        {
            //position;
            Vector3 position = -Vector3.one;

            //mouse position on the plane
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distanceToPlane;
            if (plane.Raycast(ray, out distanceToPlane))
            {
                position = ray.GetPoint(distanceToPlane);
            }
            //set indicator point to the mouse position on the mouse plane and add 0.01 to y to avoid sprite artifacts
            indicator.transform.position = position + new Vector3(0f, 0.01f, 0f);

            if (Input.GetMouseButton(0))
            {
                aoeON = false;
                indicator.SetActive(false);
            }
        }

    }
}
