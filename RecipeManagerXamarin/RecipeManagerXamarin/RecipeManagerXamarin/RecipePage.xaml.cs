using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecipeManagerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipePage : ContentPage
    {
        #region PROPERTIES
        private Recipe recipe; // Stores the recipe being displayed.
        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Constructor for the RecipePage class.
        /// </summary>
        /// <param name="recipe">Recipe</param>
        public RecipePage(Recipe recipe)
        {
            InitializeComponent();

            this.recipe = recipe;

            this.Title = recipe.Name;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemDeleteRecipe.Clicked += ToolbarItemDeleteRecipe_Clicked;
        }


        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Clicked listner for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ToolbarItemDeleteRecipe_Clicked(object sender, EventArgs e)
        {
            // Gets the answer from the alert.
            bool answer = await DisplayAlert("Delete Recipe", "Are you sure you want to delete this recipe?", "Yes", "No");

            // Deletes the recipe if the user chooses "yes".
            if (answer)
            {
                if (App.Database.DeleteRecipe(recipe) == 1)
                {
                    // Removes the page from the navigation stack.
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Error deleting recipe", "OK");
                }
            }
        }
        #endregion
    }
}