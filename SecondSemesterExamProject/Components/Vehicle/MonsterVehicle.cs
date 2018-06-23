﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class MonsterVehicle : Vehicle
    {
        private float healTimeStamp;
        public int swarmerCount = 0;
        /// <summary>
        /// Creates the tank
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="control"></param>
        /// <param name="health"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="fireRate"></param>
        public MonsterVehicle(GameObject gameObject, Controls control, int health, float movementSpeed, float rotateSpeed, int money,
             TowerType tower, int playerNumber) : base(gameObject, control, health, movementSpeed, rotateSpeed, money, tower, playerNumber)
        {
            this.vehicleType = VehicleType.MonsterVehicle;
        }

        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
        {

            animator.CreateAnimation("Idle", new Animation(4, 51, 0, 31, 51, 6, Vector2.Zero));
            animator.CreateAnimation("MoveForward", new Animation(4, 102, 0, 31, 51, 6, Vector2.Zero));
            animator.CreateAnimation("MoveBackward", new Animation(4, 102, 0, 31, 51, 6, Vector2.Zero));
            animator.CreateAnimation("Shoot", new Animation(5, 153, 0, 31, 51, 12, Vector2.Zero));
            animator.CreateAnimation("ShootMachineGun", new Animation(5, 153, 0, 31, 51, 13, Vector2.Zero));
            animator.CreateAnimation("MoveShootForward", new Animation(5, 153, 0, 31, 51, 12, Vector2.Zero));
            animator.CreateAnimation("MoveShootBackward", new Animation(5, 153, 0, 31, 51, 12, Vector2.Zero));
            animator.CreateAnimation("Death", new Animation(7, 204, 0, 31, 51, 6, Vector2.Zero));



        }

        /// <summary>
        /// loads the tank
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            if (GameWorld.Instance.pvp)
            {
                this.weapon = new MonsterWeapon(this.GameObject);
            }
            else
            {

                this.weapon = new MonsterWeapon(this.GameObject);
            }
            vehicleDeathSound = content.Load<SoundEffect>("EnemyDeath");

        }

        private void RestoreHealth()
        {
            if (healTimeStamp + Constant.monsterRegenRate <= GameWorld.Instance.TotalGameTime)
            {
                Health += 1;

                healTimeStamp = GameWorld.Instance.TotalGameTime;
            }
        }
        /// <summary>
        /// handles which animation should the tank be running
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        protected override void BuildTower()
        {
            KeyboardState keyState = Keyboard.GetState();

            if (((keyState.IsKeyDown(Keys.LeftAlt) || (keyState.IsKeyDown(Keys.G))) && control == Controls.WASD)
                 || ((keyState.IsKeyDown(Keys.OemPeriod) || (keyState.IsKeyDown(Keys.Back))) && control == Controls.UDLR))
            {
                if (builtTimeStamp + Constant.buildTowerCoolDown <= GameWorld.Instance.TotalGameTime)
                {
                    if (Money >= 10 && EnemyPool.Instance.ActiveEnemies.Count < Constant.maxEnemyOnScreen)
                    {
                        if (swarmerCount < Constant.swarmerMaxAmount)
                        {
                            SpawnSwarmer();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Spawns a swarmer 
        /// </summary>
        private void SpawnSwarmer()
        {

            GameObject tmp = EnemyPool.Instance.CreateEnemy(new Vector2(GameObject.Transform.Position.X, GameObject.Transform.Position.Y + 10), EnemyType.Swarmer, alignment);
            swarmerCount++;
            foreach (Component comp in tmp.GetComponentList)
            {
                if (comp is Enemy && comp is SwarmerEnemy)
                {

                    (comp as Enemy).playerSpawned = true;
                    (comp as Enemy).AttackRange = Constant.swarmerEnemyAttackRadius;
                    (comp as SwarmerEnemy).vehicleWhoSpawnedIt = this;



                }
                if (comp is SpriteRenderer)
                {
                    (comp as SpriteRenderer).Sprite = GameWorld.Instance.Content.Load<Texture2D>("SwarmerBlank");

                    if (control == Controls.WASD)
                    {
                        (comp as SpriteRenderer).color = Color.Cyan;

                    }
                    else if (control == Controls.UDLR)
                    {
                        (comp as SpriteRenderer).color = Color.Lime;

                    }
                }

            }

            Money -= 10;
            builtTimeStamp = GameWorld.Instance.TotalGameTime;
        }
        /// <summary>
        /// handles what the tank does
        /// </summary>
        public override void Update()
        {
            RestoreHealth();
            base.Update();
        }

        /// <summary>
        /// handles what happens when the tank dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
        public override void GetBasicGun()
        {
            this.weapon = new MonsterWeapon(this.GameObject);

        }
        protected override Vector2 Move(Vector2 translation)
        {

            KeyboardState keyState = Keyboard.GetState();

            if ((keyState.IsKeyDown(Keys.W) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Up) && control == Controls.UDLR))
            {
                translation += new Vector2(0, -1);
                if (isPlayingAnimation == false)
                {
                    animator.PlayAnimation("MoveForward");
                }
            }
            else if ((keyState.IsKeyDown(Keys.S) && control == Controls.WASD)
                || (keyState.IsKeyDown(Keys.Down) && control == Controls.UDLR))
            {
                translation += new Vector2(0, 0.5f);
                if (isPlayingAnimation == false)
                {
                    animator.PlayAnimation("MoveBackward");
                }
            }
            return translation;
        }
        protected override void PlayDeathSound()
        {
            vehicleDeathSound.Play(1f, -1f, 0);
        }
    }
}