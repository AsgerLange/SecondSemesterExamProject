using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class Melee : Enemy
    {
        public Melee(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, float attackRange, EnemyType enemyType, Alignment alignment)
            : base(gameObject, health, damage, movementSpeed, attackRate, attackRange, enemyType, alignment)
        {
        }

        /// <summary>
        /// when something is inside the enemy
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollisionStay(Collider other)
        {
            base.OnCollisionStay(other);
        }

        /// <summary>
        /// Interaction on collision
        /// </summary>
        /// <param name="other"></param>
        protected override void InteractionOnCollision(Collider other)
        {

            Collider thisCollider = (Collider)GameObject.GetComponent("Collider");

            if (other.GetAlignment != thisCollider.GetAlignment)
            {
                if (other.GetAlignment != Alignment.Neutral)
                {
                    CheckIfCanAttack(other);
                }
            }
        }
        /// <summary>
        /// The standard overwritable attack method for all enemies
        /// </summary>
        /// <param name="other"></param>
        protected virtual void CheckIfCanAttack(Collider other)
        {
            {//can enemy attack yet?
                if ((attackTimeStamp + attackRate) <= GameWorld.Instance.TotalGameTime)
                {
                    foreach (Component component in other.GameObject.GetComponentList)

                    {//does other object contain a vehicle?
                        if ((component is Vehicle && (component as Vehicle).Health > 0))
                        {
                            AttackVehicle(component as Vehicle);

                            break;
                        }

                        if ((component is Tower && (component as Tower).Health > 0))
                        {
                            AttackTower(component as Tower);
                            break;
                        }

                        if ((component is Enemy && (component as Enemy).Health > 0))
                        {
                            AttackEnemy(component as Enemy);

                            break;
                        }
                    }
                }

            }

        }
        protected override void FollowHQ()
        {
            if (playerSpawned)
            {
                targetGameObject = vehicleWhoSpawnedIt.GameObject;
            }
            else
            {
                base.FollowHQ();
            }

        }
        /// <summary>
        /// Handles attack interaction between enemy and vehicle
        /// </summary>
        /// <param name="tower">Targeted Tower component</param>
        protected virtual void AttackTower(Tower tower)
        {

            if (playerSpawned)
            {
                tower.Health -= damage*3;  //damage Tower

            }
            else
            {
            tower.Health -= damage;  //damage Tower

            }

            if (attackVariation > 2)//Adds animation variation
            {
                attackVariation = 1;
            }
            if (isAlive)
            {
                animator.PlayAnimation("Attack" + attackVariation);
            }

            attackVariation++;
            attackTimeStamp = GameWorld.Instance.TotalGameTime;

        }
        /// <summary>
        /// Handles attack interaction between Enemy and Vehicle
        /// </summary>
        /// <param name="vehicle">Targeted vehicle component</param>
        protected virtual void AttackVehicle(Vehicle vehicle)
        {
            if(playerSpawned)
            {
                vehicle.Health -= damage * 3; // damage vehicle

            }
            else
            {

                vehicle.Health -= damage; // damage vehicle
            }

            if (attackVariation > 2)//Adds animation variation
            {
                attackVariation = 1;
            }
            if (isAlive)
            {
                animator.PlayAnimation("Attack" + attackVariation);
            }
            attackVariation++;

            attackTimeStamp = GameWorld.Instance.TotalGameTime; //determines the next time an enemy can attack
        }
        protected virtual void AttackEnemy(Enemy enemy)
        {
            if (playerSpawned)
            {
                enemy.Health -= damage*3; // damage vehicle

            }
            else
            {

                enemy.Health -= damage; // damage vehicle
            }

            if (attackVariation > 2)//Adds animation variation
            {
                attackVariation = 1;
            }
            if (isAlive)
            {
                animator.PlayAnimation("Attack" + attackVariation);
            }
            attackVariation++;

            attackTimeStamp = GameWorld.Instance.TotalGameTime; //determines the next time an enemy can attack
        }
        /// <summary>
        /// Creates the animations
        /// </summary>
        protected override void CreateAnimation()
        {
            base.CreateAnimation();
        }

        /// <summary>
        /// loads the enemy
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        /// <summary>
        /// Override for Enemy.AI()
        /// </summary>
        public override void AI()
        {
            base.AI();
        }
        /// <summary>
        /// handles which animation should the tank be running
        /// </summary>
        /// <param name="animationName"></param>
        public override void OnAnimationDone(string animationName)
        {
            base.OnAnimationDone(animationName);
        }

        /// <summary>
        /// handles what the enemy does
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        /// <summary>
        /// handles what happens when the basicEnemy dies
        /// </summary>
        protected override void Die()
        {
            base.Die();
        }
    }
}
