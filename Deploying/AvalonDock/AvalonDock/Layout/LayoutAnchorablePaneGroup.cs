﻿//Copyright (c) 2007-2012, Adolfo Marinucci
//All rights reserved.

//Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
//following conditions are met:

//* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

//* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following 
//disclaimer in the documentation and/or other materials provided with the distribution.

//* Neither the name of Adolfo Marinucci nor the names of its contributors may be used to endorse or promote products
//derived from this software without specific prior written permission.

//THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
//INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, 
//EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
//STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, 
//EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace AvalonDock.Layout
{
    [ContentProperty("Children")]
    [Serializable]
    public class LayoutAnchorablePaneGroup : LayoutPositionableGroup<ILayoutAnchorablePane>, ILayoutAnchorablePane, ILayoutOrientableGroup
    {
        public LayoutAnchorablePaneGroup()
        {
        }
        
        public LayoutAnchorablePaneGroup(LayoutAnchorablePane firstChild)
        {
            Children.Add(firstChild);
        }

        #region Orientation

        private Orientation _orientation;
        public Orientation Orientation
        {
            get { return _orientation; }
            set
            {
                if (_orientation != value)
                {
                    RaisePropertyChanging("Orientation");
                    _orientation = value;
                    RaisePropertyChanged("Orientation");
                }
            }
        }

        #endregion

        protected override bool GetVisibility()
        {
            return Children.Count > 0 && Children.Any(c => c.IsVisible);
        }

        protected override void OnIsVisibleChanged()
        {
            UpdateParentVisibility();
            base.OnIsVisibleChanged();
        }

        void UpdateParentVisibility()
        {
            var parentPane = Parent as ILayoutElementWithVisibility;
            if (parentPane != null)
                parentPane.ComputeVisibility();
        }

        protected override void OnDockWidthChanged()
        {
            if (DockWidth.IsAbsolute && ChildrenCount == 1)
                ((ILayoutPositionableElement)Children[0]).DockWidth = DockWidth;

            base.OnDockWidthChanged();
        }

        protected override void OnDockHeightChanged()
        {
            if (DockHeight.IsAbsolute && ChildrenCount == 1)
                ((ILayoutPositionableElement)Children[0]).DockHeight = DockHeight;
            base.OnDockHeightChanged();
        }

        protected override void OnChildrenCollectionChanged()
        {
            if (DockWidth.IsAbsolute && ChildrenCount == 1)
                ((ILayoutPositionableElement)Children[0]).DockWidth = DockWidth;
            if (DockHeight.IsAbsolute && ChildrenCount == 1)
                ((ILayoutPositionableElement)Children[0]).DockHeight = DockHeight;
            base.OnChildrenCollectionChanged();
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteAttributeString("Orientation", Orientation.ToString());
            base.WriteXml(writer);
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("Orientation"))
                Orientation = (Orientation)Enum.Parse(typeof(Orientation), reader.Value, true);
            base.ReadXml(reader);
        }

#if DEBUG
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Debug.Write(new string(' ', tab * 4));
            System.Diagnostics.Debug.WriteLine("AnchorablePaneGroup({0})", Orientation);

            foreach (LayoutElement child in Children)
                child.ConsoleDump(tab + 1);
        }
#endif
    
    
    }

}
