using RPG.Stats;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayExperiencePoints : MonoBehaviour
{
    float experiencePoints = 0;
    GameObject mainPlayer;
    private void Awake()
    {
        mainPlayer = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        GetComponent<TextMeshProUGUI>().text =
            String.Format("Experience : {0:0}",mainPlayer.GetComponent<Experience>().GetPoints());
    }
}
