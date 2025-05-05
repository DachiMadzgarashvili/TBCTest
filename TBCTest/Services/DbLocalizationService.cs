using System.Globalization;
using TBCTest.Data;
using TBCTest.LocalizationSupport;
using TBCTest.Models;

namespace TBCTest.Services
{
    public class DbLocalizationService : IDbLocalizationService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbLocalizationService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public string Get(string key)
        {
            var lang = _httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString()
                       ?? CultureInfo.CurrentUICulture.Name
                       ?? "en-US";

            var entry = _context.Localizations
                .FirstOrDefault(l => l.Language == lang && l.Key == key);

            if (entry != null)
                return entry.Value;

            // fallback to en-US
            var fallback = _context.Localizations
                .FirstOrDefault(l => l.Language == "en-US" && l.Key == key);

            if (fallback != null)
                return fallback.Value;

            // insert default if missing
            if (AppMessages.Defaults.TryGetValue(key, out var defaultValue))
            {
                _context.Localizations.Add(new Localization
                {
                    Key = key,
                    Language = lang,
                    Value = defaultValue
                });
                _context.SaveChanges();
                return defaultValue;
            }

            // worst case fallback
            return $"[{key}]";
        }
    }
}
