using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BetaSharp;

public class Translations
{
    public Language DefaultLanguage { get; private set; }

    public Language CurrentLanguage { get; private set; }

    public Dictionary<string, Language> Languages { get; private set; } = new Dictionary<string, Language>();

    public static event Action LanguageChanged;

    private Translations()
    {
        // do no languages if the asset manager isnt initialized or it failed to loda
        string content;
        if (AssetManager.Initialized)
        {
            var asset = AssetManager.Instance.getAsset($"{Language.LangFolder}/lang.json");
            if (asset == null)
                content = "{}";

            content = asset.GetTextContent();
        } else
        {
            content = "{}";
        }

        using JsonDocument doc = JsonDocument.Parse(content);

        foreach (JsonProperty prop in doc.RootElement.EnumerateObject())
        {
            string code = prop.Name;
            JsonElement details = prop.Value;

            string name = details.GetProperty("name").GetString();
            string author = details.GetProperty("author").GetString();

            Languages[code] = new(code, name, author);

            if (code == Language.DefaultLang)
            {
                DefaultLanguage = Languages[code];
            }
        }

        CurrentLanguage = DefaultLanguage;
    }

    public static Translations Instance { get; } = new Translations();

    public static void UseLanguage(string code)
    {
        if (Instance.Languages.ContainsKey(code))
        {
            Instance.CurrentLanguage = Instance.Languages[code];

            LanguageChanged?.Invoke();
        }
    }

    public static string Get(string key) => Instance.CurrentLanguage is not null ? Instance.CurrentLanguage[key] : key;

    public static string GetFormatted(string key, params object[] values)
    {
        string str = Get(key);

        for (int i = 0; i < values.Length; i++)
        {
            str = str.Replace($"%{i + 1}$s", values[i]?.ToString() ?? string.Empty);
        }

        if (str == "%s")
            str = key + " (Failed to translate key!)";

        return str;
    }

    public static string GetNamed(string key)
    {
        return Get($"{key}.name");
    }
}
