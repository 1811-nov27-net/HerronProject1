using System;
using System.Collections.Generic;
using Moq;
using PizzaStoreApp;
using PizzaStoreAppLibrary;
using Xunit;

namespace PizzaStoreAppTest
{
    public class PizzaStoreTests
    {
        [Fact]
        public void NewStoreHasStock()
        {
            // arrange & act
            StoreClass SUT = new StoreClass("Test Name");

            // assert
            foreach (KeyValuePair<string, int> entry in SUT.Invantory)
            {
                Assert.NotEqual(0,entry.Value);
            }



        }

        [Theory]

        [InlineData("Dominoes")]
        [InlineData("Pizza Hut")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("You lost your self esteem" +
            "Along the way, yeah... O.o")]
        

        public void NewStoreHasNameGivenToConstructor(string testName)
        {
            StoreClass SUT = new StoreClass(testName);

            Assert.Equal(testName, SUT.Name);
        }

        //[Theory]

        //[InlineData("Dominoes")]
        //[InlineData("Pizza Hut")]
        //[InlineData("")]
        //[InlineData(null)]
        //[InlineData("You lost your self esteem" +
        //    "Along the way, yeah... O.o")]


        //public void NewCustomerHasNameGivenToConstructor(string testName)
        //{
        //    CustomerClass SUT = new CustomerClass { Username = testName, Password = "whatever, man" };

        //    Assert.Equal(testName, SUT.Username);
        //}


        //[Theory]

        //[InlineData("Dominoes", "abc123", "abc123", true)]
        //[InlineData("", "", "", true)]
        //[InlineData("L00ser", "My Cat Henrey", "password", false)]
        //[InlineData("Admin", "password", "password", true)]
        //[InlineData("Space Man", "", " ", false)]
        //[InlineData("Nothing", "", null, false)]
        //[InlineData("Null?", null, null, true)]


        //public void NewCustomerPasswordCheck(string testName, string testPW, string testInput, bool expected)
        //{
        //    // arrange
        //    CustomerClass SUT = new CustomerClass{Username = testName, Password = testPW};

        //    // act
        //    bool result = SUT.CheckPassword(testInput);

        //    // assert
        //    Assert.Equal(result, expected);
        //}

        [Theory]
        [InlineData("Username",null)]
        public void MokTest(string username, CustomerClass cust)
        {
            
            var mockRepo = new Mock<IPizzaStoreRepo>();
            mockRepo.Setup(repo => repo.LoadCustomerByUsername(username)).Returns(cust);

            mockRepo.Setup(repo => repo.ChangeUserPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CustomerClass>(), It.IsAny<string>()));


        }


    }
}
