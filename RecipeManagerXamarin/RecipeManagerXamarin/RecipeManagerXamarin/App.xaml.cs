using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RecipeManagerXamarin
{
    public partial class App : Application
    {
        #region PROPERTIES
        public static RecipeManagerDatabase Database { get; private set; } // Stores a database connection object.
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Constructor for the App class.
        /// </summary>
        /// <param name="dbPath">Database file path.</param>
        public App(string dbPath)
        {
            InitializeComponent();

            // Creates a new database connection object.
            Database = new RecipeManagerDatabase(dbPath);

            // Creates a new main page.
            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#008577"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };
        }
        #endregion

        /// <summary>
        /// Default constructor for XAML previewer.
        /// </summary>
        public App()
        {
            InitializeComponent();
        }

        #region EVENT HANDLERS
        /// <summary>
        /// Event handler for when the app starts.
        /// </summary>
        protected override void OnStart()
        {
            
        }

        /// <summary>
        /// Event handler for when the app sleeps.
        /// </summary>
        protected override void OnSleep()
        {
            
        }

        /// <summary>
        /// Event handler for when the app resumes.
        /// </summary>
        protected override void OnResume()
        {
            
        }
        #endregion
    }
}
