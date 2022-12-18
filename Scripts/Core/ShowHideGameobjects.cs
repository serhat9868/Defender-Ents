using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideGameobjects : MonoBehaviour
{
    [SerializeField] GameObject[] firstOpened;
    [SerializeField] GameObject[] firstClosed;
    void Start()
    {
        foreach (var gameobject in firstOpened)
        {
            gameobject.SetActive(true);
        }
        foreach (var gameobject in firstClosed)
        {
            gameobject.SetActive(false);
        }
    }

    public void StartGame()
    {
        foreach (var instanceClosed in firstClosed)
        {
            instanceClosed.SetActive(true);
        }

        foreach (var instanceOpened in firstOpened)
        {
            instanceOpened.SetActive(false);
        }
    }
}
