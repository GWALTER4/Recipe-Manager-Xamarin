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
        private List<Instruction> instructionList; // Stores a list of instructions for the recipe.
        private ImageManager imageManager; // Stores an image manager object.
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

            // Initialzies the image manager object.
            imageManager = new ImageManager();

            // Sets the title of the page.
            Title = recipe.Name;

            // Gets the instructions for the recipe from the database.
            instructionList = App.Database.GetInstructions(recipe);

            // Displays the isntructions for the recipe.
            DisplayInstructions();

            // Sets the text for the ingredients list label.
            LabelIngredientsList.Text = FormatIngredientsList(recipe.IngredientsList);

            // Sets the text for the duration label.
            LabelDuration.Text = LabelDuration.Text + " " + recipe.TotalDuration.ToString();

            // Sets the clicked listener for the toolbar item.
            ToolbarItemDeleteRecipe.Clicked += ToolbarItemDeleteRecipe_Clicked;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemEditPhoto.Clicked += ToolbarItemEditPhoto_Clicked;
        }
        #endregion

        #region METHODS

        /// <summary>
        /// Formats the ingredients list.
        /// </summary>
        /// <param name="ingredientsList">Ingredients list</param>
        /// <returns>Formatted ingredients list</returns>
        public string FormatIngredientsList(string ingredientsList)
        {
            // Replaces all commas with new line characters.
            string formattedIngredientsList = ingredientsList.Replace(",", Environment.NewLine);

            return formattedIngredientsList;
        }

        /// <summary>
        /// Displays the instructions for the recipe.
        /// </summary>
        public void DisplayInstructions()
        {
            // Initializes the instruction counter.
            int instructionCounter = 1;

            // Iterates through all the instructions in the instruction list.
            foreach(Instruction instruction in instructionList)
            {
                // Creates a label with the instruction description.
                var instructionLabel = new Label
                {
                    Text = instructionCounter.ToString() + ". " + instruction.Description,
                    LineBreakMode = LineBreakMode.WordWrap,
                    Margin = new Thickness(16, 0, 0, 0),
                    FontSize = 16
                };

                // Creats a frame for the label.
                Frame instructionFrame = new Frame
                {
                    Content = instructionLabel,
                    BorderColor = Color.FromHex("#D81B60"),
                    Margin = new Thickness(16, 0, 0, 16),
                };

                // Adds the label to the stack layout.
                StackLayoutRecipePage.Children.Add(instructionFrame);

                instructionCounter++;
            }
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

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ToolbarItemEditPhoto_Clicked(object sender, EventArgs e)
        {
            // Takes a photo.
            int takePhotoResult = await imageManager.TakePhotoAsync();

            if(takePhotoResult == -1)
            {
                await DisplayAlert("Error", "Error taking photo", "OK");
            }

            // Updates the recipe image file path.
            int updateRecipeResult = imageManager.AddPhotoToRecipe(recipe);

            if(updateRecipeResult == -1)
            {
                await DisplayAlert("Error", "Error updating recipe", "OK");
            }
            else
            {
                await DisplayAlert("Done", "Photo added", "OK");
            }
        }
        #endregion
    }
}