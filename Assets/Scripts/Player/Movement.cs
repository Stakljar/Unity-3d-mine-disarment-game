using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public static readonly string playerTag = "Player";
    const float cameraCrouchOffset = 0.4f;
    const float runningMultiplier = 1.5f;
    const float crouchingDivider = 2;
    const string animationWalkingBoolName = "isWalking";

    public string AnimationWalkingBoolName
    {
        get => animationWalkingBoolName;
    }

    const string animationRunningBool = "isRunning";

    [SerializeField]
    Transform cameraTransform;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AudioSource walkingSound;

    public AudioSource WalkingSound
    {
        get => walkingSound;
        set => walkingSound = value;
    }

    [SerializeField]
    AudioSource runningSound;

    public AudioSource RunningSound
    {
        get => runningSound;
        set => runningSound = value;
    }

    [SerializeField]
    float movementSpeed;

    bool isMovable = true;

    public bool IsMovable
    {
        get => isMovable;
        set => isMovable = value;
    }

    bool isMovementFreezeTriggered = false;
    Vector3 freezePosition = Vector3.zero;

    void Update()
    {
        if (isMovable)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (animator.GetBool(animationRunningBool) == true && !runningSound.isPlaying)
            {
                walkingSound.Stop();
                runningSound.Play();
            }
            else if (animator.GetBool(animationWalkingBoolName) == true && !walkingSound.isPlaying && !runningSound.isPlaying)
            {
                runningSound.Stop();
                walkingSound.Play();
            }
            else if (!animator.GetBool(animationWalkingBoolName))
            {
                walkingSound.Stop();
                runningSound.Stop();
            }

            float adjustedMovementSpeed = movementSpeed;

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                Vector3 newCameraPosition = cameraTransform.transform.position;
                newCameraPosition.y -= cameraCrouchOffset;
                cameraTransform.transform.position = newCameraPosition;

            }
            else if(Input.GetKeyUp(KeyCode.LeftControl))
            {
                Vector3 newCameraPosition = cameraTransform.position;
                newCameraPosition.y += cameraCrouchOffset;
                cameraTransform.position = newCameraPosition;
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                adjustedMovementSpeed /= crouchingDivider;
            }

            if (x != 0f || z != 0f)
            {
                animator.SetBool(animationWalkingBoolName, true);
            }
            else
            {
                animator.SetBool(animationWalkingBoolName, false);
            }

            if (Input.GetKey(KeyCode.LeftShift) && animator.GetBool(animationWalkingBoolName) == true)
            {
                adjustedMovementSpeed *= runningMultiplier;
                animator.SetBool(animationRunningBool, true);
            }
            else
            {
                animator.SetBool(animationRunningBool, false);
            }
            Vector3 movement = transform.right * x + transform.forward * z;
            GetComponent<CharacterController>().Move(movement * adjustedMovementSpeed * Time.deltaTime);
        }
        else
        {
            runningSound.Stop();
            walkingSound.Stop();
            animator.SetBool(animationRunningBool, false);
            animator.SetBool(animationWalkingBoolName, false);
            GetComponent<CharacterController>().Move(freezePosition);
            GetComponent<Rotation>().enabled = false;
            if (!isMovementFreezeTriggered && FindObjectOfType<MineManager>().DidAnyMineExplode)
            {
                cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y - 1.2f, cameraTransform.position.z);
                cameraTransform.rotation = Quaternion.Euler(cameraTransform.rotation.x, cameraTransform.rotation.y, cameraTransform.rotation.z - 20);
            }
            isMovementFreezeTriggered = true;
        }
    }

    public void FreezeMovement()
    {
        Cursor.lockState = CursorLockMode.Confined;
        isMovable = false;
        walkingSound.Stop();
        runningSound.Stop();
    }

    public void UnfreezeMovement()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isMovable = true;
        isMovementFreezeTriggered = false;
    }
}
