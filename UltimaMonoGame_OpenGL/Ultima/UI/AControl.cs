﻿/***************************************************************************
 *   Control.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using UltimaXNA.Configuration;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Core.Input.Windows;
#endregion

namespace UltimaXNA.Ultima.UI
{
    internal delegate void ControlMouseButtonEvent(int x, int y, MouseButton button);
    internal delegate void ControlMouseEvent(int x, int y);
    internal delegate void ControlEvent();

    public delegate void PublicControlEvent();

    /// <summary>
    /// The base class used by all GUI objects.
    /// NOTE: Gumps MUST NOT inherit from Control. They must inherit from Gump instead.
    /// </summary>
    public abstract class AControl
    {
        public int Serial = 0;
        public bool IsModal = false;

        bool m_enabled = false;
        bool m_visible = false;
        bool m_isInitialized = false;
        bool m_isDisposed = false;
        public bool Enabled { get { return m_enabled; } set { m_enabled = value; } }
        public bool Visible { get { return m_visible; } set { m_visible = value; } }
        public bool IsInitialized { get { return m_isInitialized; } set { m_isInitialized = value; } }
        public bool IsDisposed { get { return m_isDisposed; } set { m_isDisposed = value; } }
        public bool IsMovable = false;

        bool m_handlesMouseInput = false;
        public bool HandlesMouseInput { get { return m_handlesMouseInput; } set { m_handlesMouseInput = value; } }
        bool m_handlesKeyboardFocus = false;
        public bool HandlesKeyboardFocus
        {
            get
            {
                if (m_handlesKeyboardFocus)
                    return true;
                return m_controls != null && m_controls.Any(c => c.HandlesKeyboardFocus);
            }
            set
            {
                m_handlesKeyboardFocus = value;
                if (m_UserInterface.KeyboardFocusControl == null)
                    m_UserInterface.KeyboardFocusControl = this;
            }
        }
        public AControl KeyboardFocusControl
        {
            get
            {
                if (m_handlesKeyboardFocus)
                    return this;
                return m_controls == null ? null : (from c in m_controls where c.HandlesKeyboardFocus select c.KeyboardFocusControl).FirstOrDefault();
            }
        }

        protected bool m_renderFullScreen = false;

        internal ControlMouseButtonEvent OnMouseClick;
        internal ControlMouseButtonEvent OnMouseDoubleClick;
        internal ControlMouseButtonEvent OnMouseDown;
        internal ControlMouseButtonEvent OnMouseUp;
        internal ControlMouseEvent OnMouseOver;
        internal ControlMouseEvent OnMouseOut;

        float m_inputMultiplier = 1.0f;
        public float InputMultiplier
        {
            set { m_inputMultiplier = value; }
            get
            {
                if (m_renderFullScreen)
                    return m_inputMultiplier;
                else
                    return 1.0f;
            }
        }

        int m_page = 0;
        public int Page
        {
            get
            {
                return m_page;
            }
        }
        int m_activePage = 0; // we always draw m_activePage and Page 0.
        public int ActivePage
        {
            get { return m_activePage; }
            set
            {
                m_activePage = value;
                // If we own the current KeyboardFocusControl, then we should clear it.
                // UNLESS page = 0; in which case it still exists and should maintain focus.
                // Clear the current keyboardfocus if we own it and it's page != 0
                // If the page = 0, then it will still exist so it should maintain focus.
                if (m_UserInterface.KeyboardFocusControl != null)
                {
                    if (Controls.Contains(m_UserInterface.KeyboardFocusControl))
                    {
                        if (m_UserInterface.KeyboardFocusControl.Page != 0)
                            m_UserInterface.KeyboardFocusControl = null;
                    }
                }
                // When ActivePage changes, check to see if there are new text input boxes
                // that we should redirect text input to.
                foreach (AControl c in Controls)
                {
                    if (c.HandlesKeyboardFocus && (c.Page == m_activePage))
                    {
                        m_UserInterface.KeyboardFocusControl = c;
                        break;
                    }
                }
            }
        }

        Rectangle m_area = Rectangle.Empty;
        Point m_position;
        protected int OwnerX
        {
            get
            {
                if (m_owner != null)
                    return m_owner.X + m_owner.OwnerX;
                else
                    return 0;
            }
        }
        protected int OwnerY
        {
            get
            {
                if (m_owner != null)
                    return m_owner.Y + m_owner.OwnerY;
                else
                    return 0;
            }
        }
        public int X { get { return m_position.X; } set { m_position.X = value; } }
        public int Y { get { return m_position.Y; } set { m_position.Y = value; } }
        public virtual int Width
        {
            get { return m_area.Width; }
            set
            {
                m_area.Width = value;
            }
        }
        public virtual int Height
        {
            get { return m_area.Height; }
            set
            {
                m_area.Height = value;
            }
        }
        public Point Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }
        public Point Size
        {
            get { return new Point(m_area.Width, m_area.Height); }
            set
            {
                m_area.Width = value.X;
                m_area.Height = value.Y;
            }
        }
        public Rectangle Area
        {
            get { return m_area; }
        }

        protected AControl m_owner = null;
        public AControl Owner { get { return m_owner; } }
        private List<AControl> m_controls = null;
        protected List<AControl> Controls
        {
            get
            {
                if (m_controls == null)
                    m_controls = new List<AControl>();
                return m_controls;
            }
        }

#if DEBUG
        static Texture2D m_boundsTexture;
#endif

        UserInterfaceService m_UserInterface;

        public AControl(AControl owner, int page)
        {
            m_owner = owner;
            m_page = page;
            Visible = true;
            m_UserInterface = UltimaServices.GetService<UserInterfaceService>();
        }

        public void ControlInitialize()
        {
            m_isInitialized = true;
            m_isDisposed = false;
            Initialize();
        }

        public virtual void Initialize()
        {

        }

        public AControl AddControl(AControl c)
        {
            Controls.Add(c);
            return LastControl;
        }

        public AControl LastControl
        {
            get { return Controls[Controls.Count - 1]; }
        }

        public void ClearControls()
        {
            if (Controls != null)
                foreach (AControl c in Controls)
                    c.Dispose();
        }

        public virtual void Dispose()
        {
            ClearControls();
            m_isDisposed = true;
        }

        DragWidget m_dragger;
        public void MakeDragger(AControl toMove)
        {
            HandlesMouseInput = true;
            m_dragger = new DragWidget(this, m_owner);
        }

        AControl m_closeTarget;
        public void MakeCloseTarget(AControl toClose)
        {
            m_closeTarget = toClose;
            HandlesMouseInput = true;
            OnMouseClick += onCloseTargetClick;
        }
        void onCloseTargetClick(int x, int y, MouseButton button)
        {
            if (button == MouseButton.Right)
            {
                m_closeTarget.Dispose();
            }
        }

        public AControl[] HitTest(Point position, bool alwaysHandleMouseInput)
        {
            List<AControl> focusedControls = new List<AControl>();

            // offset the mouse position if we are rendering full screen...
            position.X = (int)((float)(position.X) / InputMultiplier);
            position.Y = (int)((float)(position.Y) / InputMultiplier);

            // If we're owned by something, make sure we increment our hitArea to show 
            // position.X -= OwnerX;
            // position.Y -= OwnerY;

            bool inBounds = Area.Contains((int)position.X - OwnerX, (int)position.Y - OwnerY);
            if (inBounds)
            {
                if (InternalHitTest((int)position.X - X - OwnerX, (int)position.Y - Y - OwnerY))
                {
                    if (alwaysHandleMouseInput || HandlesMouseInput)
                        focusedControls.Insert(0, this);
                    foreach (AControl c in Controls)
                    {
                        if ((c.Page == 0) || (c.Page == ActivePage))
                        {
                            AControl[] c1 = c.HitTest(position, false);
                            if (c1 != null)
                            {
                                for (int i = c1.Length - 1; i >= 0; i--)
                                {
                                    focusedControls.Insert(0, c1[i]);
                                }
                            }
                        }
                    }
                }
            }

            if (focusedControls.Count == 0)
                return null;
            else
                return focusedControls.ToArray();
        }

        protected virtual bool InternalHitTest(int x, int y)
        {
            return true;
        }

        virtual public void Update(double totalMS, double frameMS)
        {
            if (!m_isInitialized)
                return;

            // update our area X and Y to reflect any movement.
            m_area.X = X;
            m_area.Y = Y;

            foreach (AControl c in Controls)
            {
                if (!c.IsInitialized)
                    c.ControlInitialize();
                c.Update(totalMS, frameMS);
            }

            List<AControl> disposedControls = new List<AControl>();
            foreach (AControl c in Controls)
            {
                if (c.IsDisposed)
                    disposedControls.Add(c);
            }
            foreach (AControl c in disposedControls)
            {
                Controls.Remove(c);
            }
        }

        virtual public void Draw(SpriteBatchUI spriteBatch)
        {
            if (!m_isInitialized)
                return;
            if (!Visible)
                return;

#if DEBUG
            if (Settings.Debug.ShowUIOutlines)
                DrawBounds(spriteBatch, Color.White);
#endif

            foreach (AControl c in Controls)
            {
                if ((c.Page == 0) || (c.Page == ActivePage))
                {
                    if (c.IsInitialized)
                    {
                        Point saved = c.Position;
                        c.Position = new Point(c.Position.X + Position.X, c.Position.Y + Position.Y);
                        c.Draw(spriteBatch);
                        c.Position = saved;
                    }
                }
            }
        }

#if DEBUG
        protected void DrawBounds(SpriteBatchUI spriteBatch, Color color)
        {
            int hue = IO.HuesXNA.GetWebSafeHue(color);

            Rectangle drawArea = m_area;
            if (m_owner == null)
            {
                m_area.X -= X;
                m_area.Y -= Y;
            }

            if (m_boundsTexture == null)
            {
                m_boundsTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                m_boundsTexture.SetData<Color>(new Color[] { Color.White });
            }

            spriteBatch.Draw2D(m_boundsTexture, new Rectangle(X, Y, Width, 1), hue, false, false);
            spriteBatch.Draw2D(m_boundsTexture, new Rectangle(X, Y + Height - 1, Width, 1), hue, false, false);
            spriteBatch.Draw2D(m_boundsTexture, new Rectangle(X, Y, 1, Height), hue, false, false);
            spriteBatch.Draw2D(m_boundsTexture, new Rectangle(X + Width - 1, Y, 1, Height), hue, false, false);
        }
#endif

        public virtual void ActivateByButton(int buttonID)
        {
            if (m_owner != null)
                m_owner.ActivateByButton(buttonID);
        }

        public virtual void ActivateByHREF(string href)
        {
            if (m_owner != null)
                m_owner.ActivateByHREF(href);
        }

        public virtual void ActivateByKeyboardReturn(int textID, string text)
        {
            if (m_owner != null)
                m_owner.ActivateByKeyboardReturn(textID, text);
        }

        public virtual void ChangePage(int pageIndex)
        {
            if (m_owner != null)
                m_owner.ChangePage(pageIndex);
        }

        public void MouseDown(Point position, MouseButton button)
        {
            lastClickPosition = position;
            int x = (int)position.X - X - OwnerX;
            int y = (int)position.Y - Y - OwnerY;
            mouseDown(x, y, button);
            if (OnMouseDown != null)
                OnMouseDown(x, y, button);
        }

        public void MouseUp(Point position, MouseButton button)
        {
            int x = (int)position.X - X - OwnerX;
            int y = (int)position.Y - Y - OwnerY;
            mouseUp(x, y, button);
            if (OnMouseUp != null)
                OnMouseUp(x, y, button);
        }

        public void MouseOver(Point position)
        {
            // Does not double-click if you move your mouse more than x pixels from where you first clicked.
            if (Math.Abs(lastClickPosition.X - position.X) + Math.Abs(lastClickPosition.Y - position.Y) > 3)
                maxTimeForDoubleClick = 0.0f;

            int x = (int)position.X - X - OwnerX;
            int y = (int)position.Y - Y - OwnerY;
            mouseOver(x, y);
            if (OnMouseOver != null)
                OnMouseOver(x, y);
        }

        public void MouseOut(Point position)
        {
            int x = (int)position.X - X - OwnerX;
            int y = (int)position.Y - Y - OwnerY;
            mouseOut(x, y);
            if (OnMouseOut != null)
                OnMouseOut(x, y);
        }

        float maxTimeForDoubleClick = 0f;
        Point lastClickPosition;

        public void MouseClick(Point position, MouseButton button)
        {
            int x = (int)position.X - X - OwnerX;
            int y = (int)position.Y - Y - OwnerY;

            bool doubleClick = false;
            if (maxTimeForDoubleClick != 0f)
            {
                if (UltimaEngine.TotalMs <= maxTimeForDoubleClick)
                {
                    maxTimeForDoubleClick = 0f;
                    doubleClick = true;
                }
            }
            else
            {
                maxTimeForDoubleClick = (float)UltimaEngine.TotalMs + EngineVars.DoubleClickMS;
            }

            mouseClick(x, y, button);
            if (OnMouseClick != null)
                OnMouseClick(x, y, button);

            if (doubleClick)
            {
                mouseDoubleClick(x, y, button);
                if (OnMouseDoubleClick != null)
                    OnMouseDoubleClick(x, y, button);
            }
        }

        public void KeyboardInput(InputEventKeyboard e)
        {
            keyboardInput(e);
        }

        protected virtual void mouseDown(int x, int y, MouseButton button)
        {

        }

        protected virtual void mouseUp(int x, int y, MouseButton button)
        {

        }

        protected virtual void mouseOver(int x, int y)
        {

        }

        protected virtual void mouseOut(int x, int y)
        {

        }

        protected virtual void mouseClick(int x, int y, MouseButton button)
        {

        }

        protected virtual void mouseDoubleClick(int x, int y, MouseButton button)
        {

        }

        protected virtual void keyboardInput(InputEventKeyboard e)
        {

        }

        internal void Center()
        {
            Position = new Point(
                (m_UserInterface.Width - Width) / 2,
                (m_UserInterface.Height - Height) / 2);
        }

        /// <summary>
        /// This is called when the Control that current has keyboard focus releases that focus; for example, when Tab is pressed.
        /// </summary>
        /// <param name="c">The control that is releasing focus.</param>
        internal void KeyboardTabToNextFocus(AControl c)
        {
            int startIndex = Controls.IndexOf(c);
            for (int i = startIndex + 1; i < Controls.Count; i++)
            {
                if (Controls[i].HandlesKeyboardFocus)
                {
                    m_UserInterface.KeyboardFocusControl = Controls[i];
                    return;
                }
            }
            for (int i = 0; i < startIndex; i++)
            {
                if (Controls[i].HandlesKeyboardFocus)
                {
                    m_UserInterface.KeyboardFocusControl = Controls[i];
                    return;
                }
            }
        }
    }
}