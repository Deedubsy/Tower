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
        private float screenWidth;
        private float screenHeight;

        public Earth(Game1 oGame, SpriteBatch sb)
        {
            this.game = oGame;
            this.background = game.Content.Load<Texture2D>("courtTemplate");

            this.screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
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

            float x = (screenWidth / 2) - (background.Width + (background.Width / 4));
            float y = (screenHeight / 2) - (background.Height / 2);

            sb.Draw(background, new Vector2(x, y), null, Color.White, 0.0f, new Vector2(0, 0), 0.55f, SpriteEffects.None, 0.0f);
            sb.End();
        }
    }
}
