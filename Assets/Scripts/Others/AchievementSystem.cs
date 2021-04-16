using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Tanks;
using UnityEngine.UI;
using BattleTank.Bullets;
using BattleTank.Enemies;
using TMPro;

public class AchievementSystem : MonoBehaviour
{

    private int tanksDestoryed = 0;
    private int BulletsFired = 0;
    private TextMeshProUGUI AchievementText;

    private TankView tankview;
    
    void Start()
    {
        EnemyService.Instance.onDeathEvent += AchSys_OnDeathEvent;
        TankService.Instance.onBulletFire += AchSys_onBulletFire;
        AchievementText = GetComponent<TextMeshProUGUI>();
        
    }

    private void AchSys_onBulletFire()
    {
        BulletsFired += 1;
        if (BulletsFired == 100)
        {
            StartCoroutine(BulletAchieve(1));
           //AchievementText.text = "Acheivement Unlocked: " + BulletsFired + " Bullets fired!!!";
        }
    }

    private void AchSys_OnDeathEvent()
    {
        tanksDestoryed += 1;
        if(tanksDestoryed == 4)
        {
           // Debug.Log("Acheivement Unlocked: Number of tanks destoryed are " + tanksDestoryed);
            AchievementText.text = "Acheivement Unlocked: " + tanksDestoryed + " Tanks Destroyed!!!";
        }
    }

    public void followPlayerAchievement()
    {
        this.tankview = TankService.Instance.tankController.TankView;

    }

    IEnumerator BulletAchieve(float seconds)
    {
        AchievementText.text = "Acheivement Unlocked: " + BulletsFired + " Bullets fired!!!";
        yield return new WaitForSeconds(seconds);
         StopCoroutine(BulletAchieve(1));
       
    }
}
