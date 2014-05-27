using System;
using System.Data;

namespace Coating
{
    public static class DataRecordExtensions
    {
        public static string GetString(this IDataRecord record, string fieldName)
        {
            if (record == null)
            {
                return null;
            }

            return Convert.ToString(record[fieldName]);
        }
    }
}