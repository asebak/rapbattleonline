using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FreestyleOnline.classes.Base;
using FreestyleOnline.classes.Providers;

namespace FreestyleOnline.classes.Secruity
{
    public class WrittenBattleValidation: RapClass
    {
        /// <summary>
        /// This will fix a content submitted if the user disables js for secruity reasons
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="totalBars">The total bars.</param>
        /// <returns></returns>
        public string RebuildWrittenVerse(string content, int totalBars)
        {
            var verse = content;
            var verseSentences = verse.Split('\n');
            var barLength =
                Convert.ToInt32(
                    this.GetService<ApplicationProvider>().GetApplicationSettings("RAP.WrittenBattleBarLength"));
            Array.Resize(ref verseSentences, totalBars);
            var newVerse = "";
            for (var i = 0; i < verseSentences.Length; i++)
            {
                verseSentences[i] = this.Truncate(verseSentences[i], barLength);
                newVerse += verseSentences[i] + "\n";
            }
            return newVerse;
        }

        /// <summary>
        ///     Replaces the last occurence of a value in a string .
        /// </summary>
        /// <param name="originalValue">The original value.</param>
        /// <param name="occurenceValue">The occurence value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        private string ReplaceLastOccurence(string originalValue, string occurenceValue, string newValue)
        {
            int startindex = originalValue.LastIndexOf(occurenceValue);
            return originalValue.Remove(startindex, occurenceValue.Length).Insert(startindex, newValue);
        }

        /// <summary>
        ///     Truncates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
        private string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }
}