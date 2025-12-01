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

}
