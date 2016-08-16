using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tower.Infrastructure;

namespace Tower.GameStates
{
    public class Menu : GSM.IGameState
    {
        private Game1 game;
        private TextManager singleplayer;
        private TextManager multiplayer;
        private TextManager options;
        private TextManager exit;
        private InputManager inputManager;

        float x, y;

        public Menu(Game1 oGame, InputManager inputManager)
        {
            this.game = oGame;
            this.inputManager = inputManager;

            this.y = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            this.x = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2;

            this.singleplayer = new TextManager(oGame, "Single Player", TextManager.FontStyle.Arial
                , new Point(300, 100), new Point(300, 100), 1);
            this.multiplayer = new TextManager(oGame, "Multiplayer", TextManager.FontStyle.Arial
                , new Point(300, 200), new Point(300, 100), 1);
            this.options = new TextManager(oGame, "Options", TextManager.FontStyle.Arial
                , new Point(300, 300), new Point(300, 100), 1);
            this.exit = new TextManager(oGame, "Exit", TextManager.FontStyle.Arial
                , new Point(300, 400), new Point(300, 100), 1);

            singleplayer.ClickedEvent += Singleplayer_ClickedEvent;
            multiplayer.ClickedEvent += Multiplayer_ClickedEvent;
            options.ClickedEvent += Options_ClickedEvent;
            exit.ClickedEvent += Exit_ClickedEvent;

            AlignMenuItems();
        }

        public override void OnPop()
        {

        }

        public override void OnPush()
        {

        }

        public override void Update(GameTime gt)
        {
            singleplayer.Update(gt, inputManager);
            multiplayer.Update(gt, inputManager);
            options.Update(gt, inputManager);
            exit.Update(gt, inputManager);
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            singleplayer.Draw(gt, sb);
            multiplayer.Draw(gt, sb);
            options.Draw(gt, sb); 
            exit.Draw(gt, sb);
        }

        void AlignMenuItems()
        {
            Vector2 dimensions = Vector2.Zero;

            dimensions += new Vector2(singleplayer.spriteFont.Texture.Width, singleplayer.spriteFont.Texture.Height);
            dimensions += new Vector2(multiplayer.spriteFont.Texture.Width, multiplayer.spriteFont.Texture.Height);
            dimensions += new Vector2(options.spriteFont.Texture.Width, options.spriteFont.Texture.Height);
            dimensions += new Vector2(exit.spriteFont.Texture.Width, exit.spriteFont.Texture.Height);

            dimensions = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - dimensions.X) / 2,
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - dimensions.Y) / 2);
            int i = 0;
            singleplayer.pos = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - singleplayer.spriteFont.Texture.Width) / 2, 
                dimensions.Y + (singleplayer.spriteFont.Texture.Height * i++));
            multiplayer.pos = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - multiplayer.spriteFont.Texture.Width) / 2,
                dimensions.Y + (multiplayer.spriteFont.Texture.Height * i++));
            options.pos = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - options.spriteFont.Texture.Width) / 2,
                dimensions.Y + (options.spriteFont.Texture.Height * i++));
            exit.pos = new Vector2((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - exit.spriteFont.Texture.Width) / 2,
                dimensions.Y + (exit.spriteFont.Texture.Height * i++));
        }

        #region Click Events

        private void Exit_ClickedEvent(object sender, EventArgs e)
        {
            GSM.GameStateManager.PushState("SINGLEPLAYER");
        }

        private void Options_ClickedEvent(object sender, EventArgs e)
        {
            GSM.GameStateManager.PushState("SINGLEPLAYER");
        }

        private void Multiplayer_ClickedEvent(object sender, EventArgs e)
        {
            GSM.GameStateManager.PushState("SINGLEPLAYER");
        }

        private void Singleplayer_ClickedEvent(object sender, EventArgs e)
        {
            GSM.GameStateManager.PushState("SINGLEPLAYER");
        }

        #endregion
    }
}
