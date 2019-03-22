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
	public partial class CategoryPage : ContentPage
	{
        #region PROPERTIES
        private int categoryID; // Category ID for the category being displayed.
        #endregion

        #region CONSTRUCTORS
        public CategoryPage (int categoryID)
		{
			InitializeComponent();

            this.categoryID = categoryID;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemDeleteCategory.Clicked += ToolbarItemDeleteCategory_Clicked;
		}
        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ToolbarItemDeleteCategory_Clicked(object sender, EventArgs e)
        {
            // Gets the answer from the alert.
            bool answer = await DisplayAlert("Delete Category", "Are you sure you want to delete this category?", "Yes", "No");

            // Deletes the category if the user chooses "yes".
            if(answer)
            {
                if(App.Database.DeleteCategory(categoryID) == 1)
                {
                    // Removes the page from the navigation stack.
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Error deleting category", "OK");
                }
            }
        }
        #endregion
    }
}