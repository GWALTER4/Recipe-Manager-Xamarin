using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RecipeManagerXamarin
{
    public partial class MainPage : ContentPage
    {
        #region PROPERTIES
        public List<Category> CategoryList; // List of categories.
        #endregion

        #region PROPERTIES
        /// <summary>
        /// Constructor for the MainPage class.
        /// </summary>
        public MainPage()
        {
            BindingContext = this;

            InitializeComponent();

            // Sets the items source of the list view.
            Categories.ItemsSource = CategoryList;

            // Sets the item selected listener for the list view.
            Categories.ItemSelected += Categories_ItemSelected;

            Categories.ItemTapped += Categories_ItemTapped;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemAddCategory.Clicked += ToolbarItemAddCategory_Clicked;
        }

        async void Categories_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new CategoryPage());
        }
        #endregion

        #region EVENT HANDLERS
        /// <summary>
        /// Event handler for when the page appears.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#008577");

            // Gets all the categories from the database.
            CategoryList = App.Database.GetAllCategories();

            // Sets the items source of the list view.
            Categories.ItemsSource = CategoryList;
        }

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        async void ToolbarItemAddCategory_Clicked(object sender, EventArgs e)
        {
            // Adds a page to the navigation stack.
            await Navigation.PushAsync(new AddCategoryPage());
        }

        /// <summary>
        /// Item selected listener for the list view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private void Categories_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Sets the item to -1 so highlighting is disabled.
            Categories.SelectedItem = -1;
        }
        #endregion
    }
}
