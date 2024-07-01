using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private TrailRenderer trailRenderer;

    private BoxCollider boxCollider;

    private Vector3 mousePos;

    private GameMenager gameMenager;

    private Camera mainCamera;

    private bool swiping = false;   

    // Start is called before the first frame update
    void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();
        gameMenager = GameObject.Find("Game Menager").GetComponent<GameMenager>();
        mainCamera = Camera.main;
        boxCollider.enabled = false;
        trailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMenager.isGameActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }   
            else if(Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if(swiping)
            {
                UpdateMousePos();
            }
        }
       
    }

    void UpdateMousePos()
    {
        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
            Input.mousePosition.y, 10.0f)); // 10.0f is the distance from the camera to the object
            //SreenToWorldPoint converts the screen point of the mouse to a world point
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trailRenderer.enabled = swiping;
        boxCollider.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Targets>())
        {
            collision.gameObject.GetComponent<Targets>().DestroyTarget();
        }
    }

}
