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
		public AddCategoryPage ()
		{
			InitializeComponent ();

            // Sets the clicked listener for the toolbar item.
            ToolbarItemDone.Clicked += ToolbarItemDone_Clicked;
		}

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        async void ToolbarItemDone_Clicked(object sender, EventArgs e)
        {          
            // Gets the string from the entry.
            string categoryName = EntryCategoryName.Text;

            // Checks that the category name only contains valid characters.
            if(!String.IsNullOrEmpty(categoryName) && InputCheck.GetInputCheckInstance.IsCategoryNameValid(categoryName))
            {
                if(App.Database.AddNewCategory(categoryName) == 1)
                {
                    DisplayAlert("Done", "Category added.", "OK");
                }
                else
                {
                    DisplayAlert("Error", "Error.", "OK");
                }
                
                
                // Removes the page from the navigation stack.
                await Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Error", "Category name invalid.", "OK");
            }  
        }
    }
}