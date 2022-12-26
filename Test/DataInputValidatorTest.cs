using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class DataInputValidatorTest
    {
        [TestMethod]
        public void ValidNickname1()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isNicknameValid("Zhircon4327");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidNickname2()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isNicknameValid("ZhirconA1G2");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void InvalidNickname1()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isNicknameValid("ZhirconA1G2n3n4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void InvalidNickname2()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isNicknameValid("ZhirconA1G2n3i44328");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidPassword1()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("A1G2n3i4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidPassword2()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("Paracetalmol24");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword1()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("a1g2n3i4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword2()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("1234");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword3()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("password");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword4()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.DataInputValidator.isValidPassword("PASSWORD");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
    }
}
