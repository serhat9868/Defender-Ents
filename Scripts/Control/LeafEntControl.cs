using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafEntControl : MonoBehaviour
{
    [SerializeField] GameObject leaf;
    [SerializeField] float leafSpawningRate = 2f;
    float time = 0;
    void Update()
    {
        if (GetComponent<DraggingObject>().IsDraggingMode()) return;

        time += Time.deltaTime;
        if(time > leafSpawningRate)
        {
            Instantiate(leaf, transform.position, Quaternion.identity);
            time = 0;
        }
    }
}
