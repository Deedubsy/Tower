using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Tower.Infrastructure
{
    public class InputManager
    {
        KeyboardState currentKeyState, prevKeyState;
        MouseState currentMouseState, prevMouseState;

        private static InputManager instance;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        public void Update()
        {
            //Keyboard
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            //Mouse
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
        }

        #region Keyboard

        public bool KeyPressed(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyUp(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys)
        {
            foreach (var key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        #endregion

        #region Mouse

        public bool MouseLeftBtnPressed(params Keys[] keys)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public bool MouseLeftBtnDown(params Keys[] keys)
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseLeftBtnUp(params Keys[] keys)
        {
            if (prevMouseState.LeftButton == ButtonState.Pressed &&
                currentMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public bool MouseRightBtnPressed(params Keys[] keys)
        {
            if (currentMouseState.RightButton == ButtonState.Pressed &&
                prevMouseState.RightButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public bool MouseRightBtnDown(params Keys[] keys)
        {
            if (currentMouseState.RightButton == ButtonState.Pressed)
                return true;
            else
                return false;
        }

        public bool MouseRightBtnUp(params Keys[] keys)
        {
            if (prevMouseState.RightButton == ButtonState.Pressed &&
                currentMouseState.RightButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        public Point GetMousePos()
        {
            return new Point(currentMouseState.X, currentMouseState.Y); 
        }

        public bool IsInsideRec(Rectangle area)
        {
            if (area.Contains(GetMousePos()))
                return true;
            else
                return false;
        }

        #endregion
    }
}
