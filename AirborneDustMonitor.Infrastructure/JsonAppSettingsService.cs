using AirborneDustMonitor.Core.Interfaces.Configuration;
using AirborneDustMonitor.Core.Settings;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Text.Json;

namespace AirborneDustMonitor.Infrastructure
{
    // Az alkalmazás beállításait JSON fájlban tárolja és biztosítja a beállítások betöltését és mentését.
    [SupportedOSPlatform("windows")]
    public class JsonAppSettingsService : IAppSettingsService
    {
        public AppSettings Current { get; private set; }
        private readonly string? _filePath;

        // Konstruktor amely megpróbálja betölteni a beállításokat a JSON fájlból. Ha a fájl nem létezik vagy sérült, akkor alapértelmezett értékeket állít be és létrehozza a fájlt.
        public JsonAppSettingsService()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AirborneDustMonitor");
            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, "settings.json");

            Current = LoadSettings();
        }

        private AppSettings LoadSettings()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    return ParseAndValidate(json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sérült konfiguráció: {ex.Message}. Alapértelmezett értékek visszaállítása.");
            }

            return ResetToDefaults();
        }

        // A JSON fájl tartalmát deszerializálja, majd újra szérializálja, hogy ellenőrizze a struktúra épségét. Ha a struktúra nem egyezik, akkor kivételt dob.
        private AppSettings ParseAndValidate(string json)
        {
            var deserialized = JsonSerializer.Deserialize<AppSettings>(json)
            ?? throw new InvalidDataException("Null result.");

            var reserializedJson = JsonSerializer.Serialize(deserialized, new JsonSerializerOptions { WriteIndented = true });
            var original = JsonDocument.Parse(json).RootElement;
            var reserialized = JsonDocument.Parse(reserializedJson).RootElement;

            if (!JsonElement.DeepEquals(original, reserialized))
                throw new InvalidDataException("Settings structure mismatch — resetting.");

            return deserialized;
        }

        private AppSettings ResetToDefaults()
        {
            Current = new AppSettings();
            Save();
            return Current;
        }

        // Menti a beállításokat a JSON fájlba. A jelszót titkosítva tárolja.
        public void Save()
        {
            try
            {

                var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions { WriteIndented = true });

                if (_filePath != null)
                    File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba a beállítások mentésekor: {ex.Message}");
            }
        }
    }
}
