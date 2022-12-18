using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.EventSystems;

public class ButtonControl : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button quitButton;

    [Header("Highlighted Sprites")]
    [SerializeField] Sprite playButtonHighlighted;
    [SerializeField] Sprite optionButtonHighlighted;
    [SerializeField] Sprite quitButtonHighlighted;

    [Header("Default Sprites")]
    [SerializeField] Sprite playButtonDefault;
    [SerializeField] Sprite optionButtonDefault;
    [SerializeField] Sprite quitButtonDefault;

    bool playButtonMode = false;
    bool optionButtonMode = false;
    bool quitButtonMode = false;

    public void OnMouseOver()
    {
        playButton.GetComponent<Image>().sprite = playButtonHighlighted;
    }

    public void OnMouseExit()
    {
        playButton.GetComponent<Image>().sprite = playButtonDefault;
    }
}
