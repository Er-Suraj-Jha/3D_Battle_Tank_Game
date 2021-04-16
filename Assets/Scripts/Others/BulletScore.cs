using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Tanks;
using UnityEngine.UI;
using BattleTank.Bullets;
using BattleTank.Enemies;
using TMPro;

public class BulletScore : MonoBehaviour
{
    private TextMeshProUGUI bulletsFiredText;

    public TankView tankView;
     
    private int bullets = 0;

    void Start()
    {
         TankService.Instance.onBulletFire += BulletScore_OnBulletFire;
         bulletsFiredText = GetComponent<TextMeshProUGUI>();
    }


    private void BulletScore_OnBulletFire()
    {
        //if playertank = myid of player only then add this

        bullets += 1;
        bulletsFiredText.text = "Bullets Fired: " + bullets;

    }

    private void OnDestroy()
    {
        TankService.Instance.onBulletFire -= BulletScore_OnBulletFire;
    }

    public void followPlayerBullet()
    {
        this.tankView = TankService.Instance.tankController.TankView;

    }
}