using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;
using BattleTank.Tanks;

public class AttackState : EnemyState
{
    
    private float speed = 5f;
    private Transform playerPos;

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Debug.Log("Tank has started attacking!");
        enemyView.transform.LookAt(playerPos.position);
        StartCoroutine(tankFireRate1(2f));
        
    }

    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log("Tank has stopped attacking!");
        StopCoroutine(StartCoroutine(tankFireRate1(2f)));
    }

    private void FixedUpdate()
    {
        // enemyView.transform.LookAt(playerPos.position);
         enemyView.transform.Translate(0, 0, speed * Time.deltaTime);
         

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankView>() != null)
        {
            GameObject player = other.gameObject;
            Transform goal = player.transform;
            this.playerPos = goal;

            enemyView.ChangeState(enemyView.attackState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankView>() != null)
        {
            enemyView.ChangeState(enemyView.patrollingState);
            StopAllCoroutines();
        }
    }


    public IEnumerator tankFireRate1(float seconds)
    {
        enemyView.enemyController.enemyTankFire();
        yield return new WaitForSeconds(seconds);
        yield return  tankFireRate1(2f);
    }
}