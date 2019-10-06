using System.Collections.Generic;
using System.Linq;
using CalculationTools.Core.Enums;

namespace CalculationTools.Core
{
    /// <summary>
    /// This class is meant to compare the real result from in-game with the result from the battle simulator
    /// </summary>
    public class RealBattleResult
    {
        #region Properties

        public int AtkBattleModifier { get; set; }

        #region Attacking

        public UnitSet AtkUnits { get; set; }
        public UnitSet AtkUnitsLost { get; set; }
        public UnitSet AtkUnitsLeft { get; set; }

        #endregion Attacking

        #region Defending

        public UnitSet DefUnits { get; set; }

        public UnitSet DefUnitsLost;
        public UnitSet DefUnitsLeft { get; set; }

        #endregion Defending

        /// <summary>
        /// The defense modifier before the battle has started
        /// </summary>
        public int DefModifierBeforeBattle { get; set; }

        /// <summary>
        /// The defense modifer after the pre-round, where the rams/cats have damaged the wall.
        /// </summary>
        public int DefModifierDuringBattle { get; set; }
        public int WallLevelBefore { get; set; }
        public int WallLevelAfter { get; set; }
        public int WallLevelFinal { get; set; }

        #endregion Properties
    }
}