using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;

namespace BattleTank
{
public class SpawnService : MonoSingletonGeneric<SpawnService>
{

    private EnemyService enemyService;
    private Coroutine spawnEnumerator;
    private int i=0;

    private void Start()
    {
        enemyService = EnemyService.Instance;
        if (spawnEnumerator != null)
        {
            StopCoroutine(spawnEnumerator);
        }

       spawnEnumerator= StartCoroutine(spawnEnemy(2));
  }

    private void Update()
    {
        //Random spawning
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Debug.Log("Spawn when 0 is pressed");
            enemyService.CreateEnemyTank().setPositionEnemy(new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 30f)), Quaternion.identity);
        }
    }


    private IEnumerator spawnEnemy(float seconds)
    {
        i++;
        if(i>5)
        {
            StopAllCoroutines();
        }
        enemyService.CreateEnemyTank().setPositionEnemy(new Vector3(Random.Range(-40f, 40f), 0f, Random.Range(-40f, 30f)), Quaternion.identity);

        yield return new WaitForSeconds(seconds);
        yield return spawnEnemy(2);
    }


}
}