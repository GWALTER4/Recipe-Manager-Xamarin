using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManagerXamarin
{
    class ImageManager
    {
        #region PROPERTIES
        public string FilePath { get; private set; } // Stores the file path for an image.

        #endregion

        #region CONSTRUCTORS
        #endregion

        #region METHODS

        /// <summary>
        /// Takes a photo and stores the file path.
        /// </summary>
        /// <returns>Code value.</returns>
        public async Task<int> TakePhotoAsync()
        {
            // Initializes the camera components.
            await CrossMedia.Current.Initialize();

            // Checks that the device has a camera that can take photos.
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return -1;
            }

            // Takes a photo and stores it.
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                // No options specified.
            });

            // Checks that the file is not null.
            if(file == null)
            {
                return -1;
            }

            FilePath = file.Path;
            file.Dispose();

            return 0;
        }

        /// <summary>
        /// Adds a photo to a recipe.
        /// </summary>
        /// <param name="recipe">Recipe object</param>
        /// <returns>Code value</returns>
        public int AddPhotoToRecipe(Recipe recipe)
        {
            return App.Database.UpdateRecipeImagePath(recipe, FilePath);
        }
        #endregion
    }
}
