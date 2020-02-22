﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Models
{
    public class DungeonFighterModel : CreatureModel<DungeonFighterModel>
    {

        /// <summary>
        /// For heroes, the set of equipment that is currently equipped
        /// </summary>
        private HashSet<ItemModel> Equipment;
        
        /// <summary>
        /// For mobs, the list of items that are able to be dropped
        /// </summary>
        private List<ItemModel> DropItems;
        
        /// <summary>
        /// Whether the Player is alive or dead
        /// </summary>
        public bool Alive { get; set; } = true;
        
        /// <summary>
        /// Keep track of the turn order
        /// </summary>
        public int ListOrder = 0;

        public CreatureEnum PlayerType { get; set; } = CreatureEnum.Unknown;

        /// <summary>
        /// Current health of the character
        /// </summary>
        public int CurrentHealth { get; set; } = 0;
        

        /// <summary>
        /// Empty constructor
        /// </summary>
        public DungeonFighterModel()
        {

        }

        /// <summary>
        /// Constructor based on CharacterModel
        /// </summary>
        /// <param name="character"></param>
        public DungeonFighterModel(CharacterModel character)
        {
            ImageURI = character.ImageURI;
            
            Id = character.Id;
            Name = character.Name;
            Description = character.Description;
            
            Level = character.Level;
            MaxHealth = character.MaxHealth;
            SpeedAttribute = character.SpeedAttribute;
            OffenseAttribute = character.OffenseAttribute;
            DefenseAttribute = character.DefenseAttribute;
            Skill = character.Skill;
            
            Equipment = character.Equipment;
            CurrentHealth = MaxHealth;
            PlayerType = CreatureEnum.Character;
        }

        /// <summary>
        /// Constructor based on MonsterModel
        /// </summary>
        /// <param name="character"></param>
        public DungeonFighterModel(MonsterModel monster)
        {
            ImageURI = monster.ImageURI;

            Id = monster.Id;
            Name = monster.Name;
            Description = monster.Description;

            Level = monster.Level;
            MaxHealth = monster.MaxHealth;
            SpeedAttribute = monster.SpeedAttribute;
            OffenseAttribute = monster.OffenseAttribute;
            DefenseAttribute = monster.DefenseAttribute;
            Skill = monster.Skill;

            DropItems = monster.DropItems;
            CurrentHealth = MaxHealth;
            PlayerType = CreatureEnum.Monster;
        }
    }
}