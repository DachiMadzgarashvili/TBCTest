using TBCTest.Data;
using TBCTest.LocalizationSupport;
using TBCTest.Models;

namespace TBCTest.Services
{
    public static class LocalizationSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            var supportedLanguages = new[] { "en-US", "ka-GE" };

            foreach (var language in supportedLanguages)
            {
                foreach (var kvp in AppMessages.Defaults)
                {
                    bool exists = context.Localizations.Any(l => l.Key == kvp.Key && l.Language == language);
                    if (!exists)
                    {
                        string value = language == "ka-GE"
                            ? $"[translate] {kvp.Value}"
                            : kvp.Value;

                        context.Localizations.Add(new Localization
                        {
                            Key = kvp.Key,
                            Language = language,
                            Value = value
                        });
                    }
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
