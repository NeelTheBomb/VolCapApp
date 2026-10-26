using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DrawInSpaceStrokesWithUndo : MonoBehaviour
{
    [Header("Input")]
    public InputActionProperty triggerAction;    // Right trigger
    public InputActionProperty undoAction;       // A button
    public InputActionProperty clearAction;      // B button

    [Header("Drawing Point")]
    public Transform tipTransform;               // Controller tip

    [Header("Line Settings")]
    public Material lineMaterial;
    public float lineWidth = 0.01f;
    public float minDistance = 0.01f;            // Distance between points

    private LineRenderer currentLine;
    private List<Vector3> currentPoints = new List<Vector3>();
    private List<GameObject> allStrokes = new List<GameObject>();
    private bool isDrawing = false;

    void Update()
    {
        float triggerValue = triggerAction.action.ReadValue<float>();

        // Handle drawing
        if (triggerValue > 0.8f && !isDrawing)
        {
            StartNewLine();
        }
        else if (triggerValue > 0.8f && isDrawing)
        {
            AddPoint();
        }
        else if (triggerValue < 0.1f && isDrawing)
        {
            EndLine();
        }

        // Handle undo (A button)
        if (undoAction.action.WasPressedThisFrame())
        {
            UndoLastStroke();
        }

        // Handle clear (B button)
        if (clearAction.action.WasPressedThisFrame())
        {
            ClearAllStrokes();
        }
    }

    void StartNewLine()
    {
        isDrawing = true;
        currentPoints.Clear();

        GameObject lineObj = new GameObject("LineStroke");
        lineObj.transform.parent = transform; // keep hierarchy organized

        currentLine = lineObj.AddComponent<LineRenderer>();
        currentLine.material = lineMaterial;
        currentLine.startWidth = lineWidth;
        currentLine.endWidth = lineWidth;
        currentLine.positionCount = 0;
        currentLine.numCapVertices = 4;
        currentLine.numCornerVertices = 4;

        allStrokes.Add(lineObj);

        AddPoint();
    }

    void AddPoint()
    {
        if (tipTransform == null) return;

        Vector3 currentPos = tipTransform.position;

        if (currentPoints.Count == 0 || Vector3.Distance(currentPoints[currentPoints.Count - 1], currentPos) > minDistance)
        {
            currentPoints.Add(currentPos);
            currentLine.positionCount = currentPoints.Count;
            currentLine.SetPosition(currentPoints.Count - 1, currentPos);
        }
    }

    void EndLine()
    {
        isDrawing = false;
        currentLine = null;
        currentPoints.Clear();
    }

    void UndoLastStroke()
    {
        if (allStrokes.Count > 0)
        {
            GameObject lastStroke = allStrokes[allStrokes.Count - 1];
            allStrokes.RemoveAt(allStrokes.Count - 1);
            Destroy(lastStroke);
        }
    }

    public void ClearAllStrokes()
    {
        foreach (var stroke in allStrokes)
        {
            Destroy(stroke);
        }
        allStrokes.Clear();
    }
}
