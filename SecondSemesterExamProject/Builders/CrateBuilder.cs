using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class CrateBuilder
    {
        private GameObject go;

        private CrateType type;

        /// <summary>
        /// Builds the crate
        /// </summary>
        public void Build()
        {
            int random = GameWorld.Instance.Rnd.Next(0,7);

            switch (random)
            {
                case 1:
                    type = CrateType.TowerCrate;
                    break;
                case 2:
                    type = CrateType.WeaponCrate;

                    break;
                case 3:
                    type = CrateType.WeaponCrate;

                    break;
                case 4:
                    type = CrateType.MoneyCrate;

                    break;
                case 5:
                    type = CrateType.MoneyCrate;
                    break;
                case 6:
                    type = CrateType.HealthCrate;
                    break;

                default:
                    type = CrateType.HealthCrate;

                    break;
            }
            go = new GameObject();

            go.Transform.Position = new Vector2(GameWorld.Instance.Rnd.Next(100, Constant.width - 100),
                GameWorld.Instance.Rnd.Next(100, Constant.hight - 100));

            switch (type)
            {
                case CrateType.WeaponCrate:
                    go.AddComponent(new SpriteRenderer(go, Constant.GunCrateSpriteSheet, 0.2f));
                    go.AddComponent(new WeaponCrate(go));
                    break;
                case CrateType.TowerCrate:
                    go.AddComponent(new SpriteRenderer(go, Constant.TowerCrateSpriteSheet, 0.2f));
                    go.AddComponent(new TowerCrate(go));
                    break;

                case CrateType.MoneyCrate:
                    go.AddComponent(new SpriteRenderer(go, Constant.MoneyCrateSpriteSheet, 0.2f));
                    go.AddComponent(new MoneyCrate(go));

                    break;
                case CrateType.HealthCrate:
                    go.AddComponent(new SpriteRenderer(go, Constant.HealthCrateSpriteSheet, 0.2f));
                    go.AddComponent(new HealthCrate(go));
                    break;
                default:
                    break;
            }


            go.AddComponent(new Collider(go, Alignment.Neutral));
            go.AddComponent(new Animator(go));

        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }
}

