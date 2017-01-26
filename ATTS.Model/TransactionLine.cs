using ATTS.Model.Attributes;

namespace ATTS.Model
{
    /// <summary>
    /// Represents a transaction line.
    /// </summary>
    public struct TransactionLine
    {
        public string Account { get; set; }
        public string Description { get; set; }
        public string CurrencyCode { get; set; }
        public decimal? Value { get; set; }

        [SkipPropertyAttribute]
        public int LineNumber { get; set; }
    }
}