using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Bullets;
using BattleTank.Enemies;


namespace BattleTank.Tanks
{
     public class TankService : MonoSingletonGeneric<TankService>
    {
        public event Action onBulletFire;
        public event Action onDamageTaken;
        public event Action onDeathofPlayer;

        public TankController tankController;
        private TankModel tankModel;
        public TankView tankView;
        private TankScriptableObjects tankScriptableObject;
        public TankScriptableObjectList tankScriptableObjectList;
        public Enter_Again enter_Again;
        
        private void Start()
        {
            tankScriptableObject = ScriptableObject.CreateInstance<TankScriptableObjects>();
            CreateNewTank();
        }

        public TankController CreateNewTank()
        {
            tankScriptableObject = tankScriptableObjectList.tanks[0];
            tankModel = new TankModel(tankScriptableObject);

            tankController = new TankController(tankModel, tankView);
            return tankController;
        }

         public void damageEvenet()
        {
            onDamageTaken?.Invoke();
        }

        public void fireEvent()
        {
            onBulletFire?.Invoke();
        }

         public void DestroyTank(TankController tankController)
        {
            onDeathofPlayer?.Invoke();
            Debug.Log("onDeathofPlayer has been set true");
            enter_Again.PlayerDied();
            tankController.DestroyStuff();
            tankController = null;
        }
    }
}

