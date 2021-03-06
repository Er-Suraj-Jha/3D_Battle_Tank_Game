using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BattleTank.Tanks;

namespace BattleTank.Enemies
{
    public class EnemyService : MonoSingletonGeneric<EnemyService>
    {
        public event Action onDeathEvent;
        public event Action onDamageTaken;

        private EnemyModel enemyModel;
        public EnemyView enemyView;
        public EnemyController enemyController;
        public List<EnemyController> enemyList = new List<EnemyController>();
        public EnemyTankScriptableObject enemyTankScriptableObject;


        private void Start()
        {
            enemyTankScriptableObject = ScriptableObject.CreateInstance<EnemyTankScriptableObject>();
            Debug.Log(enemyList.Count + " Enemy service script");
        }

 

        public EnemyController CreateEnemyTank()
        {
            enemyModel = new EnemyModel(enemyTankScriptableObject);

            enemyController = new EnemyController(enemyModel, enemyView);

            enemyList.Add(enemyController);

            Debug.Log(enemyList.Count + " Updated enemy count!!!");
            return enemyController;

        }

        public void DestroyEnemyTank(EnemyController enemyController)
        {
            onDeathEvent?.Invoke();

            enemyController.DestroyStuff();
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] == enemyController)
                {
                    enemyList.Remove(enemyController);
                }
            }
            enemyController = null;
        }

        public void onDamageEvent()
        {
            onDamageTaken?.Invoke();
        }
    }

}
