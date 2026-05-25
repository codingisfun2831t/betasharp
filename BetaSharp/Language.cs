using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Hexa.NET.ImGui.Backends.Vulkan;
using Microsoft.Extensions.Logging;

namespace BetaSharp;

public class Language
{
    private static ILogger s_logger = Log.Instance.For<Language>();

    public static string LangFolder = "lang";
    public static string DefaultLang = "en_us";

    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Author { get; private set; }

    public Dictionary<string, string>? Translations { get; private set; } // null if not loaded yet

    public bool Loaded => Translations != null;

    public Language(string code, string name, string author)
    {
        Code = code;
        Name = name;
        Author = author;

        if (Code == DefaultLang)
        {
            Load(); // always wanna load it (fallback for missing translations)
        }
    }

    public void Load()
    {
        if (Loaded) return;

        try
        {
            var asset = AssetManager.Instance.getAsset($"{LangFolder}/{Code}.json");
            if (asset == null)
                return;

            using JsonDocument doc = JsonDocument.Parse(asset.GetTextContent());
            Translations = new();
            FlattenJson(doc.RootElement);
        }
        catch (Exception ex)
        {
            s_logger.LogError(ex, $"Failed to load language file for {Code}", Code);
        }
    }

    private void FlattenJson(JsonElement element, string prefix = "")
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (var property in element.EnumerateObject())
                {
                    string key = string.IsNullOrEmpty(prefix)
                        ? property.Name
                        : $"{prefix}.{property.Name}";

                    FlattenJson(property.Value, key);
                }
                break;

            case JsonValueKind.String:
                Translations[prefix] = element.GetString() ?? string.Empty;
                break;

            case JsonValueKind.Number:
            case JsonValueKind.True:
            case JsonValueKind.False:
                Translations[prefix] = element.ToString();
                break;

            default:
                break;
        }
    }

    public string this[string key]
    {
        get
        {
            if (!Loaded)
                Load();

            if (Translations != null && Translations.TryGetValue(key, out var value))
                return value;

            s_logger.LogWarning($"Missing translation for key '{key}' in language '{Code}'");

            if (Code == DefaultLang)
                return key; // fallback to key if translation is missing
            else return global::BetaSharp.Translations.Instance.DefaultLanguage[key];
        }
    }
}
