﻿/***************************************************************************
 *   ItemGumplingPaperdoll.cs
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using Microsoft.Xna.Framework;
using UltimaXNA.Ultima.Entities.Items;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Ultima.UI;

namespace UltimaXNA.Ultima.UI.Controls
{
    class ItemGumplingPaperdoll : ItemGumpling
    {
        public int SlotIndex = 0;
        public bool IsFemale = false;

        private int m_x, m_y;
        private bool m_isBuilt = false;

        public ItemGumplingPaperdoll(AControl owner, int x, int y, Item item)
            : base(owner, item)
        {
            m_x = x;
            m_y = y;
        }

        protected override Point InternalGetPickupOffset(Point offset)
        {
            // fix this to be more centered on the object.
            return offset;
        }

        public override void Update(double totalMS, double frameMS)
        {
            base.Update(totalMS, frameMS);
            if (!m_isBuilt)
            {
                Position = new Point(m_x, m_y);
            }
        }

        public override void Draw(SpriteBatchUI spriteBatch)
        {
            
            if (Texture == null)
            {
                if (IsFemale)
                    Texture = IO.GumpData.GetGumpXNA(Item.ItemData.AnimID + 60000);
                if (Texture == null)
                    Texture = IO.GumpData.GetGumpXNA(Item.ItemData.AnimID + 50000);
                Size = new Point(Texture.Width, Texture.Height);
            }
            spriteBatch.Draw2D(Texture, Position, Item.Hue & 0x7FFF, (Item.Hue & 0x8000) == 0x8000 ? true : false, false);
            base.Draw(spriteBatch);
        }
    }
}
