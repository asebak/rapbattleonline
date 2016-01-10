#region Using

using System;
using FreestyleOnline.classes.Base;

#endregion

namespace FreestyleOnline.classes.Types.Helpers
{
    public class RapTournamentHelpers : RapClass
    {
        #region Methods

        /// <summary>
        ///     Calculates the total rounds.
        /// </summary>
        /// <param name="totalChallengers">The total challengers.</param>
        /// <returns></returns>
        public int CalculateTotalRounds(int totalChallengers)
        {
            return (int) (Math.Log10(totalChallengers)/Math.Log10(2));
        }

        /// <summary>
        ///     Finds the number of matches per round
        /// </summary>
        /// <param name="totalChallengers">The total challengers.</param>
        /// <param name="currentRound">The current round.</param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public Double RapMatchesPerRound(int totalChallengers, int currentRound)
        {
            return (totalChallengers/Math.Pow(2, currentRound))/2;
        }

        /// <summary>
        ///     Finds the Next Match Position
        /// </summary>
        /// <param name="totalChallengers">The total challengers.</param>
        /// <param name="currentMatchId">The current match identifier.</param>
        /// <returns></returns>
        public int NextRapMatchUp(int totalChallengers, int currentMatchId)
        {
            return (totalChallengers/2) + (int) Math.Ceiling((double) currentMatchId/2);
        }

        #endregion
    }
}