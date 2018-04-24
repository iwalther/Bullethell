using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Bullet
{
    class Bullet
    {
        Vector2 dir;
        float speed;
        Texture2D texture;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;

        public Bullet(Vector2 bulletDir, float bulletSpeed, Texture2D bulletTexture, Vector2 startPosition)
        {
            dir = bulletDir;
            dir.Normalize();
            speed = bulletSpeed;
            texture = bulletTexture;
            position = startPosition;
            offset = (bulletTexture.Bounds.Size.ToVector2() / 2.0f);
            scale = new Vector2(0.5f, 0.5f);

        }

        public void Update(float deltaTime)
        {
            position += dir * speed * deltaTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Debug.Print("a bullet has spawned");
            spriteBatch.Draw(texture, position, null, Color.White, 0, offset, scale, SpriteEffects.None, 1);
        }
    }
}
