using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenClickTrigger : MonoBehaviour, IInteractable
{
    [Header("Animation Settings")]
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName = "Interact";


    public void Interact()
    {
        if (animator != null && !string.IsNullOrEmpty(triggerName))
        {
            animator.SetTrigger(triggerName);
            Debug.Log($"{gameObject.name} animator triggered: {triggerName}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no animator or trigger set.");
        }
    }
    public bool IsAvailable()
    {
        return true;
    }

    //void FixedUpdate()
    //{

    //    RaycastHit hit;
    //    // Does the ray intersect any objects excluding the player layer
    //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))

    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
    //        Debug.Log("Did Hit");
    //    }
    //    else
    //    {
    //        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
    //        Debug.Log("Did not Hit");
    //    }

    //}
}