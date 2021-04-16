using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Bullets;
using BattleTank.Tanks;

namespace BattleTank.Enemies
{
    public class EnemyController
    {
        private float Full_Health;

        public EnemyController(EnemyModel enemyModel, EnemyView EnemyPrefab)
        {
            this.EnemyModel = enemyModel;
            this.EnemyView = GameObject.Instantiate<EnemyView>(EnemyPrefab);
            EnemyView.initialize(this);
            this.Full_Health=EnemyModel.Health;
        }

        public EnemyModel EnemyModel { get; }
        public EnemyView EnemyView { get; }
        public float rayDistance = 100f;
        public BulletController bulletController;

        public float TurnSpeed=180f;
  
        public EnemyModel getModel()
        {
            return EnemyModel;
        }

        //Enemy movement
        public void EnemyZMovement()
        {
            EnemyView.transform.Translate(new Vector3(0f, 0f, 1f) * EnemyModel.Speed * Time.deltaTime);
        }

        //Set position of enemy.
        public void setPositionEnemy(Vector3 position, Quaternion quaternion)
        {
            EnemyView.transform.position = position;
            EnemyView.transform.rotation = quaternion;
        }


        //Damage
        public void ApplyDamage(int damage)
        {
            if (EnemyModel.Health - damage <= 0)
            {
                //Destroy function being called from service.
                OnDeath();
                EnemyService.Instance.DestroyEnemyTank(this);

            }
            else
            {
                EnemyModel.Health -= damage;
                SetHealthUI();
                Debug.Log("Enemy took damage " + EnemyModel.Health);
            }
        }



        //Destroy EnemyTank stuff.
        public void DestroyStuff()
        {
            EnemyModel.modelDestroy(getModel());
            EnemyView.enemyDestroyView(this.EnemyView);
        }

        //Particle system to be played on tank's death
        private void OnDeath()
        {
            EnemyView.m_Dead=true;
            EnemyView.ExplosionParticles.transform.position=EnemyView.transform.position;
            EnemyView.ExplosionParticles.gameObject.SetActive(true);
            EnemyView.ExplosionParticles.Play();
        }

        //Setting HealthUI of the tank
        public void SetHealthUI()
        {
            EnemyView.health_slider.value=EnemyModel.Health;
            EnemyView.fill_Image.color=Color.Lerp(EnemyView.Zero_HealthColor,EnemyView.Full_HealthColor,EnemyModel.Health/Full_Health);
        }



        //Firing bullets by enemy.
        public void enemyTankFire()
        {
            Debug.Log("Tank Fired a bullet ");
            EnemyView.Fired=true;
            bulletController = BulletService.Instance.CreateBullet(1,"Enemy");
            bulletController.SetPos(EnemyView.Fire_Transform,15);
            //TankService.Instance.fireEvent();
        }
    }

}
