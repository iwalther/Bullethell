using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Bullethell
{
    class Player
    {
        //public static Vector2 centerPosition { get; set; }
        Texture2D texture;
        Rectangle rectangle;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Color color;
        float rotation;
        float speed;
        float attackSpeed;
        float attackTimer;
        float health;
        bool alive = true;

        public Player(Texture2D playerTexture, Vector2 playerStartPos, float playerSpeed, Vector2 playerScale, float playerRotation, Color playerColor, float playerHealth, float playerAttackSpeed)
        {
            texture = playerTexture;
            position = playerStartPos;
            speed = playerSpeed;
            moveDir = Vector2.Zero;
            scale = playerScale;
            offset = playerTexture.Bounds.Size.ToVector2() * 0.5f;
            rectangle = new Rectangle((playerStartPos - offset * scale).ToPoint(), (playerTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            color = playerColor;
            rotation = playerRotation;
            health = playerHealth;
            attackSpeed = playerAttackSpeed;
            attackTimer = 0;
        }

        public void Update(float deltaTime, KeyboardState keyboardState, MouseState mouseState, Point windowSize)
        {
            float pixelsToMove = speed * deltaTime;

            if (alive)
            {
                Vector2 mousePos = mouseState.Position.ToVector2();
                moveDir = mousePos - position;

                if (moveDir != Vector2.Zero)
                {
                    moveDir.Normalize();


                    if (Vector2.Distance(position, mouseState.Position.ToVector2()) < pixelsToMove)
                    {
                        position = mouseState.Position.ToVector2();
                    }
                    else
                    {
                        position += moveDir * pixelsToMove;
                    }
                    rectangle.Location = (position - offset * scale).ToPoint();
                }


                if (position.X <= 0)
                {
                    position.X = 0;
                }
                if (position.X >= 1400)
                {
                    position.X = 1400;
                }
                if (position.Y >= 900)
                {
                    position.Y = 900;
                }
                if (position.Y <= 0)
                {
                    position.Y = 0;
                }

                attackTimer += deltaTime;
                if (attackTimer <= attackSpeed)
                {
                    attackTimer += deltaTime;
                }

                if (attackTimer >= attackSpeed)
                {
                    Vector2 bulletDir = new Vector2(1, 0);

                    BulletManager.AddBullet(TextureLibrary.GetTexture("bullet"), position, bulletDir, 400, new Vector2(0.2f, 0.2f), Bullet.Owner.Player, color, 0);
                    attackTimer = 0;
                }
            }
            else
            {
                color = Color.Black;
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
