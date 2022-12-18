using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField] float leaf = 100;
    [SerializeField] int numberOfAbilities = 1;
    [SerializeField] float magicalLeaf = 50;
    [SerializeField] CursorMapping[] cursorMappings = null;
    [SerializeField] GameObject vfx;

    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }

    void Update()
    {
        if (InteractWithComponent()) return;
        // SetCursor(CursorType.None);

        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(vfx);
        }
    }
    private bool InteractWithComponent()
    {
        RaycastHit2D[] hits = RaycastAllSorted();
        foreach (RaycastHit2D hit in hits)
        {
            IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
            foreach (IRaycastable raycastable in raycastables)
            {
                if (raycastable.HandleRaycast(this))
                {
                    SetCursor(raycastable.GetCursorType());
                    return true;
                }
            }
        }
        return false;
    }

    private void SetCursor(CursorType type)
    {
        CursorMapping mapping = GetCursorMapping(type);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }
    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach (CursorMapping cursorMapping in cursorMappings)
        {
            if (cursorMapping.type == type)
            {
                return cursorMapping;
            }
        }
        return cursorMappings[0];
    }
    private RaycastHit2D[] RaycastAllSorted()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(GetMousePosition(), Vector2.up);
        float[] distances = new float[hits.Length];

        for (int i = 0; i < hits.Length; i++)
        {
            distances[i] = hits[i].distance;
        }
        Array.Sort(distances, hits);
        return hits;
    }

    public float GetNumberOfAbilities()
    {
        return numberOfAbilities;
    }


    public static Vector2 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2 GetMapOrigin()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
    }

    // LEAF CONTROL
    public float GetLeaf()
    {
        return leaf;
    }
  
    public void EarnLeaf(float increasingAmount)
    {
        leaf += increasingAmount;
    }

    public void SpendLeaf(float decreasingAmount)
    {
        leaf -= decreasingAmount;
    }

    // MAGICAL LEAF CONTROL
    public float GetMagicalLeaf()
    {
        return magicalLeaf;
    }

    public void EarnMagicalLeaf(float increasingAmount)
    {
        magicalLeaf += increasingAmount;
    }
    public void SpendMagicalLeaf(float decreasingAmount)
    {
        magicalLeaf -= decreasingAmount;
    }
}
