﻿using Game.Models;
using Game.ViewModels;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Game.Views
{
    /// <summary>
    /// Create Item
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ScoreCreatePage : ContentPage
    {
        // The item to create
        GenericViewModel<ScoreModel> ViewModel { get; set; }

        /// <summary>
        /// Constructor for Create makes a new model
        /// </summary>
        public ScoreCreatePage(GenericViewModel<ScoreModel> data)
        {
            InitializeComponent();

            data.Data = new ScoreModel();

            BindingContext = this.ViewModel = data;

            this.ViewModel.Title = "Create";
        }

        /// <summary>
        /// Save by calling for Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Save_Clicked(object sender, EventArgs e)
        {
            // If the image in the data box is empty, use the default one..
            if (string.IsNullOrEmpty(ViewModel.Data.ImageURI))
            {
                ViewModel.Data.ImageURI = Services.ItemService.DefaultImageURI;
            }

            MessagingCenter.Send(this, "Create", ViewModel.Data);
            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Cancel the Create
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        // <summary>
        /// Catch the change to the Stepper for RoundCount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void RoundCount_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            RoundValue.Text = String.Format("{0}", e.NewValue);
        }

        // <summary>
        /// Catch the change to the Stepper for TurnCount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TurnCount_OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            TurnValue.Text = String.Format("{0}", e.NewValue);
        }
    }
}