using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;
using BattleTank.Tanks;
using UnityEngine.UI;
using BattleTank.Bullets;
using TMPro;

public class Score : MonoBehaviour
{

    private TextMeshProUGUI tankDestroyText;
    public Enter_Again enter_Again;
    private int score = 0;
    

    void Start()
    {

        EnemyService.Instance.onDeathEvent += Score_OnDeathEvent;
    
        tankDestroyText = GetComponent<TextMeshProUGUI>();
       
    }

    void Update()
    {
        if(score==6)
        {
           enter_Again.PlayerDied();
        }
    }

    private void Score_OnDeathEvent()
    {
        score += 1;
        tankDestroyText.text = "Enemies destroyed: " + score;
    }

    private void OnDestroy()
    {
        EnemyService.Instance.onDeathEvent -= Score_OnDeathEvent;
    }


}