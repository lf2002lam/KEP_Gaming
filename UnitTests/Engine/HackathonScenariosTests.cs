﻿using NUnit.Framework;

using Game.Engine;
using Game.Models;
using System.Threading.Tasks;
using Game.Helpers;
using System.Linq;
using Game.ViewModels;
using System.Collections.Generic;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        //AutoBattleEngine AutoBattleEngine;
        BattleEngine BattleEngine;

        [SetUp]
        public void Setup()
        {
            BattleEngine = EngineViewModel.Engine;
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void HakathonScenario_Constructor_0_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task HackathonScenario_Scenario_1_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new CharacterModel
                            {
                                SpeedAttribute = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperiencePoints = 1,
                                Name = "Mike",
                            };


            var playerList = new List<CharacterModel>();
            playerList.Add(CharacterPlayerMike);

            EngineViewModel.Engine.SetParty(playerList);

            EngineViewModel.Engine.SetAutoBattle(true);

            // Set Monster Conditions

            // Auto Battle will add the monsters


            //Act
            var result = EngineViewModel.Engine.startBattle();

            //Reset

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.Referee.Characters.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.Referee.BattleScore.RoundCount);
        }

        //[Test]
        //public void HackathonScenario_Scenario_2_Character_Bob_Should_Miss()
        //{
        //    /* 
        //     * Scenario Number:  
        //     *  2
        //     *  
        //     * Description: 
        //     *      Make a Character called Bob
        //     *      Bob Always Misses
        //     *      Other Characters Always Hit
        //     * 
        //     * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
        //     *      Change to Turn Engine
        //     *      Changed TurnAsAttack method
        //     *      Check for Name of Bob and return miss
        //     *                 
        //     * Test Algrorithm:
        //     *  Create Character named Bob
        //     *  Create Monster
        //     *  Call TurnAsAttack
        //     * 
        //     * Test Conditions:
        //     *  Test with Character of Named Bob
        //     *  Test with Character of any other name
        //     * 
        //     * Validation:
        //     *      Verify Enum is Miss
        //     *  
        //     */

        //    //Arrange

        //    // Set Character Conditions

        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var CharacterPlayer = new PlayerInfoModel(
        //                    new CharacterModel
        //                    {
        //                        Speed = 200,
        //                        Level = 10,
        //                        CurrentHealth = 100,
        //                        ExperienceTotal = 100,
        //                        ExperienceRemaining = 1,
        //                        Name = "Bob",
        //                    });

        //    BattleEngine.CharacterList.Add(CharacterPlayer);

        //    // Set Monster Conditions

        //    // Add a monster to attack
        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var MonsterPlayer = new PlayerInfoModel(
        //        new MonsterModel
        //        {
        //            Speed = 1,
        //            Level = 1,
        //            CurrentHealth = 1,
        //            ExperienceTotal = 1,
        //            ExperienceRemaining = 1,
        //            Name = "Monster",
        //        });

        //    BattleEngine.CharacterList.Add(MonsterPlayer);

        //    // Have dice rull 19
        //    DiceHelper.EnableForcedRolls();
        //    DiceHelper.SetForcedRollValue(19);

        //    //Act
        //    var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

        //    //Reset
        //    DiceHelper.DisableForcedRolls();

        //    //Assert
        //    Assert.AreEqual(true, result);
        //    Assert.AreEqual(HitStatusEnum.Miss, BattleEngine.BattleMessagesModel.HitStatus);
        //}

        //[Test]
        //public void HackathonScenario_Scenario_2_Character_Not_Bob_Should_Hit()
        //{
        //    /* 
        //     * Scenario Number:  
        //     *      2
        //     *      
        //     * Description: 
        //     *      See Default Test
        //     * 
        //     * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
        //     *      See Defualt Test
        //     *                 
        //     * Test Algrorithm:
        //     *      Create Character named Mike
        //     *      Create Monster
        //     *      Call TurnAsAttack so Mike can attack Monster
        //     * 
        //     * Test Conditions:
        //     *      Control Dice roll so natural hit
        //     *      Test with Character of not named Bob
        //     *  
        //     *  Validation
        //     *      Verify Enum is Hit
        //     *      
        //     */

        //    //Arrange

        //    // Set Character Conditions

        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var CharacterPlayer = new PlayerInfoModel(
        //                    new CharacterModel
        //                    {
        //                        Speed = 200,
        //                        Level = 10,
        //                        CurrentHealth = 100,
        //                        ExperienceTotal = 100,
        //                        ExperienceRemaining = 1,
        //                        Name = "Mike",
        //                    });

        //    BattleEngine.CharacterList.Add(CharacterPlayer);

        //    // Set Monster Conditions

        //    // Add a monster to attack
        //    BattleEngine.MaxNumberPartyCharacters = 1;

        //    var MonsterPlayer = new PlayerInfoModel(
        //        new MonsterModel
        //        {
        //            Speed = 1,
        //            Level = 1,
        //            CurrentHealth = 1,
        //            ExperienceTotal = 1,
        //            ExperienceRemaining = 1,
        //            Name = "Monster",
        //        });

        //    BattleEngine.CharacterList.Add(MonsterPlayer);

        //    // Have dice roll 20
        //    DiceHelper.EnableForcedRolls();
        //    DiceHelper.SetForcedRollValue(20);

        //    //Act
        //    var result = BattleEngine.TurnAsAttack(CharacterPlayer, MonsterPlayer);

        //    //Reset
        //    DiceHelper.DisableForcedRolls();

        //    //Assert
        //    Assert.AreEqual(true, result);
        //    Assert.AreEqual(HitStatusEnum.Hit, BattleEngine.BattleMessagesModel.HitStatus);
        //}
    }
}