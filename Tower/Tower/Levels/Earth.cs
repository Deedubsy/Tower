using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;


namespace Tower.Levels
{
    public class Earth : GSM.IGameState
    {
        private Texture2D background;
        private Game1 game;

        public Earth(Game1 oGame, SpriteBatch sb)
        {
            this.game = oGame;
            this.background = game.Content.Load<Texture2D>("earth_bg");
        }

        public override void OnPop()
        {

        }

        public override void OnPush()
        {

        }

        public override void Update(GameTime gt)
        {

        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Begin();
            sb.Draw(background, new Vector2(0, 0), Color.SkyBlue);
            sb.End();
        }
    }
}
