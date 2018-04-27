using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Bullethell
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
            scale = new Vector2(0.25f, 0.25f);

        }

        public void Update(float deltaTime)
        {
            position += dir * speed * deltaTime;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(texture, position, null, Color.White, 0, offset, scale, SpriteEffects.None, 1);
        }

        public void DestroyBullet(List<Bullet> bullets)
        {
            if( position.X > 800)
            {
                bullets.Remove(this);
                Debug.Print("Bullet destroyed");
            }
        }
    }
}
