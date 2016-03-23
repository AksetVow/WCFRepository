using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GeoLib.Data;
using GeoCode.Contracts;
using GeoCode.Services;

namespace GeoCode.Tests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void test_zip_code_retrieval()
        {
            Mock<IZipCodeRepository> mockZipCodeRepository = new Mock<IZipCodeRepository>();

            ZipCode zipCode = new ZipCode()
            {
                City = "Cherkassy",
                State = new State() { Abbreviation = "Ch"},
                Zip = "03127"
            };

            mockZipCodeRepository.Setup(obj => obj.GetByZip("03127")).Returns(zipCode);

            IGeoService geoService = new GeoManager(zipCodeRepository: mockZipCodeRepository.Object, stateRepository: null);

            ZipCodeData zipCodeData = geoService.GetZipInfo("03127");

            Assert.AreEqual(zipCodeData.City, "Cherkassy");
            Assert.AreEqual(zipCodeData.State, "Ch");
            


        }
    }
}
