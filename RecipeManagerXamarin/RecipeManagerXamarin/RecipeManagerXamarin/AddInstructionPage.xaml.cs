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
	public partial class AddInstructionPage : ContentPage
	{
        #region PROPERTIES
        private ObservableCollection<Instruction> instructionList; // List of instructions.
        #endregion

        #region CONSTRUCTORS
        public AddInstructionPage ()
		{
            InitializeComponent();

            // Changes the colour of the navigation bar.
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#D81B60");

            //Initializes the instruction list.
            instructionList = new ObservableCollection<Instruction>();

            // Sets the items source of the list view.
            Test.ItemsSource = instructionList;

            // Sets the item selected listener for the list view.
            Test.ItemSelected += ListViewInstructions_ItemSelected;

            // Sets the item tapped listener for the list view.
            Test.ItemTapped += ListViewInstructions_ItemTapped;

            // Sets the clicked listener for the image button.
            ImageButtonAddIngredient.Clicked += ImageButtonAddInstruction_Clicked;

            // Sets the clicked listener for the toolbar item.
            ToolbarItemInstructionDone.Clicked += ToolbarItemInstructionsDone_Clicked;
        }
        #endregion

        #region EVENT HANDLERS

        /// <summary>
        /// Item selected listener for the list view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private void ListViewInstructions_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Sets the item to -1 so highlighting is disabled.
            Test.SelectedItem = -1;
        }

        /// <summary>
        /// Tapped listener for the list view.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ListViewInstructions_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool answer = await DisplayAlert("Delete", "Are you sure you want to delete this ingredient?", "Yes", "No");

            if (answer)
            {
                // Removes the ingredient from the list.
                instructionList.RemoveAt(e.ItemIndex);
            }
        }

        /// <summary>
        /// Clicked listener for the image button.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ImageButtonAddInstruction_Clicked(object sender, EventArgs e)
        {
            // Gets the string from the entry.
            string instructionText = EditorInstruction.Text;

            // Checks that the ingredient name only contains valid characters.
            if (!string.IsNullOrEmpty(instructionText) && InputCheck.GetInputCheckInstance.IsCategoryNameValid(instructionText))
            {
                instructionList.Add(new Instruction{ Description = instructionText});
                EditorInstruction.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Instruction text invalid.", "OK");
            }
        }

        /// <summary>
        /// Clicked listener for the toolbar item.
        /// </summary>
        /// <param name="sender">Sending object</param>
        /// <param name="e">Event</param>
        private async void ToolbarItemInstructionsDone_Clicked(object sender, EventArgs e)
        {
            // Creates an array to store the instruction list.
            Instruction[] instructionListArray = new Instruction[instructionList.Count()];

            // Copies the ingredients to an array.
            instructionList.CopyTo(instructionListArray, 0);

            bool answer = await DisplayAlert("Add instructions", "Are you sure you want to add these instructions?", "Yes", "No");

            if (answer)
            {
                // Sends a message with the ingredients list.
                MessagingCenter.Send(this, "AddInstructions", instructionListArray);

                // Removes the page from the navigation stack.
                await Navigation.PopAsync();
            }
        }
        #endregion
    }
}