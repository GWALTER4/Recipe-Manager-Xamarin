using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeManagerXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddRecipePage : ContentPage
	{
        #region PROPERTIES
        private Ingredient[] ingredientList; // List of ingredients.
        private Instruction[] instructionList; // List of instructions.
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor for the AddRecipePage class.
        /// </summary>
        public AddRecipePage ()
		{
            BindingContext = this;

			InitializeComponent ();
            
            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            // Sets the clicked listener for the image button.
            ImageButtonAddIngredients.Clicked += ImageButtonAddIngredients_Clicked;

            // Sets the clicked listener for the image button.
            ImageButtonAddInstructions.Clicked += ImageButtonAddInstructions_Clicked;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemRecipeDone.Clicked += ToolbarItemRecipeDone_Clicked;           

            // Sets the page to listen for a message.
            MessagingCenter.Subscribe<AddIngredientPage, Ingredient[]>(this, "AddIngredients", (sender, arg) => {
                ingredientList = arg;

                LabelIngredientsAdded.IsVisible = true;

                LabelIngredientsAdded.Text = ingredientList.Count().ToString() + " Ingredients Added";
            });

            // Sets the page to listen for a message.
            MessagingCenter.Subscribe<AddInstructionPage, Instruction[]>(this, "AddInstructions", (sender, arg) => {
                instructionList = arg;

                LabelInstructionsAdded.IsVisible = true;

                LabelInstructionsAdded.Text = instructionList.Count().ToString() + " Instructions Added";
            });
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Event handler for when the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Clicked listener for the image button.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ImageButtonAddIngredients_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddIngredientPage());
        }

        /// <summary>
        /// Clicked listener for the image button.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ImageButtonAddInstructions_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddInstructionPage());
        }

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private void ToolbarItemRecipeDone_Clicked(object sender, EventArgs e)
        {
            
        }
        #endregion

    }
}