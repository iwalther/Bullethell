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
    class Button
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 scale;
        Vector2 offset;
        Vector2 textOffset;
        SpriteFont font;
        Vector2 textScale;
        Color color;
        string text;

        public Button(Texture2D buttonTexture, Vector2 startPosition, string buttonText, Vector2 buttonScale, Vector2 buttonTextScale, SpriteFont textFont)
        {
            font = textFont;
            texture = buttonTexture;
            text = buttonText;
            position = startPosition;
            scale = buttonScale;
            textScale = buttonTextScale;
            offset = buttonTexture.Bounds.Size.ToVector2() * 0.5f;
            rectangle = new Rectangle((position - offset).ToPoint(), (buttonTexture.Bounds.Size.ToVector2() * scale).ToPoint());
            textOffset = font.MeasureString(buttonText) * 0.5f;

        }
        public bool Update(MouseState mouseState)
        {
            bool containsMouse = false;
            color = Color.White;
            if (rectangle.Contains(mouseState.Position))
            {
                color = Color.Green;
                containsMouse = true;
            }
            return containsMouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0, offset, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0);
            spriteBatch.DrawString(font, text, position, Color.White, 0, textOffset, textScale, SpriteEffects.None, 0);
        }

        public string GetButtonText()
        {
            return text;
        }

        public void Place(Vector2 newPosition)
        {
            position = newPosition;
            rectangle.Location = (position - offset).ToPoint();
        }

    }
}