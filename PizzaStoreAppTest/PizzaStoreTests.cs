using System;
using System.Collections.Generic;
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

        [Theory]

        [InlineData("Dominoes")]
        [InlineData("Pizza Hut")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("You lost your self esteem" +
            "Along the way, yeah... O.o")]


        public void NewCustomerHasNameGivenToConstructor(string testName)
        {
            CustomerClass SUT = new CustomerClass(testName,"whatever, man");

            Assert.Equal(testName, SUT.Username);
        }


        [Theory]

        [InlineData("Dominoes", "abc123","abc123",true)]
        [InlineData("", "", "", true)]
        [InlineData("L00ser", "My Cat Henrey", "password", false)]
        [InlineData("Admin", "password", "password", true)]
        [InlineData("Space Man", "", " ", false)]
        [InlineData("Nothing", "", null, false)]
        [InlineData("Null?", null, null, true)]


        public void NewCustomerPasswordCheck(string testName, string testPW, string testInput, bool expected)
        {
            // arrange
            CustomerClass SUT = new CustomerClass(testName, testPW);

            // act
            bool result = SUT.CheckPassword(testInput);

            // assert
            Assert.Equal(result, expected);
        }


    }
}
