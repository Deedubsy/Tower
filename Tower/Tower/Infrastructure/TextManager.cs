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
        public SpriteFont spriteFont;
        Rectangle rec;
        string textValue;
        private Vector2 _pos;
        public Vector2 pos
        {
            get
            {
                return _pos;
            }
            set
            {
                this._pos = value;
                this.rec = new Rectangle(new Point((int)pos.X, (int)pos.Y), new Point(spriteFont.Texture.Width, spriteFont.Texture.Height));
            }
        }
        Vector2 origin;
        float scale;
        public event EventHandler ClickedEvent;
        public event EventHandler HoverEvent;

        public enum FontStyle
        {
            Arial
        };

        public TextManager(Game1 game, string value, FontStyle font, Point location, Point size, float scale)
        {
            this.spriteFont = game.Content.Load<SpriteFont>(GetFont(font));
            this.textValue = value;
            this.pos = location.ToVector2();
            this.origin = new Vector2();
            this.scale = scale;
            this.rec = new Rectangle(location, new Point(spriteFont.Texture.Width, spriteFont.Texture.Height));
        }

        public void Update(GameTime gt, InputManager input)
        {
            if(input.IsInsideRec(rec))
            {
                if(input.MouseLeftBtnPressed())
                {
                    //Clicked
                    ClickedEvent(null, null);
                }
                else
                {
                    //Hover effect
                    //HoverEvent.Invoke();
                    //ClickedEvent(null, null);
                    scale = 1.1f;
                }
            }
            else
            {
                scale = 1.0f;
            }
        }

        public void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Begin();
            sb.DrawString(spriteFont, textValue, pos, Color.Black, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
            sb.End();
        }

        private string GetFont(FontStyle fontStyle)
        {
            string sResult = string.Empty;

            switch (fontStyle)
            {
                case FontStyle.Arial: sResult = "Arial"; break;
                default:
                    sResult = "Arial";
                    break;
            }

            return sResult;
        }
    }
}
