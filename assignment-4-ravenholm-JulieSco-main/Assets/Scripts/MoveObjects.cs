using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.XR;


public class MoveItem : MonoBehaviour
{
    [SerializeField] private float maxGrabDistance = 10f, lerpSpeed = 10f, throwForce = 20f;
    [SerializeField] private float holdDistance = 3f;
    [SerializeField] private LayerMask grabbableLayer;

    private Camera mainCamera;
    private GameObject heldObject;
    private Rigidbody heldRigidbody;
    private Vector3 targetPosition;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }
        else if (Input.GetMouseButtonDown(1) && heldObject != null) 
        {
            ReleaseObject();
        }

        if (heldObject != null)
        {
            UpdateHeldObjectPosition();
        }
    }

    private void TryGrabObject()
    {
        if (heldObject != null) return;

        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxGrabDistance, grabbableLayer))
        {
            heldObject = hit.collider.gameObject;
            heldRigidbody = heldObject.GetComponent<Rigidbody>();

            if (heldRigidbody != null)
            {
                heldRigidbody.useGravity = false;
                heldRigidbody.freezeRotation = true;
            }
        }
    }

    private void UpdateHeldObjectPosition()
    {

        targetPosition = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;

        if (heldRigidbody != null)
        {
            Vector3 lerpedPosition = Vector3.Lerp(heldObject.transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            heldRigidbody.MovePosition(lerpedPosition);
        }
        else
        {
            heldObject.transform.position = targetPosition;
        }
    }

    private void ReleaseObject()
    {
        if (heldRigidbody != null)
        {
            heldRigidbody.useGravity = true;
            heldRigidbody.freezeRotation = false;

            heldRigidbody.AddForce(mainCamera.transform.forward * throwForce, ForceMode.Impulse);
        }

        heldObject = null;
        heldRigidbody = null;
    }

}
