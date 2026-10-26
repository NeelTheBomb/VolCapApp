using UnityEngine;
using UnityEngine.InputSystem;

public class Calib : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty triggerAction;   // Assign 'XRI LeftHand Trigger' here

    [Header("Scene References")]
    public Transform home; // The target (Maze/Anchor)
    public Transform rig;  // The XR Origin/Rig

    [Header("Calibration Settings")]
    [Tooltip("Adjust this slider to fix the rotation error.")]
    [Range(-180, 180)] public float rotationOffset = 0f; 
    
    public bool alignYawToHome = true;
    public Vector2 offsetForwardUp = Vector2.zero;
    public float triggerThreshold = 0.8f;

    private bool wasDownLastFrame = false;
    

    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();
        bool isDown = triggerValue > triggerThreshold;
        
        if (isDown && !wasDownLastFrame)
        {
            Calibrate();
        }
        wasDownLastFrame = isDown;
    }

    public void Calibrate()
    {
        if (home == null || rig == null)
        {
            Debug.LogError("Assign 'Home' and 'Rig' in the Inspector!");
            return;
        }

        if (alignYawToHome)
        {
            // Calculate difference between Controller and Home
            float currentYaw = transform.eulerAngles.y;
            float targetYaw = home.eulerAngles.y;
            float deltaYaw = Mathf.DeltaAngle(currentYaw, targetYaw);

            // Add the Manual Offset
            deltaYaw += rotationOffset;

            // Rotate the Rig
            rig.RotateAround(transform.position, Vector3.up, deltaYaw);
            
            Debug.Log($"Calibrated. Added Offset: {rotationOffset}. Delta applied: {deltaYaw}");
        }

        // Move Rig to Position
        Vector3 desiredCtrlPos = home.position + home.forward * offsetForwardUp.x + Vector3.up * offsetForwardUp.y;
        Vector3 delta = desiredCtrlPos - transform.position;
        rig.position += delta;
    }
}