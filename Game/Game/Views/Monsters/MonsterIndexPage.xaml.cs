﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
    public partial class MonsterIndexPage : ContentPage
    {

        // The view model, used for data binding
        readonly MonsterIndexViewModel ViewModel;

        /// <summary>
        /// Constructor for Index Page
        /// 
        /// Get the ItemIndexView Model
        /// </summary>
        public MonsterIndexPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = MonsterIndexViewModel.Instance;
        }

        /// <summary>
        /// The row selected from the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            MonsterModel data = args.SelectedItem as MonsterModel;
            if (data == null)
            {
                return;
            }

            // Open the Read Page
            await Navigation.PushAsync(new MonsterReadPage(new GenericViewModel<MonsterModel>(data)));

            // Manually deselect item.
            MonsterListView.SelectedItem = null;

        }

        /// <summary>
        /// Call to Add a new record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void AddMonster_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new MonsterCreatePage(new GenericViewModel<MonsterModel>())));
        }

        /// <summary>
        /// Refresh the list on page appearing
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            // If no data, then set it for needing refresh
            if (ViewModel.Dataset.Count == 0)
            {
                ViewModel.SetNeedsRefresh(true);
            }

            // If the needs Refresh flag is set update it
            if (ViewModel.NeedsRefresh())
            {
                ViewModel.LoadDatasetCommand.Execute(null);
            }

            BindingContext = ViewModel;
        }
    }
}