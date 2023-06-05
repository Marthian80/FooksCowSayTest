using FooksCowSayTest;

namespace FooksCowSayTestUnitTests
{
    public class JsonReadWriteTests
    {
        private JsonReadWrite jsonReaderWriter;
        private MessageData messageData;
        private string filePath;

        private const string TESTFILENAME = "test.json";
        private const string TESTDATA = "Test";

        [SetUp]
        public void Setup()
        {
            jsonReaderWriter = new JsonReadWrite();
            messageData = new MessageData();
            messageData.CowMessage = new List<string>() { TESTDATA };
            filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, TESTFILENAME);
        }

        [Test]
        public void WriteFileSucceedTest()
        {
            jsonReaderWriter.WriteToFile(messageData, TESTFILENAME);           
                        
            Assert.IsTrue(File.Exists(filePath));
        }

        [Test]
        public void ReadFromFileSucceedsTest()
        {
            jsonReaderWriter.WriteToFile(messageData, TESTFILENAME);

            Assume.That(File.Exists(filePath));

            var result = jsonReaderWriter.ReadMessageDataFromFile(TESTFILENAME);

            Assert.That(result.CowMessage[0].Equals(TESTDATA));
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
