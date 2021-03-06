﻿using System;
using System.ComponentModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;
using System.Linq;


namespace Game.Views
{
    /// <summary>
    /// Selecting Characters for the Game
    /// 
    /// TODO: Team
    /// Mike's game allows duplicate characters in a party (6 Mikes can all fight)
    /// If you do not allow duplicates, change the code below
    /// Instead of using the database list directly make a copy of it in the viewmodel
    /// Then have on select of the database remove the character from that list and add to the part list
    /// Have select from the party list remove it from the party list and add it to the database list
    ///
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0019:Use pattern matching", Justification = "<Pending>")]
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickCharactersPage : ContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        //maximum and minimum character for play
        private const int MinCharCount = 1;
        private const int MaxCharCount = 6;
        private int currentCount = 0;

        // Empty Constructor for UTs
        public PickCharactersPage(bool UnitTest) { }

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the CharacterIndexView Model
        /// </summary>
        public PickCharactersPage()
        {
            InitializeComponent();

            BindingContext = EngineViewModel;

            // Clear the Database List and the Party List to start
            EngineViewModel.PartyCharacterList.Clear();

            //preset false
            ConfigureBattleButtons();


        }


        /// <summary>
        /// operation to check or uncheck box to pick character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void checkbox_CheckChanged(object sender, EventArgs e)
        {
            var checkbox = (Plugin.InputKit.Shared.Controls.CheckBox)sender;
            var ob = checkbox.BindingContext as CharacterModel;
            Console.WriteLine(ob.Name);
            Console.WriteLine(ob.Description);
            if (ob == null)
            {
                return;
            }


            //checked character
            if (checkbox.IsChecked)
            {
                EngineViewModel.PartyCharacterList.Add(ob);
                currentCount++;
            }

            //uncheck character
            else
            {
                bool uncheck = false;
                while (!uncheck)
                {
                    for (int i = 0; i < EngineViewModel.PartyCharacterList.Count; i++)
                    {
                        if (EngineViewModel.PartyCharacterList[i].Name == ob.Name)
                        {
                            EngineViewModel.PartyCharacterList.RemoveAt(i);
                            currentCount--;
                            uncheck = true;
                        }
                    }
                   
                }
                
            }
            ConfigureBattleButtons();


        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnPartyCharacterItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            CharacterModel data = args.SelectedItem as CharacterModel;
            if (data == null)
            {
                return;
            }

            // Remove the character from the list
            EngineViewModel.PartyCharacterList.Remove(data);

            
        }

        

        /// <summary>
        /// Jump to the Battle
        /// 
        /// Its Modal because don't want user to come back...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BattleButton_Clicked(object sender, EventArgs e)
        {
            // 
            CreateEngineCharacterList();

            await Navigation.PushModalAsync(new NavigationPage(new BattlePage()));
            await Navigation.PopAsync();
        }

        public async void AutoBattleButton_Clicked(object sender, EventArgs e)
        {
            // 
            CreateEngineCharacterList();
            EngineViewModel.Engine.Referee.AutoBattleEnabled = true;
            EngineViewModel.Engine.startBattle();



            await Navigation.PushModalAsync(new NavigationPage(new ScorePage()));
            await Navigation.PopAsync();
        }



        /// <summary>
        /// Clear out the old list and make the new list
        /// </summary>
        public void CreateEngineCharacterList()
        {
            // Clear the current list
            EngineViewModel.Engine.PickedCharacters.Clear();

            // Load the Characters into the Engine
            foreach (var data in EngineViewModel.PartyCharacterList)
            {
                EngineViewModel.Engine.PickedCharacters.Add(data);
            }
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Clear the Binding
            BindingContext = null;

            EngineViewModel.PartyCharacterList.Clear();

            // Force the Binding to Update
            BindingContext = EngineViewModel;

        }


        /// <summary>
        /// Enable/Disable BattleStartButton if characters less than minimum or over maximum
        /// </summary>
        private void ConfigureBattleButtons()
        {
            if(currentCount > MaxCharCount || 
                currentCount < MinCharCount)
            {
                BeginBattleButton.IsEnabled = false;
                BeginAutoBattleButton.IsEnabled = false;
                ErrorMessage.IsVisible = true;
            }
            else
            {
                BeginBattleButton.IsEnabled = true;
                BeginAutoBattleButton.IsEnabled = true;
                ErrorMessage.IsVisible = false;
            }

        }
    }
}