using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    [SerializeField] GameObject configShowing;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartToFight);
    }

    private void StartToFight()
    {
       configShowing.GetComponent<ShowHideGameobjects>().StartGame();
    }

    private void LoadGame()
    {
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneBuildIndex);
    }
}
