using StudentPortal.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPortal.Core.Extensions
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// In the database, application details are stored in lookup tables. For example, an applicants gender may be stored as '2',
        /// but that really refers to the gender in the lookup table with an ID of 2, which would be 'Female'
        /// 
        /// In application summaries and reports, users want to see the data as the looked up value, not the identifier,
        /// so this extension method will lookup the selected value in a given list of selectable items and return the looked up name.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        public static string ConvertSelectedValueToString(this List<ISelectable> items, string selectedValue)
        {
            var item = items.FirstOrDefault(i => i.Id.ToString() == selectedValue);
            if (item != null)
            {
                return item.Name;
            }

            return string.Empty;
        }

    }
}
