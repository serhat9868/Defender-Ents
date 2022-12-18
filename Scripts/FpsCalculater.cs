using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FpsCalculater : MonoBehaviour
{
    float fps;
    float time;
    float maxTime = 0.5f;
    void Update()
    {
        time += Time.deltaTime;
        fps = 1 / Time.deltaTime;

        if(time > maxTime)
        {
            GetComponent<TextMeshProUGUI>().text = string.Format("FPS : {0}",((int)fps));
            time = 0;
        }
    }
}
