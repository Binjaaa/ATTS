using System;

namespace ATTS.Model.Attributes
{
    /// <summary>
    /// Attribute to skip property to match database schema when converting an IEnumerable<T> to DataTable.
    /// </summary>
    public class SkipPropertyAttribute : Attribute
    {
    }
}