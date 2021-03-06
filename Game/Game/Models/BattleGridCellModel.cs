﻿using System;
using System.Collections.Generic;
using System.Text;
using Game.Models.Enum;

namespace Game.Models
{
    /// <summary>
    /// Holds information relevant to a single cell in the BattleGrid, such as players
    /// </summary>
    public class BattleGridCellModel
    {

        // Index for a tilemap, showing the image
        public int Tile { get; set; } = -1;
        
        // Whether the cell is occupied with a character or monster, or empty
        private BattleGridEnum CellStatus { get; set; } = BattleGridEnum.Empty;

        // Fighter occupying the cell, if any
        public DungeonFighterModel Player { get; set; } = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public BattleGridCellModel()
        {

        }

        /// <summary>
        /// Check if a cell contains a fighter
        /// </summary>
        /// <returns>True if empty, false otherwise</returns>
        public bool IsEmpty()
        {
            return CellStatus == BattleGridEnum.Empty;
        }

        public bool HasCharacter()
        {
            return CellStatus == BattleGridEnum.HasCharacter;
        }

        public bool HasMonster()
        {
            return CellStatus == BattleGridEnum.HasMonster;
        }

        public bool PlayerDied()
        {
            // Set tile to dead icon, whatever that turns out to be
            Tile = 1;
            // Need to refresh view afterwords

            return true;
        }

        public DungeonFighterModel GetPlayer()
        {
            if (CellStatus == BattleGridEnum.Empty)
            {
                return null;
            }

            return Player;

        }
        
        public bool SetPlayer(DungeonFighterModel player)
        {
            if (CellStatus != BattleGridEnum.Empty)
            {
                return false;
            }

            Player = player;
            return true;
        } 
        
    }
}
