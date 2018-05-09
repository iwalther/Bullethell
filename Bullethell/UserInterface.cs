using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Bullethell
{
    class UserInterface
    {
        static bool pause;
        static SpriteFont font;
        static KeyboardState prevKeyboardState = Keyboard.GetState();
        static List<Button> buttons = new List<Button>();
        static MouseState prevMouseState;

        public static void AddButton(Texture2D texture, string text, Vector2 scale, Vector2 textScale, Vector2 screenSize)
        {
            buttons.Add(new Button(texture, Vector2.Zero, text, scale, textScale, font));
            PlaceButtons(screenSize);
        }

        public static void LoadSpriteFont(ContentManager content, string fontName)
        {
            font = content.Load<SpriteFont>(fontName);
        }

        public static bool Update(KeyboardState keyboardState, MouseState mouseState)
        {
            bool exit = false;
            if (keyboardState.IsKeyDown(Keys.Escape) && prevKeyboardState.IsKeyUp(Keys.Escape))
            { 
                pause = !pause;
            }
            string clickedButton = "";
            if (pause)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    bool mouseOverButton = buttons[i].Update(mouseState);
                    if (mouseOverButton && mouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                    {
                        clickedButton = buttons[i].GetButtonText();
                    }
                }
            }
            switch (clickedButton)
            {
                case "Continue":
                    pause = false;
                    break;
                case "Exit":
                    exit = true;
                    break;
            }
            prevMouseState = mouseState;
            prevKeyboardState = keyboardState;
            return exit;
        }

        public static void Draw(SpriteBatch spriteBatch, Player player)
        {
            string playerHealth = "Health: " + player.GetHealth().ToString();
            string spacing = "   ";
            string playerScore = "Score: " + player.GetScore().ToString();
            spriteBatch.DrawString(font, playerHealth + spacing + playerScore, Vector2.Zero, Color.White);
            if (pause)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].Draw(spriteBatch);
                }
            }
        }

        public static bool GetPause()
        {
            return pause;
        }

        private static void PlaceButtons(Vector2 screenSize)
        {
            float factor = (float)buttons.Count + 3;
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Place(new Vector2(screenSize.X * 0.5f, screenSize.Y / factor * (i + 2)));
            }
        }
    }

}
