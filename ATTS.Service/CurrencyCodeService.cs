using ATTS.Contracts.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ATTS.Service
{
    public sealed class CurrencyCodeService : ICurrencyCodeService
    {
        private HashSet<string> _cache;

        public CurrencyCodeService()
        {
            this.Init();
        }

        private void Init()
        {
            var allCurrencySymbols = GetCurrencySymbols();

            _cache = new HashSet<string>(allCurrencySymbols);
        }

        public static IEnumerable<string> GetCurrencySymbols()
        {
            var regionInfos = from culture in CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures)
                              where culture.Name.Length > 0 && !culture.IsNeutralCulture
                              let region = new RegionInfo(culture.LCID)
                              select region.ISOCurrencySymbol;

            return regionInfos;
        }

        public bool IsCurrencyCodeValid(string currencyCode)
        {
            return _cache.Contains(currencyCode);
        }
    }
}