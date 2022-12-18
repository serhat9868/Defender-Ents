using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterButtons : MonoBehaviour
{
    [SerializeField] GameObject abilityPanel;
    [SerializeField] GameObject characterPanel;
    public void ShowAbilityPanel()
    {
        abilityPanel.SetActive(true);
        characterPanel.SetActive(false);
    }

    public void ShowCharacterPanel()
    {
        abilityPanel.SetActive(false);
        characterPanel.SetActive(true);
    }
}
