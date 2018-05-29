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
        public Melee(GameObject gameObject, int health, int damage, float movementSpeed, float attackRate, EnemyType enemyType) : base(gameObject, health, damage, movementSpeed, attackRate, enemyType)
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
        
        protected override void InteractionOnCollision(Collider other)
        {
            if (other.GetAlignment != Alignment.Neutral)
            {
                if (other.GetAlignment == Alignment.Friendly)
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

                    }
                }

            }

        }
        /// <summary>
        /// Handles attack interaction between enemy and vehicle
        /// </summary>
        /// <param name="tower">Targeted Tower component</param>
        protected virtual void AttackTower(Tower tower)
        {
            tower.Health -= damage;  //damage Tower

            if (attackVariation > 2)//Adds animation variation
            {
                attackVariation = 1;
            }
            animator.PlayAnimation("Attack" + attackVariation);
            attackVariation++;
            attackTimeStamp = GameWorld.Instance.TotalGameTime;

        }
        /// <summary>
        /// Handles attack interaction between Enemy and Vehicle
        /// </summary>
        /// <param name="vehicle">Targeted vehicle component</param>
        protected virtual void AttackVehicle(Vehicle vehicle)
        {
            vehicle.Health -= damage; // damage vehicle

            if (attackVariation > 2)//Adds animation variation
            {
                attackVariation = 1;
            }

            animator.PlayAnimation("Attack" + attackVariation);
            attackVariation++;

            attackTimeStamp = GameWorld.Instance.TotalGameTime; //determines the next time an enemy can attack
        }
        /// <summary>
        /// Creates the animations
        /// </summary>
        public override void CreateAnimation()
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
