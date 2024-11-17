using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberalMode : GameMode_StateMachine
{
    private GameObject selectedGameObject;

    private Vector3 mouse_position;
    private Vector3 offset, mouseStartPos;
    private float depth;



    public override void HandleInput()
    {
        base.HandleInput();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f, ~LayerMask.GetMask("Environment")))
            {
                selectedGameObject = hit.collider.gameObject;
                depth = hit.distance;
                mouseStartPos = ray.origin + ray.direction * depth;
                offset = selectedGameObject.transform.position - mouseStartPos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            selectedGameObject = null;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f, ~LayerMask.GetMask("Environment")))
            {
                if (hit.collider.gameObject.GetComponent<Function>() != null)
                {
                    hit.collider.gameObject.GetComponent<Function>().enabled = hit.collider.gameObject.GetComponent<Function>().enabled ? false : true;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameMode.gameMode = GetComponent<CreativeMode>();
            gameMode.gameMode.enabled = true;
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (selectedGameObject != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                selectedGameObject.transform.position = ray.origin + ray.direction * depth + offset;
            }
        }
    }
}
//public Camera game_camera;
//public Collider coll;
//Vector3 mouse_position;
//Vector3 offset, mouseStartPos;
//float depth;

//void Start()
//{
//    coll = GetComponent<Collider>();
//}
//void Update()
//{
//    //mouse_position = game_camera.ScreenToWorldPoint(Input.mousePosition);
//    Ray ray = game_camera.ScreenPointToRay(Input.mousePosition);
//    RaycastHit hit;

//    if (coll.Raycast(ray, out hit, 100.0f))
//    {
//        if (Input.GetMouseButtonDown(0))
//        {

//            depth = hit.distance;
//            mouseStartPos = ray.origin + ray.direction * depth;
//            offset = selectedGameObject.transform.position - mouseStartPos;
//        }
//        if (Input.GetMouseButton(0))
//        {
//            selectedGameObject.transform.position = ray.origin + ray.direction * depth + offset;
//        }
//    }
//}