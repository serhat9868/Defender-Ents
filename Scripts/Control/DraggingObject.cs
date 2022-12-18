using UnityEditor;
using UnityEngine;

public class DraggingObject : MonoBehaviour
{

    bool draggingMode = false;

    public bool IsDraggingMode()
    {
        return draggingMode;
    }

    public void SetDraggingMode(bool state)
    {
        draggingMode = state;
    }
}
