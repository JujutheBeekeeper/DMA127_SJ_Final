using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFirstPersonController : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    private Vector2 turn;
    private float targetAngle;

    [Header("Movement Settings")]
    [Tooltip("Movement speed along X-Z plane")]
    public float speed = 6.0f;

    [Tooltip("Gravity strength applied when airborne")]
    public float customGravity = 15.0f;

    [Header("Dialogue Integration")]
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Interaction Settings")]
    [Tooltip("Maximum distance for interactable raycast")]
    public float interactDistance = 3f;

    [Header("Crosshair UI")]
    [SerializeField] private Image crosshair;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color interactColor = Color.green;

    void Start()
    {
        //hide cursor
        Cursor.visible = false;

        //get the character controller component attached to this object.
        characterController = GetComponent<CharacterController>();
        turn = Vector2.zero;

    }

    void Update()
    {
        // If dialogue is active, intercept input
        if (dialogueManager != null && dialogueManager.IsDialoguePlaying)
        {
            if (Input.GetButtonDown("Submit")) // "Submit" is usually mapped to Enter/Space
            {
                dialogueManager.ContinueDialogue();
            }
            return; // Skip movement while dialogue is active
        }

        // Otherwise, handle normal movement input
        HandleMovement();

        HandleRaycast();
    }

    private void HandleRaycast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>()
                                       ?? hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                // Check availability before changing crosshair
                if (interactable.IsAvailable())
                {
                    if (crosshair != null)
                        crosshair.color = interactColor;

                    if (Input.GetMouseButtonDown(0))
                    {
                        interactable.Interact();
                    }
                }
                else
                {
                    // Not available → keep default color
                    if (crosshair != null)
                        crosshair.color = defaultColor;
                }

                return; // stop here so we don’t reset the crosshair again
            }
        }

        // No interactable hit → reset to default
        if (crosshair != null)
        {
            crosshair.color = defaultColor;
        }
    }


    private void HandleMovement()
    {
        //camera rotation via mouse movt
        turn.x += Input.GetAxis("Mouse X");
        turn.y += Input.GetAxis("Mouse Y");
        Camera.main.transform.localRotation = Quaternion.Euler(-turn.y, 0.0f, 0.0f);
        transform.localRotation = Quaternion.Euler(0.0f, turn.x, 0.0f);

        //access horizontal and vertical input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //get our movement direction along X-Z plane (ie. sideways and back-and-forth directions)
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical);
        targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

        if (characterController.isGrounded)
        {
            // move along XZ plane at defined speed, following camera orientation
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed * inputDirection.magnitude;
        }
        else
        {
            //custom gravity solver
            moveDirection.y -= customGravity * Time.deltaTime;
        }

        //finally, move our character controller in this direction 
        characterController.Move(moveDirection * Time.deltaTime);
    }


}
