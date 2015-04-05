using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestClass]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[TestInitialize]
		public void TestInitialize()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[TestMethod]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[TestMethod]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [TestMethod()]
    public void TestThatCarDoesGetLocationFromTheDatabase()
    {
    IDatabase mockDB = mocks.StrictMock<IDatabase>();
    String aLocation = "Owasso, OK";
    String anotherLocation = "San Diego, CA";
    Expect.Call(mockDB.getCarLocation(24)).Return(aLocation);
    Expect.Call(mockDB.getCarLocation(1025)).Return(anotherLocation);
    mocks.ReplayAll();  
    Car target = new Car(10);
    target.Database = mockDB;
    String result;
    result = target.getCarLocation(1025);
    Assert.AreEqual(anotherLocation, result);
    result = target.getCarLocation(24);
    Assert.AreEqual(aLocation, result);
    mocks.VerifyAll();
    }
   
        [TestMethod()]
    public void TestThatCarDoesGetMileageFromDatabase()
    {
    IDatabase mockDatabase = mocks.StrictMock<IDatabase>();
    Int32 Miles = 100000;

    Expect.Call(mockDatabase.Miles).PropertyBehavior();
    mocks.ReplayAll();
    mockDatabase.Miles = Miles;
    var target = new Car(10);
    target.Database = mockDatabase;
    int MileCount = target.Mileage;
    Assert.AreEqual(MileCount, Miles);
    mocks.VerifyAll();
    }
        [TestMethod()]
        public void TestThatObjectMotherReturnsMileageForBMW()
        {
            var target = ObjectMother.BMW();
            int targetExpectedPrice = 80;
            Assert.AreEqual(targetExpectedPrice, target.getBasePrice());
           
        }
	}
}
