using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using ImageGalleryUnitTest;

namespace ImageGalleryUnitTest
{
    [TestClass]
    public class SavingImagesLocally
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            string readText =
               File.ReadAllText(@"Data/sampleData.json");
            // Act
            ImageGallery img = new ImageGallery();
            img.imagesList
            
            // Assert


        }
    }
}
