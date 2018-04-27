using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Bullethell
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D rocket;
        Texture2D space1;
        Texture2D space2;
        Rectangle rocketRect;
        Vector2 moveDir;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        float speed;
        float rotation;
        Color color;
        float attackSpeed;
        float attackTimer;
        Background background = new Background();


        List<Bullet> bullets;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
            position = new Vector2(100, 100);
            moveDir = Vector2.Zero;
            speed = 3000;
            rotation = 0;
            scale = new Vector2(0.10f, 0.10f);
            color = Color.White;
            offset = (rocket.Bounds.Size.ToVector2() / 2.0f);
            rocketRect = new Rectangle((position - offset).ToPoint(), (rocket.Bounds.Size.ToVector2() * scale).ToPoint());
            attackSpeed = 0.15f;
            attackTimer = 0;
            background.Initialize(new Vector2(-1, 0), space1, space2, new Vector2(0, 0), 500);

            bullets = new List<Bullet>();

        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            TextureLibrary.LoadTexture(Content, "Rocket");
            TextureLibrary.LoadTexture(Content, "Space1");
            TextureLibrary.LoadTexture(Content, "Space2");
            TextureLibrary.LoadTexture(Content, "bullet");

            rocket = Content.Load<Texture2D>("Rocket");
            space1 = Content.Load<Texture2D>("Space1");
            space2 = Content.Load<Texture2D>("Space2");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = mouseState.Position.ToVector2();
            moveDir = mousePos - position;
            float pixelsToMove = speed * deltaTime;

            attackTimer += deltaTime;
            
            background.Update(deltaTime);

            if (attackTimer >= attackSpeed/*mouseState.LeftButton == ButtonState.Pressed*/)
            {

                attackTimer = 0;
                bullets.Add(new Bullet(new Vector2(1, 0), 1000, TextureLibrary.GetTexture("bullet"), position));
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].Update(deltaTime);
                bullets[i].DestroyBullet(bullets);
            }


            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();
                rocketRect.Location += (moveDir * speed * deltaTime).ToPoint();
            }

            if (moveDir != Vector2.Zero)
            {
                moveDir.Normalize();

                
                if (Vector2.Distance(position, mousePos) < pixelsToMove)
                {
                    position = mousePos;
                }
                else
                {
                    position += moveDir * pixelsToMove;
                }
                rocketRect.Location = (position - offset).ToPoint();
            }

            if (position.X <= 0)
            {
                position.X = 0;
            }
            if(position.X >= 800)
            {
                position.X = 800;
            }
            if(position.Y >= 480)
            {
                position.Y = 480;
            }
            if (position.Y <= 0)
            {
                position.Y = 0;
            }
           
            color = Color.White;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            background.Draw(spriteBatch);
            spriteBatch.Draw(rocket, position, null, color, rotation, offset, scale, SpriteEffects.None, 1);
           for(int i =0; i< bullets.Count;i++)
            {
                bullets[i].Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
