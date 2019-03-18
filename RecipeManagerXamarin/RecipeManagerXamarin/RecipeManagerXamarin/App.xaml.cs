using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RecipeManagerXamarin
{
    public partial class App : Application
    {
        public static RecipeManagerDatabase Database { get; private set; }

        public App(string dbPath)
        {
            InitializeComponent();

            Database = new RecipeManagerDatabase(dbPath);

            MainPage = new NavigationPage(new MainPage())
            {
                BarBackgroundColor = Color.FromHex("#008577"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };
        }

        public App()
        {
            InitializeComponent();
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
    }
}
