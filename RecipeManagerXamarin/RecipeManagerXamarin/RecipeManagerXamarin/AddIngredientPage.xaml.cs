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
	public partial class AddIngredientPage : ContentPage
	{
        #region PROPERTIES
        private ObservableCollection<Ingredient> ingredientList; // List of ingredients.
        #endregion

        #region CONSTRUCTORS
        public AddIngredientPage ()
		{
			InitializeComponent();

            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            //Initializes the ingredient list.
            ingredientList = new ObservableCollection<Ingredient>();

            // Sets the items source of the list view.
            ListViewIngredients.ItemsSource = ingredientList;

            // Sets the item selected listener for the list view.
            ListViewIngredients.ItemSelected += ListViewIngredients_ItemSelected;

            // Sets the item tapped listener for the list view.
            ListViewIngredients.ItemTapped += ListViewIngredients_ItemTapped;

            // Sets the clicked listener for the image button.
            ImageButtonAddIngredient.Clicked += ImageButtonAddIngredient_Clicked;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemIngredientDone.Clicked += ToolbarItemIngredientDone_Clicked;
        }
        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Item selected listener for the list view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private void ListViewIngredients_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Sets the item to -1 so highlighting is disabled.
            ListViewIngredients.SelectedItem = -1;
        }

        /// <summary>
        /// Tapped listener for the list view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ListViewIngredients_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("Delete", "Are you sure you want to delete this ingredient?", "Yes", "No");

            if (answer)
            {
                // Removes the ingredient from the list.
                ingredientList.RemoveAt(e.ItemIndex);
            }
        }

        /// <summary>
        /// Clicked listener for the image button.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ImageButtonAddIngredient_Clicked(object sender, EventArgs e)
        {
            // Gets the string from the entry.
            string ingredientName = EntryIngredientName.Text;
            
            // Checks that the ingredient name only contains valid characters.
            if (!string.IsNullOrEmpty(ingredientName) && InputCheck.GetInputCheckInstance.IsCategoryNameValid(ingredientName))
            {
                ingredientList.Add(new Ingredient(ingredientName));
                EntryIngredientName.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Ingredient name invalid.", "OK");
            }
        }

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ToolbarItemIngredientDone_Clicked(object sender, EventArgs e)
        {
            // Creates an array to store the ingredients list.
            Ingredient[] ingredientListArray = new Ingredient[ingredientList.Count()];

            // Copies the ingredients to an array.
            ingredientList.CopyTo(ingredientListArray, 0);

            bool answer = await DisplayAlert("Add ingredients", "Are you sure you want to add these ingredients?", "Yes", "No");

            if (answer)
            {
                // Sends a message with the ingredients list.
                MessagingCenter.Send(this, "AddIngredients", ingredientListArray);

                // Removes the page from the navigation stack.
                await Navigation.PopAsync();
            }          
        }
        #endregion
    }
}