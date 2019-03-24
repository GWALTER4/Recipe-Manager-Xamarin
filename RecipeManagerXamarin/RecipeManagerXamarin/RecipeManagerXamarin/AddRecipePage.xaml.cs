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
	public partial class AddRecipePage : ContentPage
	{
        #region CONSTRUCTORS
        public AddRecipePage ()
		{
			InitializeComponent ();

            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            // Sets the clicked listener for the toolbar item.
            ToolbarItemRecipeDone.Clicked += ToolbarItemRecipeDone_Clicked;
        }


        #endregion

        #region EVENT HANDLERS

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