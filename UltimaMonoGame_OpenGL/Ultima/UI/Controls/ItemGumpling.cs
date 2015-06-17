/***************************************************************************
 *   ItemGumpling.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UltimaXNA.Ultima.Entities.Items;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Core.Input.Windows;
using UltimaXNA.Ultima.UI;
using UltimaXNA.Ultima.World;
using UltimaXNA.Ultima.Network.Client;

namespace UltimaXNA.Ultima.UI.Controls
{
    class ItemGumpling : AControl
    {
        protected Texture2D Texture = null;
        Item _item;
        public Item Item { get { return _item; } }
        public Serial ContainerSerial { get { return _item.Parent.Serial; } }
        public bool CanPickUp = true;

        bool _clickedCanDrag = false;
        float _pickUpTime;
        Point _clickPoint;
        bool _sendClickIfNoDoubleClick = false;
        float _singleClickTime;

        private readonly WorldModel _world;

        public ItemGumpling(AControl owner, Item item)
            : base(owner, 0)
        {
            BuildGumpling(item);
            HandlesMouseInput = true;

            _world = UltimaServices.GetService<WorldModel>();
        }

        void BuildGumpling(Item item)
        {
            Position = item.InContainerPosition;
            _item = item;
        }

        public override void Update(double totalMS, double frameMS)
        {
            if (_item.IsDisposed)
            {
                Dispose();
                return;
            }
           

            if (_clickedCanDrag && UltimaEngine.TotalMs >= _pickUpTime)
            {
                _clickedCanDrag = false;
                AttemptPickUp();
            }

            if (_sendClickIfNoDoubleClick && UltimaEngine.TotalMs >= _singleClickTime)
            {
                _sendClickIfNoDoubleClick = false;
                _world.Interaction.SingleClick(_item);
            }
            base.Update(totalMS, frameMS);
        }

        public override void Draw(SpriteBatchUI spriteBatch)
        {
            if (Texture == null)
            {
                Texture = IO.ArtData.GetStaticTexture(_item.DisplayItemID);
                Size = new Point(Texture.Width, Texture.Height);
            }
            spriteBatch.Draw2D(Texture, Position, _item.Hue, false, false);
            base.Draw(spriteBatch);
        }

        protected override bool InternalHitTest(int x, int y)
        {
            // Allow selection if there is a non-transparent pixel below the mouse cursor or at an offset of
            // (-1,0), (0,-1), (1,0), or (1,1). This will allow selection even when the mouse cursor is directly
            // over a transparent pixel, and will also increase the 'selection space' of an item by one pixel in
            // each dimension - thus a very thin object (2-3 pixels wide) will be increased.

            if (x == 0)
                x++;
            if (x == Texture.Width - 1)
                x--;
            if (y == 0)
                y++;
            if (y == Texture.Height - 1)
                y--;

            var pixelData = new Color[9];
            Texture.GetData(0, new Rectangle(x - 1, y - 1, 3, 3), pixelData, 0, 9);
            return (pixelData[1].A > 0) || (pixelData[3].A > 0) ||
                   (pixelData[4].A > 0) || (pixelData[5].A > 0) ||
                   (pixelData[7].A > 0);
        }

        protected override void mouseDown(int x, int y, MouseButton button)
        {
            // if click, we wait for a moment before picking it up. This allows a single click.
            _clickedCanDrag = true;
            _pickUpTime = (float)UltimaEngine.TotalMs + EngineVars.ClickAndPickUpMS;
            _clickPoint = new Point(x, y);
        }

        protected override void mouseOver(int x, int y)
        {
            // if we have not yet picked up the item, AND we've moved more than 3 pixels total away from the original item, pick it up!
            if (!_clickedCanDrag || (Math.Abs(_clickPoint.X - x) + Math.Abs(_clickPoint.Y - y) <= 3)) return;
            _clickedCanDrag = false;
            AttemptPickUp();
        }

        protected override void mouseClick(int x, int y, MouseButton button)
        {
            if (!_clickedCanDrag) return;
            _clickedCanDrag = false;
            _sendClickIfNoDoubleClick = true;
            _singleClickTime = (float)UltimaEngine.TotalMs + EngineVars.DoubleClickMS;
        }

        protected override void mouseDoubleClick(int x, int y, MouseButton button)
        {
            _world.Interaction.DoubleClick(_item);
            _sendClickIfNoDoubleClick = false;
        }

        protected virtual Point InternalGetPickupOffset(Point offset)
        {
            return offset;
        }

        private void AttemptPickUp()
        {
            if (!CanPickUp) return;
            if (this is ItemGumplingPaperdoll)
            {
                int w, h;
                IO.ArtData.GetStaticDimensions(Item.DisplayItemID, out w, out h);
                var clickPoint = new Point(w / 2, h / 2);
                _world.Interaction.PickupItem(_item, InternalGetPickupOffset(clickPoint));
            }
            else
            {
                _world.Interaction.PickupItem(_item, InternalGetPickupOffset(_clickPoint));
            }
        }
    }
}
