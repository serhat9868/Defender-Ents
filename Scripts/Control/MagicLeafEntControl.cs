using System.Collections;
using UnityEngine;

public class MagicLeafEntControl : MonoBehaviour
{
    [SerializeField] GameObject magicLeaf;
    [SerializeField] float leafSpawningRate = 2f;
    float time = 0;
    void Update()
    {
        if (GetComponent<DraggingObject>().IsDraggingMode()) return;

        time += Time.deltaTime;
        if (time > leafSpawningRate)
        {
            Instantiate(magicLeaf, transform.position, Quaternion.identity);
            time = 0;
        }
    }
}