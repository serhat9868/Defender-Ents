using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectDrag : MonoBehaviour
{

    private Vector3 offset;
    private bool isPlaced = false;
    private bool isPlacingMode = true;
    private bool isdid = false;
    private bool isNecessary = false;

    private void Update()
    {
        return;
        if (isPlacingMode)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("is not pressed");
                GetComponent<SpriteRenderer>().color = Color.white;
                isPlacingMode = false;
                isPlaced = true;
                return;
            }
            if (Input.GetMouseButton(0))
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
                Vector2 pos = BuildingSystem.GetMouseWorldPosition() + offset;
                transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
            }          
        }
    }

#if isNecessary == true
    private void OnMouseDown()
    {
        if (isPlaced) return;
        isPlacingMode = true;
        GetComponent<SpriteRenderer>().color = Color.blue;
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        if (isPlaced) return;
        Vector2 pos = BuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
    }

    private void OnMouseUp()
    {
        isPlacingMode = false;
        isPlaced = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
#endif
    public bool IsPlacingMode()
    {
        return isPlacingMode;
    }



}
