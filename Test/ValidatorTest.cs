﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void ValidNickname1()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsNicknameValid("Zhircon4327");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidNickname2()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsNicknameValid("ZhirconA1G2");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void InvalidNickname1()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsNicknameValid("ZhirconA1G2n3n4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void InvalidNickname2()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsNicknameValid("ZhirconA1G2n3i44328");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidPassword1()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("A1G2n3i4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ValidPassword2()
        {
            bool expectedResult = true;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("Paracetalmol24");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword1()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("a1g2n3i4");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword2()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("1234");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword3()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("password");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
        [TestMethod]
        public void ÏnvalidPassword4()
        {
            bool expectedResult = false;
            bool obtainedResult = ChinesseCheckersClient.Validator.IsPasswordValid("PASSWORD");
            Assert.AreEqual(expectedResult, obtainedResult);
        }
    }
}