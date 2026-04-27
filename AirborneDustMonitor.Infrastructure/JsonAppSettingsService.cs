using AirborneDustMonitor.Core.Interfaces;
using AirborneDustMonitor.Core.Settings;
using System.Diagnostics;
using System.Text.Json;

namespace AirborneDustMonitor.Infrastructure
{
    // Az alkalmazás beállításait JSON fájlban tárolja és biztosítja a beállítások betöltését és mentését.
    public class JsonAppSettingsService : IAppSettingsService
    {
        public AppSettings Current { get; private set; }
        private readonly string? _filePath;

        // Konstruktor amely megpróbálja betölteni a beállításokat a JSON fájlból. Ha a fájl nem létezik vagy sérült, akkor alapértelmezett értékeket állít be és létrehozza a fájlt.
        public JsonAppSettingsService()
        {
            try
            {
                var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "AirborneDustMonitor");
                Directory.CreateDirectory(folder);
                _filePath = Path.Combine(folder, "settings.json");

                if (File.Exists(_filePath))
                {
                    var json = File.ReadAllText(_filePath);
                    Current = JsonSerializer.Deserialize<AppSettings>(json) ?? new AppSettings();

                    if (DataProtectionHelper.TryUnprotect(Current.Email.Password, out var unprotectedPassword))
                    {
                        Current.Email.Password = unprotectedPassword!;
                    }
                }
                else
                {
                    Current = new AppSettings();
                    Save();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Sérült konfiguráció: {ex.Message}. Alapértelmezett értékek visszaállítása.");
                Current = new AppSettings();
                Save();
            }


        }

        // Menti a beállításokat a JSON fájlba. A jelszót titkosítva tárolja.
        public void Save()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Current.Email.Password))
                {
                    Current.Email.Password = DataProtectionHelper.Protect(Current.Email.Password);
                }

                var json = JsonSerializer.Serialize(Current, new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                if (_filePath != null)
                {
                    File.WriteAllText(_filePath, json);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Hiba a beállítások mentésekor: {ex.Message}");
            }
        }
    }
}
