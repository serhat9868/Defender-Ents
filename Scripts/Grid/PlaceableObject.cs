using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
   public bool Placed { get; private set; }
   public Vector3Int Size { get; private set; }
    private Vector3[] Vertices;

    private void GetColliderVertexPositionLocal()
    {
        BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Vertices = new Vector3[4];

        Vertices[0] = boxCollider2D.offset + new Vector2(-boxCollider2D.size.x, -boxCollider2D.size.y) * 0.5f;
        Vertices[1] = boxCollider2D.offset + new Vector2(boxCollider2D.size.x, -boxCollider2D.size.y) * 0.5f;
        Vertices[2] = boxCollider2D.offset + new Vector2(boxCollider2D.size.x, boxCollider2D.size.y) * 0.5f;
        Vertices[3] = boxCollider2D.offset + new Vector2(-boxCollider2D.size.x, boxCollider2D.size.y) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] vertices = new Vector3Int[Vertices.Length];

        for(int i = 0; i < vertices.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(Vertices[i]);
            vertices[i] = BuildingSystem.current.gridLayout.WorldToCell(worldPos);
        }

        Size = new Vector3Int(Mathf.Abs((vertices[0] - vertices[1]).x), Mathf.Abs((vertices[0] - vertices[3]).y));
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(Vertices[0]);
    }

    private void Start()
    {
        GetColliderVertexPositionLocal();
        CalculateSizeInCells();
    }

    public virtual void Place()
    {
        ObjectDrag objectDrag = gameObject.GetComponent<ObjectDrag>();
        Destroy(objectDrag);

        Placed = true;

        //invoke event of placement
    }
}
