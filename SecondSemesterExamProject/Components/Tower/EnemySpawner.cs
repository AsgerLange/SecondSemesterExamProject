using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class EnemySpawner
    {
        private int enemyTypeInt;

        private EnemyType enemyType;

        private int enemyBuildCost;

        private MonsterVehicle vehicle;

        private float builtTimeStamp;
        private float toggleTimeStamp;

        public EnemyType GetEnemyType
        {
            get { return enemyType; }
        }

        public int EnemyBuildCost
        {
            get { return enemyBuildCost; }
        }
        public float BuiltTimeStamp
        {
            get { return builtTimeStamp; }
        }



        public EnemySpawner(MonsterVehicle vehicle)
        {
            this.vehicle = vehicle;
            this.enemyType = 0;
            SetSpawnCost();

        }

        public void SpawnEnemy()
        {
            if (vehicle.Money >= EnemyBuildCost)
            {


                GameObject tmp;
                if (vehicle.Control == Controls.WASD)
                {
                    tmp = EnemyPool.Instance.CreateEnemy(new Vector2(vehicle.GameObject.Transform.Position.X, vehicle.GameObject.Transform.Position.Y + 15),
                        enemyType, vehicle.alignment);

                }
                else
                {

                    tmp = EnemyPool.Instance.CreateEnemy(new Vector2(vehicle.GameObject.Transform.Position.X, vehicle.GameObject.Transform.Position.Y + 15),
                       enemyType, vehicle.alignment);


                }
                vehicle.EnemyCount++;

                SetupEnemy(tmp);

                vehicle.Money -= EnemyBuildCost;
                builtTimeStamp = GameWorld.Instance.TotalGameTime;
            }
        }

        private void SetupEnemy(GameObject tmp)
        {
            bool spitter = false;

            foreach (Component comp in tmp.GetComponentList)
            {
                if (comp is Enemy)
                {
                    (comp as Enemy).VehicleWhoSpawnedIt = vehicle;
                    (comp as Enemy).playerSpawned = true;
                    (comp as Enemy).AttackRange = (comp as Enemy).AttackRange * 1.5f;

                    if (comp is SiegebreakerEnemy || comp is BasicEliteEnemy)
                    {
                        (comp as Enemy).Health = (comp as Enemy).Health / 3;

                    }
                    else
                    {
                        (comp as Enemy).Health = (comp as Enemy).Health / 2;
                    }

                    if (comp is Spitter)
                    {
                        spitter = true;
                    }
                    break;

                }


            }
            //(comp as SpriteRenderer).Sprite = GameWorld.Instance.Content.Load<Texture2D>("SwarmerBlank");

            if (vehicle.Control == Controls.WASD && spitter == false)
            {

                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.Cyan;

            }
            else if (vehicle.Control == Controls.WASD && spitter)
            {


                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).Sprite = GameWorld.Instance.Content.Load<Texture2D>("SpitterBlue");

            }
            else if (vehicle.Control == Controls.UDLR && spitter == false)
            {
                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).color = Color.PaleGreen;

            }
            else if (vehicle.Control == Controls.UDLR && spitter)
            {

                ((SpriteRenderer)tmp.GetComponent("SpriteRenderer")).Sprite = GameWorld.Instance.Content.Load<Texture2D>("SpitterGreen");

            }

        }
        private void SetSpawnCost()
        {
            switch (enemyType)
            {
                case EnemyType.BasicEnemy:
                    enemyBuildCost = Constant.basicEnemyCost;
                    break;
                case EnemyType.BasicEliteEnemy:
                    enemyBuildCost = Constant.basicEliteCost;
                    break;
                case EnemyType.Swarmer:
                    enemyBuildCost = Constant.swarmerCost;
                    break;
                case EnemyType.Spitter:
                    enemyBuildCost = Constant.spitterCost;
                    break;
                case EnemyType.SiegebreakerEnemy:
                    enemyBuildCost = Constant.siegeBreakerCost;
                    break;
                default:
                    enemyBuildCost = Constant.basicEnemyCost;
                    Console.WriteLine("error setspawn cost enemybuilder");
                    break;
            }
        }

        public void ToggleEnemy()
        {

            if (GameWorld.Instance.TotalGameTime > toggleTimeStamp + Constant.buildTowerCoolDown)
            {

                enemyTypeInt++;
                int maxLenght = Enum.GetNames(typeof(EnemyType)).Length;


                if (enemyTypeInt >= maxLenght)
                {
                    enemyTypeInt = 0;
                }
                enemyType = (EnemyType)enemyTypeInt;

                toggleTimeStamp = GameWorld.Instance.TotalGameTime;

                SetSpawnCost();
            }
        }

        public override string ToString()
        {
            return enemyType.ToString() + ":  $" + enemyBuildCost + "     " + vehicle.EnemyCount + " / " + Constant.spawnedEnemyMaxAmount;
        }
    }
}
