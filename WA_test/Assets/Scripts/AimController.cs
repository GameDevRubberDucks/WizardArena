﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public enum SpellIndicatorType
{
    AOE,
    CONE,
    BEAM,
    PROJECTILE,
    OFF,
    Num_Types
}

public class AimController : MonoBehaviour
{

    public GameObject indicator;
    public GameObject spellSpawnPoint;
    //universal indicator traits
    [Serializable]
    public struct IndicatorType
    {
        public SpellIndicatorType type;
        public Sprite indicatorSprite;
        public Color indicatorColor;
        public float maxDistanceFromBody;
    }

    public IndicatorType[] indicatorTypes;


    //private objects
    private SpriteRenderer m_spriteRenderer;
    //find the indicator child
    private GameObject indicatorSprite;

    //make new plane object for mouse raycast 
    //this is not a 3d plane mesh, but rather a math model to represent a plane.
    // refer to this Doc-> https://docs.unity3d.com/ScriptReference/Plane.html
    private Plane plane;

    //indicator identifiter;
    private SpellIndicatorType spellIndicatorType;


    // Start is called before the first frame update
    void Start()
    {
        indicatorSprite = GameObject.Find("indicator Sprite");
        //get the sprite renderer
        m_spriteRenderer = indicatorSprite.GetComponent<SpriteRenderer>();

        indicator.SetActive(false);

        //inital new mouse plane at y=0;
        plane = new Plane(Vector3.up, 0.0f);

        //init spell indicator type
        spellIndicatorType = SpellIndicatorType.OFF;
    }

    // Update is called once per frame
    void Update()
    {
        //################## SPELL ACTIVITION ##################
        //activate AOE
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    IndicatorToggle(SpellIndicatorType.AOE);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    IndicatorToggle(SpellIndicatorType.CONE);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    IndicatorToggle(SpellIndicatorType.BEAM);
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    IndicatorToggle(SpellIndicatorType.PROJECTILE);
        //}

        //################## SPELL BEHAVIOUR ##################
        // if the skill is active, behave as follows
        if (spellIndicatorType == SpellIndicatorType.AOE)
        {
            //set indicator point to the mouse position on the mouse plane and add 0.01 to y to avoid sprite artifacts
            indicator.transform.position = GetMousePositionOnPlane() + new Vector3(0f, 0.01f, 0f);

            ////De-activate the spell
            //if (Input.GetMouseButton(0))
            //{
            //    DeactivateIndicator();
            //}
        }
        else if (spellIndicatorType == SpellIndicatorType.CONE || spellIndicatorType == SpellIndicatorType.BEAM || spellIndicatorType == SpellIndicatorType.PROJECTILE)
        {
            //set indicator point to the mouse position on the mouse plane and add 0.01 to y to avoid sprite artifacts

            Vector3 pos_temp = GetMousePositionOnPlane() - indicator.transform.position;
            //lock the sprite to the x,z plane
            pos_temp.y = 0f;
            //set forward to indicat
            indicator.transform.forward = pos_temp;

            ////De-activate the spell
            //if (Input.GetMouseButton(0))
            //{
            //    DeactivateIndicator();
            //}
        }



    }

    //return the mouse position on the floor plane based base on camera
    Vector3 GetMousePositionOnPlane()
    {

        //mouse position on the plane
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distanceToPlane;
        if (plane.Raycast(ray, out distanceToPlane))
        {
            return ray.GetPoint(distanceToPlane);
        }
        else
        {
            return new Vector3(0f, 0f, 0f);
        }
    }

    //deactivate the current spell
    public void DeactivateIndicator()
    {
        spellIndicatorType = SpellIndicatorType.OFF;
        //disable the indicator sprite object
        indicator.SetActive(false);
        indicator.transform.position = gameObject.transform.position;
    }

    public void IndicatorToggle(SpellIndicatorType type)
    {
        if(spellIndicatorType == type)
        {
            DeactivateIndicator();
            return;
        }
        spellIndicatorType = type;
        //set the sprite
        m_spriteRenderer.sprite = indicatorTypes[(int)type].indicatorSprite;
        //set the color
        m_spriteRenderer.color = indicatorTypes[(int)type].indicatorColor;
        //set indicator scale based on the max distance
        indicator.transform.localScale = new Vector3(indicatorTypes[(int)type].maxDistanceFromBody, indicatorTypes[(int)type].maxDistanceFromBody, indicatorTypes[(int)type].maxDistanceFromBody);
        //reset position to self in case spell was cancel
        indicator.transform.position = gameObject.transform.position;

        //enable the indicator sprite object
        indicator.SetActive(true);
    }

    public GameObject GetSpellSpawnPoint()
    {
        return spellSpawnPoint;
    }
}
