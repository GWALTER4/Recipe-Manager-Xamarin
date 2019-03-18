using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RecipeManagerXamarin
{
    public partial class App : Application
    {
        private static RecipeManagerDatabase database; // Stores the database.

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static RecipeManagerDatabase Database
        {
            get
            {
                // Creates a new database if one does not exist.
                if (database == null)
                {
                    database = new RecipeManagerDatabase(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RecipeManagerDatabase.db3"));
                }
                return database;
            }
        }
    }
}
