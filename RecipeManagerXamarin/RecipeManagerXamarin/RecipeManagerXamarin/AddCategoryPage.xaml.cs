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
	public partial class AddCategoryPage : ContentPage
	{
        #region CONSTRUCTORS
        /// <summary>
        /// Constructor for the AddCategoryPage class.
        /// </summary>
        public AddCategoryPage()
		{
			InitializeComponent ();

            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            // Sets the clicked listener for the toolbar item.
            ToolbarItemCategoryDone.Clicked += ToolbarItemCategoryDone_Clicked;
		}
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        async void ToolbarItemCategoryDone_Clicked(object sender, EventArgs e)
        {          
            // Gets the string from the entry.
            string categoryName = EntryCategoryName.Text;

            // Checks that the category name only contains valid characters.
            if(!String.IsNullOrEmpty(categoryName) && InputCheck.GetInputCheckInstance.IsCategoryNameValid(categoryName))
            {
                if(App.Database.InsertCategory(categoryName) == 1)
                {
                    await DisplayAlert("Done", "Category added.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Error.", "OK");
                }

                // Removes the page from the navigation stack.
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Category name invalid.", "OK");
            }  
        }
        #endregion
    }
}