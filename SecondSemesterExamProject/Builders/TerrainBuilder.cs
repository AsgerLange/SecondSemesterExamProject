using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankGame
{
    class TerrainBuilder
    {
        private GameObject go;
        private float terrainNumber = 1;

        public void Build(Vector2 position, int size, int rotation)
        {
            go = new GameObject();
            go.Transform.Position = position;
            go.AddComponent(new SpriteRenderer(go, Constant.rockImage, 0.99f - terrainNumber / 100));
            go.AddComponent(new Terrain(go, size, rotation, Alignment.Neutral));
            go.AddComponent(new Collider(go, Alignment.Neutral));
            terrainNumber++;
        }


        public GameObject GetResult()
        {
            go.LoadContent(GameWorld.Instance.Content);
            return go;
        }
    }
}
