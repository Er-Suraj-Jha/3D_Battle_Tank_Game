using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Enemies;
using BattleTank.Tanks;

public class IdleState : EnemyState
{
    private float speed=5f;
    private Transform playerPos;
   
    
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Debug.Log("Tank has entered idle state");
         enemyView.transform.LookAt(playerPos.position);
         StartCoroutine(ChangeDirection(5f));
    }

    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log("Tank has exited idle state");
    }

    private void Update()
    {
       // enemyView.transform.LookAt(playerPos.position);
        enemyView.transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.GetComponent<TankView>() != null)
        {
            GameObject player = other.gameObject;
            Transform goal = player.transform;
            this.playerPos = goal;
            enemyView.ChangeState(enemyView.idleState);
        }
    }

    private IEnumerator ChangeDirection(float seconds)
    {
        enemyView.ChangeDirection();
        yield return new WaitForSeconds(seconds);
        yield return ChangeDirection(7f);
    } 
 

}