using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tower.GameStates
{
    public class Menu : GSM.IGameState
    {
        private Game1 game;
        private Dictionary<string, SpriteFont> MenuItems;

        public Menu(Game1 oGame)
        {
            this.game = oGame;
            this.MenuItems = new Dictionary<string, SpriteFont>();
            this.MenuItems.Add("", )
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
            sb.End();
        }
    }
}
