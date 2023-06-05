using FooksCowSayTest;

namespace FooksCowSayTestUnitTests
{
    public class CowSayTests
    {
        private string? previousMessage;
        private CowSay cowSay;
        
        private const string CowFirstLineCheck = "         \\  ^__^";
        private const string TestMessage = "Hello world!";
        private const string LargeTestMessage = "BlaatBlaatBlaatBlaatBlaatBlaatBlaatBlaatBlaatBlaat";
        private const string ShowPreviousResultKeyword = "previous";

        [SetUp]
        public void Setup()
        {
            cowSay = new CowSay();

            //temp store previous cow message if present
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "previouscow.json")))
            {
                previousMessage = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "previouscow.json"));
            }
        }

        [Test]
        public void IsOnlyCowShowingOnEmptyInputTest()
        {
            var result = cowSay.CreateCow(string.Empty);

            Assert.That(result[0].Equals(CowFirstLineCheck));
        }

        [Test]
        public void IsTestMessageShowingAboveCowTest()
        {
            var result = cowSay.CreateCow(TestMessage);

            Assert.That(result[1].Contains(TestMessage));
            Assert.That(result[3].Equals(CowFirstLineCheck));
        }

        [Test]
        public void ShowResultMultipleLineOnLargeInputStringTest()
        {
            var result = cowSay.CreateCow(LargeTestMessage);

            Assert.That(result[1].Contains("Blaat"));
            Assert.That(result[2].Contains("Blaat"));
        }

        [Test]
        public void ShowPreviousResultOnSpecificInputTest()
        {
            cowSay.CreateCow(TestMessage);
            var result = cowSay.CreateCow(ShowPreviousResultKeyword);

            Assert.That(result[1].Contains(TestMessage));
        }

        [TearDown]
        public void RestorePreviousCowMessage()
        {
            if (!string.IsNullOrEmpty(previousMessage))
            {
                File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "previouscow.json"), previousMessage);
            }            
        }
    }
}