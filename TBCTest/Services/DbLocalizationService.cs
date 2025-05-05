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

            // worst case fallback
            return $"[{key}]";
        }
    }
}
