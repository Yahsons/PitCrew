using System.Globalization;
using System.Text.Json;

namespace PitCrewCommon
{
    public class Translatable
    {
        private static Dictionary<string, string> Translations { get; set; } = [];

        private static string currentLangague = Constants.DEFAULT_LANG;

        public static string Initialize(string fileName)
        {
            string path = Path.Combine("Languages", fileName);
            string langName = Path.GetFileNameWithoutExtension(fileName);

            if (!File.Exists(path) || !IsValidCulture(langName))
                langName = Constants.DEFAULT_LANG;

            currentLangague = langName;
            Load(Path.ChangeExtension(currentLangague, ".json"));
            return currentLangague;
        }

        public static void Load(string fileName)
        {
            string path = Path.Combine("Languages", fileName);

            if (!File.Exists(path))
                return;

            string json = File.ReadAllText(path);

            Translations.Clear();
            //If invalid json, simply don't use it.
            try
            {
                Translations = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                currentLangague = Path.GetFileNameWithoutExtension(fileName);
            }
            catch (JsonException e)
            {
                Logger.Error(201, e.StackTrace);
            }
        }

        public static string Get(string key)
        {
            if (Translations.TryGetValue(key, out string? value))
                return value;

            //Missing translation
            return key;
        }

        public static string GetCurrentLanguage()
        {
            return currentLangague;
        }

        public static bool IsValidCulture(string langName)
        {
            try
            {
                CultureInfo.GetCultureInfo(langName);
            }
            catch (CultureNotFoundException)
            {
                return false;
            }

            return true;
        }
    }
}
