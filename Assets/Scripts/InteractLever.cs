using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractLever : MonoBehaviour
{
    public float interactDistance = 3f;

    [SerializeField] private LayerMask objectLayer;

    private Lever leverInteract;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) TryInteract();

    }

    private void OnDrawGizmos()
    {
        if (Camera.main == null)
            return;

        Gizmos.color = Color.yellow;

        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * interactDistance;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.05f);
    }

    void TryInteract()
    {
        // Try to grab
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, objectLayer))
        {
            Lever leverInteract = hit.collider.GetComponent<Lever>();
            if (leverInteract)
            {
                leverInteract.ActivateLever();
            }
        }
    }

}
