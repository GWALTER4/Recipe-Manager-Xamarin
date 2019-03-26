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
        private int categoryID;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor for the AddRecipePage class.
        /// </summary>
        public AddRecipePage (int categoryID)
		{
            BindingContext = this;

			InitializeComponent ();
            
            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            // Sets the category ID.
            this.categoryID = categoryID;

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
        private async void ToolbarItemRecipeDone_Clicked(object sender, EventArgs e)
        {
            // Gets the recipe name from the entry.
            string recipeName = EntryRecipeName.Text;

            // Initializes the duration.
            int duration = 0;

            bool invalid = false;

            // Checks that the duration is not emtpy and converts it to an int.
            if (!string.IsNullOrEmpty(EntryDuration.Text))
            {
                duration = Int32.Parse(EntryDuration.Text);
            }
            else
            {
                invalid = true;
            }
            
            // Checks that the recipe is valid.
            if(!string.IsNullOrEmpty(recipeName) && InputCheck.GetInputCheckInstance.IsRecipeValid(recipeName, ingredientList.Length, instructionList.Length, duration))
            {
                // Creates a string with all the ingredients from the list.
                string ingredientListString = InputCheck.GetInputCheckInstance.CreateIngredientListString(ingredientList);

                // Creates a new recipe object.
                Recipe newRecipe = new Recipe() { CategoryID = categoryID, Name = recipeName, IngredientsList = ingredientListString,
                    InstructionCount = instructionList.Length, TotalDuration = duration };

                // Inserts the recipe into the database.
                if(App.Database.InsertRecipe(newRecipe, instructionList) == 1)
                {
                    DisplayAlert("Done", "Recipe added", "OK");

                    // Removes the page from the navigation stack.
                    await Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Error", "Error", "OK");
                }


            }
            else
            {
                invalid = true;
            }

            if (invalid)
            {
                DisplayAlert("Error", "Invalid recipe", "OK");
            }
        }
        #endregion

    }
}