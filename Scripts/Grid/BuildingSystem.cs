using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    public static BuildingSystem current;
    public GridLayout gridLayout;
    public GameObject prefab1;
    public GameObject prefab2;

    [Header("Grid Boundary")]
    public float maxX;
    public float minX;
    public float minY;
    public float maxY;

    private Grid grid;   
    private TileBase whiteTile;
    private PlaceableObject objectToPlace;
    Dictionary<Vector3, string> tilePoint = new Dictionary<Vector3, string>();
    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    public bool IsInGrid(Transform entTransform)
    {
        if(entTransform.position.x < maxX && entTransform.position.x > minX 
            && entTransform.position.y < maxY && entTransform.position.y > minY)
        {
            return true;
        }

        return false;
    }

    public Dictionary<Vector3,string> GetTileDictionary()
    {
        return tilePoint;
    }

    public void AddToDictionary(Vector3 key,string value)
    {
        tilePoint.Add(key, value);
    }
    public void RemoveFromDictionary(GameObject entToRemove)
    {
        tilePoint.Remove(entToRemove.transform.position);
    }


    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if(Physics2D.Raycast(ray,Vector2.zero))
        {
            return ray;
        }
        else
        {
            return Vector2.zero;
        }
    }
    
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area,Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;

        foreach(var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }


    #endregion

    #region Building Placement

    public void InitalizeWithObject(GameObject prefab)
    {
        Vector3 clickedMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = SnapCoordinateToGrid(clickedMousePosition);
        position.z = 0;
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlaceableObject>();
        obj.AddComponent<ObjectDrag>();       
    }
    
    private bool CanBePlaced(PlaceableObject placeableObject)
    {
        BoundsInt area = new BoundsInt();

        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        TileBase[] baseArray = GetTilesBlock(area, tilemap);

        foreach(var b in baseArray)
        {
            if(b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start,Vector3Int size)
    {
        tilemap.BoxFill(start, whiteTile, start.x, start.y,
                        start.x + size.x, start.y + size.y);
    }
 
    public float GetMinY()
    {
        return minY;
    }

    public float GetMaxY()
    {
       return maxY;
    }
    #endregion
}

