using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using BattleTank.Bullets;
using BattleTank.Tanks;


namespace BattleTank.Tanks
{
    public class TankController
    {   
        private int Full_Health;

        public TankController(TankModel tankModel, TankView tankPrefab)
        {
            this.TankModel = tankModel;
            this.TankView = GameObject.Instantiate<TankView>(tankPrefab);
            TankView.initialize(this);
            this.Full_Health=TankModel.Health;
        }

        public TankModel TankModel { get; }
        public TankView TankView { get; }
        public BulletController bulletController;
        public float TurnSpeed=180f;


        public TankModel getModel()
        {
            return TankModel;
        }


        //Tank Fire Logic.
        public void tankFire()
        {
            Debug.Log("Tank Fired a bullet ");
            TankView.Fired=true;
            //BulletService.Instance.CreateBullet((int)TankModel.BulletType,"Player",TankView);
            bulletController = BulletService.Instance.CreateBullet((int)TankModel.BulletType,"Player");
            bulletController.SetPos(TankView.Fire_Transform,TankView.Current_Launch_Force);
            TankView.Current_Launch_Force=TankView.Min_Launch_Force;

            TankService.Instance.fireEvent();
        }

        //Movement of tank.
        public void Move(float Movement_Input_Value)
        {
           TankView.transform.Translate(0f,0f,Movement_Input_Value * TankModel.Speed * Time.deltaTime);
        }
        
        //Tank Rotation
        public void Turn(float Turn_Input_Value)
        {
          TankView.transform.Rotate(0f,Turn_Input_Value *10* TankModel.Speed * Time.deltaTime,0f);
        }

        //Damage taken by tank
         public void ApplyDamage(int damage)
        {
            if (TankModel.Health - damage <= 0)
            {
                Debug.Log("Player tank has been destoryed.");
                OnDeath();
            }
            else
            {
                TankModel.Health -= damage;
                SetHealthUI();
                Debug.Log("Player took damage " + TankModel.Health);
            }
        }

        //Slider UI initialisation
        public void SetHealthUI()
        {
            TankView.health_slider.value=TankModel.Health;
            TankView.fill_Image.color=Color.Lerp(TankView.Zero_HealthColor,TankView.Full_HealthColor,TankModel.Health/Full_Health);
        }

        //Player is dead
        public void OnDeath()
        {
            TankService.Instance.DestroyTank(this);
            TankView.m_Dead=true;
            TankView.ExplosionParticles.transform.position=TankView.transform.position;
            TankView.ExplosionParticles.gameObject.SetActive(true);
            TankView.ExplosionParticles.Play();

        }

        public void DestroyStuff()
        {
            TankModel.destroyModel(TankModel);
            TankView.DestroyView(this.TankView);
        }

    }
}
