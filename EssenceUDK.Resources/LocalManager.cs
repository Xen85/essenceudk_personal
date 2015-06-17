// This is based on project "Advanced WPF Localization".
// For updates and addition information look at http://www.codeproject.com/Articles/249369/Advanced-WPF-Localization

//TODO: Rewrire.... this dhit doesn't work and it's uncomfortable to use, we have to use smth like separated *.xml or *.ini
//      filse to make it smart and comfortable. I mean using separated localization for each module, or just sections in xml.

namespace EssenceUDK.Resources
{
    using System;
    using System.Windows;
    using System.Globalization;
    using System.Collections.Generic;
    using EssenceUDK.Resources.Localization;

    public static class LocalManager
    {
        private static int SelectedLang;
        private readonly static string[,] Languages = new[,] {
                                        {"Russian",  "ru-RU", "Русский"  },
                                        {"English",  "en-US", "English"  }, 
                                        {"German",   "de-DE", "Deutsch"  },
                                        {"Italian",  "it-IT", "Italiano" },
                                        {"Spanish",  "es-ES", "Español"  },
                                        {"Japanese", "ja-JP", "日本人"   },
        };

        public static string[] GetLocals()
        {
            var langs = new List<string>(Languages.GetLength(0));
            for (int lang_id = 0; lang_id < Languages.GetLength(0); ++lang_id)
                langs.Add(Languages[lang_id, 2]);
            return langs.ToArray();
        }

        public static void ApplyLocal(this Application app, string lang)
        {
            int lang_id;
            for (lang_id = Languages.GetLength(0) - 1; lang_id >= 0; --lang_id)
                if (String.Compare(Languages[lang_id, 0], lang, true) == 0 || String.Compare(Languages[lang_id, 2], lang, true) == 0)
                    break;

            if (SelectedLang == lang_id) return;
            var culture = CultureInfo.GetCultureInfo(Languages[lang_id, 1]);
            app.Dispatcher.Thread.CurrentCulture = culture;
            app.Dispatcher.Thread.CurrentUICulture = culture;
            
            LocalizationManager.UpdateValues();
            SelectedLang = lang_id;
        }
    }
}
