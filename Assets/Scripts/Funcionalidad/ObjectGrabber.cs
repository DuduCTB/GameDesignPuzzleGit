using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectGrabber : MonoBehaviour
{

    public Camera cam;
    public float distance = 10f;

    public float grabDistance = 3f;
    public float moveForce = 50f;
    public Transform grabPoint;   // Empty object in front of the camera
    public float rotationSpeed = 100f;

    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private LayerMask UILayer;

    private Rigidbody grabbedObject;

    //public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            TryGrabOrDrop();

        if (grabbedObject)
            HandleRotationInput();

        CheckUIButton();

    }

    void FixedUpdate()
    {
        if (grabbedObject)
        {
            Vector3 direction = grabPoint.position - grabbedObject.position;
            grabbedObject.AddForce(direction * moveForce, ForceMode.Acceleration);
        }
    }

    private void OnDrawGizmos()
    {
        if (Camera.main == null)
            return;

        Gizmos.color = Color.red;

        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * grabDistance;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }

    void TryGrabOrDrop()
    {
        if (grabbedObject)
        {
            DropObject();
            return;
        }

        // Try to grab
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, grabDistance, objectLayer))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb)
            {
                grabbedObject = rb;
                grabbedObject.useGravity = false;
                grabbedObject.drag = 10;
            }
        }
    }

    void DropObject()
    {
        grabbedObject.useGravity = true;
        grabbedObject.drag = 0;
        grabbedObject = null;
    }

    private void HandleRotationInput()
    {
        float rotationAmount = 0f;

        if (Input.GetKey(KeyCode.Q))
            rotationAmount -= rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            rotationAmount += rotationSpeed * Time.deltaTime;

        if (rotationAmount != 0f)
        {
            grabbedObject.transform.Rotate(0f, rotationAmount, 0f, Space.World);
        }
    }

    private void CheckUIButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, grabDistance, UILayer))
            {
                ExecuteEvents.Execute(hit.collider.gameObject,
                new PointerEventData(eventSystem),
                ExecuteEvents.pointerClickHandler);

                Debug.Log("Click UI en: " + hit.collider.name);
            }
        }


    }

}
