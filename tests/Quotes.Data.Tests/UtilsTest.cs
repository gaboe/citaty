using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Quotes.Data.Utils;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class UtilsTest
    {
        [TestMethod]
        public void ParseObjectIdTest()
        {
            var id = "59c7a24ae651d381070f54b5";
            var objectID = TypeExtensions.Parse<ObjectId>(id);

            Assert.IsNotNull(objectID);
        }

        [TestMethod]
        public void ParseIntTest()
        {
            var id = "666454544";
            var idInt = TypeExtensions.Parse<int>(id);

            Assert.IsNotNull(idInt);
        }
    }
}
