using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class VRDrawer : MonoBehaviour
{
    [Header("Drawing Settings")]
    public GameObject linePrefab;
    public Transform tip;
    public Transform annotationContainer;

    [Header("Input Actions")]
    // Drag your "XRI RightHand/Activate" or "Trigger" action here
    public InputActionReference drawAction;

    private LineRenderer currentLine;
    private List<Vector3> points = new List<Vector3>();
    private bool isDrawing = false;

    void Update()
    {
        // Check the trigger value from the Action Reference
        float triggerValue = drawAction.action.ReadValue<float>();

        if (triggerValue > 0.1f && !isDrawing)
        {
            StartDrawing();
        }
        else if (triggerValue > 0.1f && isDrawing)
        {
            UpdateDrawing();
        }
        else if (triggerValue < 0.1f && isDrawing)
        {
            StopDrawing();
        }
    }

    void StartDrawing()
    {
        isDrawing = true;
        // Instantiate as a child of the container immediately
        GameObject newLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, annotationContainer);
        currentLine = newLine.GetComponent<LineRenderer>();
        points.Clear();

        AddPoint();
    }

    void UpdateDrawing()
    {
        // Optimization: Only add a point if the hand has moved 1cm
        if (points.Count > 0 && Vector3.Distance(points[points.Count - 1], tip.position) > 0.01f)
        {
            AddPoint();
        }
    }

    void AddPoint()
    {
        points.Add(tip.position);
        currentLine.positionCount = points.Count;
        currentLine.SetPosition(points.Count - 1, tip.position);
    }

    void StopDrawing()
    {
        isDrawing = false;
        currentLine = null;
    }
}