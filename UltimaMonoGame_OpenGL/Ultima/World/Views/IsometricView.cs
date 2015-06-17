/***************************************************************************
 *   IsometricRenderer.cs
 *   Based on code from ClintXNA's renderer: http://www.runuo.com/forums/xna/92023-hi.html
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using UltimaXNA.Configuration;
using UltimaXNA.Core.Graphics;
using UltimaXNA.Core.Input;
using UltimaXNA.Ultima.Entities;
using UltimaXNA.Ultima.Entities.Items;
using UltimaXNA.Ultima.EntityViews;
using UltimaXNA.Ultima.World.Controllers;
using UltimaXNA.Ultima.World.Maps;
#endregion

namespace UltimaXNA.Ultima.World.Views
{
    public class IsometricRenderer
    {
        #region RenderingVariables
        private SpriteBatch3D _spriteBatch;
        //private VertexPositionNormalTextureHue[] _vertexBufferStretched;
        #endregion

        #region LightingVariables
        private int _lightLevelPersonal = 9, _lightLevelOverall = 9;
        private float _lightDirection = 4.12f, _lightHeight = -0.75f;
        public int PersonalLightning
        {
            set { _lightLevelPersonal = value; RecalculateLightning(); }
            get { return _lightLevelPersonal; }
        }
        public int OverallLightning
        {
            set { _lightLevelOverall = value; RecalculateLightning(); }
            get { return _lightLevelOverall; }
        }
        public float LightDirection
        {
            set { _lightDirection = value; RecalculateLightning(); }
            get { return _lightDirection; }
        }
        public float LightHeight
        {
            set { _lightHeight = value; RecalculateLightning(); }
            get { return _lightHeight; }
        }
        #endregion

        private bool m_flag_HighlightMouseOver = false;
        public bool Flag_HighlightMouseOver
        {
            get { return m_flag_HighlightMouseOver; }
            set { m_flag_HighlightMouseOver = value; }
        }

        public int ObjectsRendered { get; internal set; }
        public bool DrawTerrain = true;
        private int _maxItemAltitude;

        Vector2 _renderOffset;
        public Vector2 RenderOffset
        {
            get { return _renderOffset; }
        }

        private readonly InputManager _input;

        public IsometricRenderer()
        {
            _input = UltimaServices.GetService<InputManager>();
        }

        public void Initialize()
        {
            _spriteBatch = UltimaServices.GetService<SpriteBatch3D>();

            //_vertexBufferStretched = new [] {
            //    new VertexPositionNormalTextureHue(new Vector3(), new Vector3(),  new Vector3(0, 0, 0)),
            //    new VertexPositionNormalTextureHue(new Vector3(), new Vector3(),  new Vector3(1, 0, 0)),
            //    new VertexPositionNormalTextureHue(new Vector3(), new Vector3(),  new Vector3(0, 1, 0)),
            //    new VertexPositionNormalTextureHue(new Vector3(), new Vector3(),  new Vector3(1, 1, 0))
            //};
        }

        public void Draw(Map map, Position3D center, MousePicking mousePick)
        {
            InternalDetermineIfUnderEntity(map, center);
            InternalDrawEntities(map, center, mousePick, out _renderOffset);
        }

        private void InternalDetermineIfUnderEntity(Map map, Position3D center)
        {
            // Are we inside (under a roof)? Do not draw tiles above our head.
            _maxItemAltitude = 255;

            MapTile mapTile;
            if ((mapTile = map.GetMapTile(center.X, center.Y)) == null) return;
            AEntity underObject, underTerrain;
            mapTile.IsZUnderEntityOrGround(center.Z, out underObject, out underTerrain);

            // if we are under terrain, then do not draw any terrain at all.
            DrawTerrain = (underTerrain == null);

            if (underObject == null) return;
            // Roofing and new floors ALWAYS begin at intervals of 20.
            // if we are under a ROOF, then get rid of everything above me.Z + 20
            // (this accounts for A-frame roofs). Otherwise, get rid of everything
            // at the object above us.Z.
            var underItem = underObject as Item;
            if (underItem != null)
            {

                if (underItem.ItemData.IsRoof)
                    _maxItemAltitude = center.Z - (center.Z % 20) + 20;
                else if (underItem.ItemData.IsSurface || underItem.ItemData.IsWall)
                    _maxItemAltitude = underItem.Z - (underItem.Z % 10);
                else
                {
                    var z = center.Z + ((underItem.ItemData.Height > 20) ? underItem.ItemData.Height : 20);
                    _maxItemAltitude = (int)(z);// - (z % 20));
                }
            }

            // If we are under a roof tile, do not make roofs transparent if we are on an edge.
            if (!(underObject is Item) || !((Item) underObject).ItemData.IsRoof) return;

            var isRoofSouthEast = true;
            if ((mapTile = map.GetMapTile(center.X + 1, center.Y)) != null)
            {
                mapTile.IsZUnderEntityOrGround(center.Z, out underObject, out underTerrain);
                isRoofSouthEast = underObject != null;
            }

            if (!isRoofSouthEast)
                _maxItemAltitude = 255;
        }

        private void InternalDrawEntities(Map map, Position3D center, MousePicking mousePick, out Vector2 renderOffset)
        {
            if (center == null)
            {
                renderOffset = new Vector2();
                return;
            }

            const int renderDimensionY = 16; // the number of tiles that are drawn for half the screen (doubled to fill the entire screen).
            const int renderDimensionX = 18; // the number of tiles that are drawn in the x-direction ( + renderExtraColumnsAtSides * 2 ).
            const int renderExtraColumnsAtSides = 2; // the client draws additional tiles at the edge to make wide objects that are mostly offscreen visible.


            // when the player entity is higher (z) in the world, we must offset the first row drawn. This variable MUST be a multiple of 2.
            int renderZOffset = (center.Z / 14) * 2 + 4;
            // this is used to draw tall objects that would otherwise not be visible until their ground tile was on screen. This may still skip VERY tall objects (those weird jungle trees?)
            int renderExtraRowsAtBottom = renderZOffset + 10;

            Point firstTile = new Point(
                center.X + renderExtraColumnsAtSides - ((renderZOffset + 1) / 2),
                center.Y - renderDimensionY - renderExtraColumnsAtSides - (renderZOffset / 2));

            renderOffset.X = ((Settings.Game.Resolution.Width + ((renderDimensionY) * 44)) / 2) - 22 + renderExtraColumnsAtSides * 44;
            renderOffset.X -= (int)((center.X_offset - center.Y_offset) * 22);
            renderOffset.X -= (firstTile.X - firstTile.Y) * 22;

            renderOffset.Y = ((Settings.Game.Resolution.Height - (renderDimensionY * 44)) / 2);
            renderOffset.Y += (center.Z * 4) + (int)(center.Z_offset * 4);
            renderOffset.Y -= (int)((center.X_offset + center.Y_offset) * 22);
            renderOffset.Y -= (firstTile.X + firstTile.Y) * 22;
            renderOffset.Y -= (renderZOffset) * 22;

            ObjectsRendered = 0; // Count of objects rendered for statistics and debug

            MouseOverList overList = new MouseOverList(_input.MousePosition, mousePick.PickOnly); // List of entities mouse is over.
            List<AEntity> deferredToRemove = new List<AEntity>();

            for (int col = 0; col < renderDimensionY * 2 + renderExtraRowsAtBottom; col++)
            {
                Vector3 drawPosition = new Vector3();
                drawPosition.X = (firstTile.X - firstTile.Y + (col % 2)) * 22 + renderOffset.X;
                drawPosition.Y = (firstTile.X + firstTile.Y + col) * 22 + renderOffset.Y;

                Point index = new Point(firstTile.X + ((col + 1) / 2), firstTile.Y + (col / 2));

                for (int row = 0; row < renderDimensionX + renderExtraColumnsAtSides * 2; row++)
                {
                    var maptile = map.GetMapTile(index.X - row, index.Y + row);
                    if (maptile == null)
                        continue;

                    foreach (var entity in maptile.Entities)
                    {
                        if (!DrawTerrain)
                        {
                            if (entity is Ground)
                                break;
                            if (entity.Z > maptile.Ground.Z)
                                break;
                        }

                        if (entity.Z >= _maxItemAltitude)
                            continue;

                        var view = entity.GetView();

                        if (view != null)
                            if (view.Draw(_spriteBatch, drawPosition, overList, map))
                                ObjectsRendered++;

                        if (entity is DeferredEntity)
                        {
                            deferredToRemove.Add(entity);
                        }
                    }

                    foreach (var deferred in deferredToRemove)
                        maptile.OnExit(deferred);
                    deferredToRemove.Clear();

                    drawPosition.X -= 44f;
                }
            }

            OverheadRenderer.Render(_spriteBatch, overList, map);

            // Update the MouseOver objects
            mousePick.UpdateOverEntities(overList, _input.MousePosition);

            // Draw the objects we just send to the spritebatch.
            _spriteBatch.Prepare(true, true);
            _spriteBatch.Flush();
        }

        private void RecalculateLightning()
        {
            float light = Math.Min(30 - OverallLightning + PersonalLightning, 30f);
            light = Math.Max(light, 0);
            light /= 30; // bring it between 0-1

            _spriteBatch.SetLightIntensity(light);


            // i'd use a fixed lightning direction for now - maybe enable this effect with a custom packet?
            _lightDirection = 1.2f;
            Vector3 lightDirection = Vector3.Normalize(new Vector3((float)Math.Cos(_lightDirection), (float)Math.Sin(_lightDirection), 1f));
            _spriteBatch.SetLightDirection(lightDirection);
        }
    }
}