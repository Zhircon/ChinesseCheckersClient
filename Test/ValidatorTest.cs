using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class ValidatorTest
    {
        [TestMethod]
        public void ValidEmail()
        {
            bool expectedResult = true;
            string inputOne = "agnizahir@gmail.com";
            string inputTwo = "zs20015687@estudiantes.uv.mx.com";
            string inputThree = "turo602@gmail.com";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsEmailValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsEmailValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsEmailValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void InvalidEmail()
        {
            bool expectedResult = false;
            string inputOne = "agnizahirgmail.com";
            string inputTwo = "@estudiantes.uv.mx.com";
            string inputThree = "turo602@";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsEmailValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsEmailValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsEmailValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void ValidNickname()
        {
            bool expectedResult = true;
            string inputOne = "Zhircon";
            string inputTwo = "Osonovle";
            string inputThree = "vegeta777";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsNicknameValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsNicknameValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsNicknameValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void InvalidNickname()
        {
            bool expectedResult = false;
            string inputOne = "Zhircon4328agni";
            string inputTwo = "Aversiestonotruena";
            string inputThree = "Este no es un nombre valdo";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsNicknameValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsNicknameValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsNicknameValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void ValidPassword()
        {
            bool expectedResult = true;
            string inputOne = "Zhircon4328agni";
            string inputTwo = "A1G2n3i4zhircon.";
            string inputThree = "Yava020811HdFxnga8";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsPasswordValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsPasswordValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsPasswordValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void InvalidPassword()
        {
            bool expectedResult = false;
            string inputOne = "zhircon";
            string inputTwo = "12334";
            string inputThree = "__";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsPasswordValid(inputOne);
            bool obtainedResultTwo = ChinesseCheckersClient.Validator.IsPasswordValid(inputTwo);
            bool obtainedResultThree = ChinesseCheckersClient.Validator.IsPasswordValid(inputThree);
            Assert.AreEqual(expectedResult, obtainedResultOne);
            Assert.AreEqual(expectedResult, obtainedResultTwo);
            Assert.AreEqual(expectedResult, obtainedResultThree);
        }
        [TestMethod]
        public void ValidSubject()
        {
            bool expectedResult = true;
            string inputOne = "saludos";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsSubjectValid(inputOne);
            Assert.AreEqual(expectedResult, obtainedResultOne);
        }
        [TestMethod]
        public void InvalidSubject()
        {
            bool expectedResult = false;
            string inputOne = "Muvaffakiyetsezlestiricilestiriveremeyebileceklerimizdenmissinizcesine a";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsSubjectValid(inputOne);
            Assert.AreEqual(expectedResult, obtainedResultOne);
        }
        public void ValidBody()
        {
            bool expectedResult = true;
            string inputOne = "Muvaffakiyetsezlestiricilestiriveremeyebileceklerimizdenmissinizcesine a";
            bool obtainedResultOne = ChinesseCheckersClient.Validator.IsSubjectValid(inputOne);
            Assert.AreEqual(expectedResult, obtainedResultOne);
        }
    }
}