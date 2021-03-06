﻿using Game.Models;
using Game.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScorePage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();

            BindingContext = EngineViewModel;

            DrawOutput();
        }

        /// <summary>
        /// Draw data for
        /// Character
        /// Monster
        /// Item
        /// </summary>
        public void DrawOutput()
        {
            ConfigurePage();

            // Highest round achieved
            RoundNumber.Text = EngineViewModel.Engine.CurrentRound.RoundCount.ToString();
            // Monsters killed
            TotalKilled.Text = EngineViewModel.Engine.Referee.BattleScore.MonsterSlainNumber.ToString();
            // Turns taken
            TotalTurns.Text = EngineViewModel.Engine.Referee.BattleScore.TurnCount.ToString();
            // EXP
            TotalExperience.Text = EngineViewModel.Engine.Referee.BattleScore.ExperienceGainedTotal.ToString();

            // Draw the Monsters
            foreach (var data in EngineViewModel.Engine.Referee.Monsters)
            {
                MonsterListFrame.Children.Add(CreateMonsterDisplayBox(data));
            }

            // Draw the Items
            foreach (var data in EngineViewModel.Engine.Referee.BattleScore.ItemModelDropList)
            {
                ItemListFrame.Children.Add(CreateItemDisplayBox(data));
            }

            // Draw the Dead Characters
            foreach (var data in EngineViewModel.Engine.Referee.BattleScore.CharacterModelDeathList)
            {
                DeadCharacterListFrame.Children.Add(CreateCharacterDisplayBox(data));
            }

            foreach (var data in EngineViewModel.Engine.Referee.Characters)
            {
                AliveCharacterListFrame.Children.Add(CreateCharacterDisplayBox(data));
            }

            if(EngineViewModel.Engine.Referee.BattleScore.CharacterModelDeathList.Count > 0)
            {
                DeadCharacterListFrame.IsVisible = true;
                DeadCharacterLabel.IsVisible = true;
            }
            
            if(EngineViewModel.Engine.Referee.Characters.Count <= 0)
            {
                AliveCharacterLabel.IsVisible = false;
                AliveCharacterListFrame.IsVisible = false;
            }

        }

        /// <summary>
        /// Return a stack layout for the Characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateCharacterDisplayBox(DungeonFighterModel data)
        {
            if (data == null)
            {
                data = new DungeonFighterModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleMediumStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel_1 = new Label
            {
                Text = "Lv: " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleSmall"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var PlayerLevelLabel_2 = new Label
            {
                Text = "HP: " + data.CurrentHealth + "/" + data.MaxHealth,
                Style = (Style)Application.Current.Resources["ValueStyleSmall"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreCharacterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerLevelLabel_1,
                    PlayerLevelLabel_2
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout for the Monsters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateMonsterDisplayBox(DungeonFighterModel data)
        {
            if (data == null)
            {
                data = new DungeonFighterModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Lv: " + data.Level,
                Style = (Style)Application.Current.Resources["ValueStyleSmall"],
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreMonsterInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                    PlayerLevelLabel,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreateItemDisplayBox(ItemModel data)
        {
            if (data == null)
            {
                data = new ItemModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                Style = (Style)Application.Current.Resources["ScoreItemInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children = {
                    PlayerImage,
                },
            };

            return PlayerStack;
        }

        /// <summary>
        /// Exit battle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void CloseButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Battle", "Are you sure you want to Quit?", "Yes", "No");

			if (answer)
			{
                // create new score record
                MessagingCenter.Send(this, "Create", EngineViewModel.Engine.Referee.BattleScore);

                //reset battle engine
                EngineViewModel.Engine = new Engine.BattleEngine();

                await Navigation.PushModalAsync(new MainPage());
            }
        }

        /// <summary>
        /// ContinueNextRound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Show ScorePage header message
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ConfigurePage()
        {
            if(EngineViewModel.Engine.Referee.Characters.Count() > 0)
            {
                GameOverOrNextRound.Text = "Congrulations!";
                MonsterBoxDisplayMessage.Text = "The Next Round:";
                NextRoundButton.IsVisible = true;
            }
            else
            {
                GameOverOrNextRound.Text = "Game Over!";
                MonsterBoxDisplayMessage.Text = "Monster remains in this round:";
                NextRoundButton.IsVisible = false;

            }
        }
    }
}