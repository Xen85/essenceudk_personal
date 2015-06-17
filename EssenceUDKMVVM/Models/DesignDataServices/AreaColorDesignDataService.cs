using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EssenceUDK.MapMaker.Elements;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorArea;
using EssenceUDK.MapMaker.Elements.ColorArea.ColorMountains;
using EssenceUDK.MapMaker.Elements.Textures.TexureCliff;

namespace EssenceUDKMVVM.Models.DesignDataServices
{
    public class AreaColorDesignDataService : IServiceModelAreaColor
    {
        public void GetData(Action<object, Exception> callback)
        {

            var areacolor = new AreaColor();
            var random = new Random();
            areacolor.CliffCoast = false;
            areacolor.CoastAltitude = -5;
            areacolor.CoastSmoothCircles.Add(new CircleMountain()
            {
                From = random.Next(1, 50),
                To = random.Next(51, 100)
            });
            areacolor.Color = Color.FromRgb(0, 100, 100);
            areacolor.CoastSmoothCircles.Add(new CircleMountain()
            {
                From = random.Next(1, 50),
                To = random.Next(51, 100)
            });
            areacolor.ColorTopMountain = Color.FromRgb(50, 50, 50);
            areacolor.Index = 666;
            areacolor.Max = -5;
            areacolor.Min = -7;
            areacolor.MinCoastTextureZ = -5;
            areacolor.ModeAutomatic = false;
            areacolor.Name = "succhiottone";
            foreach (
                LineType lineType in
                    Enum.GetValues(typeof (LineType)))
            {
                foreach (var direction in Enum.GetValues(typeof(Direction)))
                {
                    for (var i = 0; i < 10; i ++)
                    {
                        areacolor.Coasts.Ground.AddElement(lineType, (int)direction, random.Next(0, 100));
                        areacolor.Coasts.Coast.AddElement(lineType, (int)direction, random.Next(100, 200));
                    }
                }
            }

            for (int color = 0; color < 2; color++)
            {
                
            
                var numberRed = (byte)random.Next(0, byte.MaxValue);
                var numberGreen = (byte)random.Next(0, byte.MaxValue);
                var numberBlue = (byte)random.Next(0, byte.MaxValue);
                var idTo = random.Next(0,byte.MaxValue);
                foreach (DirectionCliff cliffType in Enum.GetValues(typeof(DirectionCliff)))
                {
                    for (int i = 0; i < 3; i++)
                    {
                        List<int> list = new List<int>() {random.Next(0, 400), random.Next(0, 400), random.Next(0, 400)};
                        areacolor.TransitionCliffTextures.Add(new AreaTransitionCliffTexture()
                        {
                            Directions = cliffType, ColorTo = Color.FromArgb(byte.MaxValue, numberRed, numberGreen, numberBlue),
                            List = new ObservableCollection<int>(list),
                            Name = "Color Cliff " + Color.FromArgb(byte.MaxValue, numberRed, numberGreen, numberBlue)
                            //IdTo = idTo

                        });
                    }
                }

            }

            for (int i = 0; i < 10; i++)
            {
                var min = random.Next(0, 10);
                var max = random.Next(10, 20);
                areacolor.List.Add(new CircleMountain() {From = min, To = max});
            }
            callback(areacolor, null);
        }
    }
}
