using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    // Initial character controller based on this:
    // https://www.youtube.com/watch?v=_QajrabyTJc

    public CharacterController characterController;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Light torch;
    public Camera playerCamera;

    public float speed = 12f;
    public float gravity = -9.8f;
    public float groundDistance = 0.4f;
    public bool isMovementEnabled = true;
    
    private Vector3 velocity;
    public bool isGrounded;
    public bool isMoving;
    private bool isTorchOn = false;
    private bool raycastActive = false;
    private string torchCondition = "FlashlightUsed";

    public List<GameObject> activeTriggers = new List<GameObject>();   
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovementEnabled)
        {
            // Check if grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // Get WASD input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            isMoving = (move.magnitude > 0.01f);

            // Move character
            characterController.Move(move * speed * Time.deltaTime);

            // Gravity
            velocity.y += gravity * Time.deltaTime;

            characterController.Move(velocity * Time.deltaTime);

            // Torch control
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isTorchOn)
                {
                    torch.intensity = 0f;
                }
                else
                {
                    torch.intensity = 1.34f;

                    if(!gameManager.hasUsedTorch && gameManager.activeObjective._ID == 1)
                    {
                        gameManager.hasUsedTorch = true;
                        gameManager.ConditionMet(torchCondition);
                    }
                }

                isTorchOn = !isTorchOn;
            }

            // Quest check
            if(isTorchOn && gameManager.activeObjective._ID == 1)
            {
                // Player turned the torch on before they got to the quest step...
                // Progress immediately because idk what to do otherwise
                gameManager.hasUsedTorch = true;
                gameManager.ConditionMet(torchCondition);
            }

            // Check raycasts if in trigger area.
            if (raycastActive)
            {
                RaycastHit hitInfo;

                Ray centerScreen = playerCamera.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));

                bool hit = Physics.Raycast(centerScreen, out hitInfo, 100f);

                if (hit)
                {
                    //Debug.DrawRay(centerScreen.origin, centerScreen.direction, Color.white);
                    Debug.Log(hitInfo.collider.gameObject.name);

                    if (hitInfo.collider.gameObject.CompareTag("RaycastCheck"))
                    {
                        // Interact with object.
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("Parent interaction");
                            hitInfo.collider.gameObject.GetComponent<Interaction>().Interact();
                        }
                    }
                    else if (hitInfo.collider.gameObject.CompareTag("RaycastChild"))
                    {
                        // Interact with parent of object.
                        if (Input.GetMouseButtonDown(0))
                        {
                            Debug.Log("Child interaction");
                            hitInfo.collider.gameObject.GetComponentInParent<Interaction>().Interact();
                        }
                    }
                }
            }

        }

        // Emails separate from movement in-game.
        if (Input.GetKeyDown(KeyCode.E))
        {
            gameManager.ToggleEmails();
            ToggleMovement(!gameManager.isEmailOpen);
        }
    }

    public void SetRaycastActive(GameObject obj)
    {
        raycastActive = true;
        activeTriggers.Add(obj);
    }

    public void DisableRaycast(GameObject obj)
    {
        // Check so that players can be in multiple object trigger areas at once.
        if(activeTriggers.Contains(obj))
        {
            activeTriggers.Remove(obj);
        }

        if(activeTriggers.Count == 0)
        {
            raycastActive = false;
        }
    }

    public void ToggleMovement(bool set)
    {
        isMovementEnabled = set;
    }
}
