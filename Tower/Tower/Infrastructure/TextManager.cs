using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.Infrastructure
{
    public class TextManager
    {
        SpriteFont spriteFont;
        Rectangle rec;
        string textValue;
        Vector2 pos;
        Vector2 origin;
        float scale;
        public event Action ClickedEvent;
        public event Action HoverEvent;

        public enum FontStyle
        {
            Arial
        };

        public TextManager(Game1 game, string value, FontStyle font, Point location, Point size, float scale)
        {
            this.rec = new Rectangle(location, size);
            this.textValue = value;
            this.pos = location.ToVector2();
            this.origin = new Vector2();
            this.scale = scale;
            this.spriteFont = game.Content.Load<SpriteFont>(GetFont(font));
        }

        public void Update(GameTime gt, InputManager input)
        {
            if(input.IsInsideRec(rec))
            {
                if(input.MouseLeftBtnPressed())
                {
                    //Clicked
                    ClickedEvent.Invoke();
                }
                else
                {
                    //Hover effect
                    HoverEvent.Invoke();
                }
            }
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Begin();
            sb.DrawString(spriteFont, textValue, pos, Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            sb.End();
        }

        private string GetFont(FontStyle fontStyle)
        {
            string sResult = string.Empty;

            switch (fontStyle)
            {
                case FontStyle.Arial: sResult = "Arial_14"; break;
                default:
                    sResult = "Arial_14";
                    break;
            }

            return sResult;
        }
    }
}
