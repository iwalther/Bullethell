using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bullethell
{
    class Enemy
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Color color;
        Vector2 scale;
        Vector2 offset;
        float rotation;
        float speed;
        float health;
        bool alive;
        float attackSpeed;
        float attackTimer;
        float attackRange;


        public Enemy(Texture2D enemyTexture, Vector2 enemyStartPos, float enemySpeed, Vector2 enemyScale, float enemyRotation, Color enemyColor, float enemyHealth, float enemyAttackRange, float enemyAttackSpeed)
        {
            texture = enemyTexture;
            position = enemyStartPos;
            speed = enemySpeed;
            moveDir = Vector2.Zero;
            scale = enemyScale * 0.5f;
            offset = enemyTexture.Bounds.Size.ToVector2() * 0.5f;
            rectangle = new Rectangle((enemyStartPos - offset * scale).ToPoint(), (enemyTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            color = enemyColor;
            rotation = enemyRotation;
            health = enemyHealth;
            attackRange = enemyAttackRange;
            attackSpeed = enemyAttackSpeed;
            attackTimer = 0;

            alive = true;

            //bullets = new List<Bullet>();
        }



        public void Update(float deltaTime, int windowHeight, Player player)
        {
            if (alive)
            {
                //Enemy Movement

                attackTimer += deltaTime;

                if (attackTimer <= attackSpeed)
                {
                    attackTimer += deltaTime;
                }

                if (Vector2.Distance(position, player.GetPosition()) <= attackRange && attackTimer >= attackSpeed)
                {
                    BulletManager.AddBullet(TextureLibrary.GetTexture("bullet"), position, player.GetPosition() - position, 400, new Vector2(0.2f, 0.2f), Bullet.Owner.Enemy, color);
                    attackTimer = 0;
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, rotation, offset, scale, SpriteEffects.None, 0);
        }

        public void ChangeHealth(float healthMod)
        {
            health += healthMod;

            if (health <= 0)
            {
                alive = false;
            }
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public bool GetIsAlive()
        {
            return alive;
        }

    }
}
