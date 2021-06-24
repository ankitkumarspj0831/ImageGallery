using Image_Gallery_Demo_Ankit_Kumar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ImageGalleryUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<ImageItem> imagesList = JsonConvert.DeserializeObject<List<ImageItem>>(File.ReadAllText(@"Data/sampleData.json"));
            ImageGallery g = new ImageGallery();
            
            g.setImagesList(imagesList);
            g.calladdTileControl();
            g.AddTiles(imagesList);
            g._SaveAll();
        }
    }
}
