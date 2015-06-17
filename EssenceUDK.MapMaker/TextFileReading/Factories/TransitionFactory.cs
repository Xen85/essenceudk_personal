using System;
using System.Collections.Generic;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes;
using EssenceUDK.MapMaker.Elements.BaseTypes.ComplexTypes.Enum;

namespace EssenceUDK.MapMaker.TextFileReading.Factories
{
    public class FactoryTransition : Factory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filelocation"></param>
        public FactoryTransition(string filelocation)
            : base(filelocation)
        {
        }

        protected char[] sep = { ' ', ',', '\t' };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="transition"></param>
        /// <param name="j"></param>
        /// <param name="s"></param>
        protected virtual void SetTransition(int counter, Transition transition, int j, string s)
        {
            int value = Convert.ToInt32(s, 16);
            transition.AddElement((LineType)counter,j, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="strings"></param>
        /// <param name="counter"></param>
        protected void TransitionCheck(Transition transition, List<String> strings, int counter)
        {
            counter = counter%3;
            for (int j = 0; j < strings.Count; j++)
            {

                string[] defined = strings[j].Split(sep, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in defined)
                {
                    SetTransition(counter, transition, j, s);
                }
               
            }

        }
    }
}
