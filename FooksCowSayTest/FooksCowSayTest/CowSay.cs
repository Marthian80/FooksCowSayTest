using System.Linq;

namespace FooksCowSayTest
{
    public class CowSay
    {
        private const string DividerWhiteSpace = "  ";
        private const string PreviousMessageFile = "previouscow.json";
        private const string ShowPreviousKeyword = "previous";

        private readonly int maxInputLineLength = 40;
        private MessageData messageData;
        private JsonReadWrite jsonReaderWriter;

        public CowSay()
        {
            messageData = new MessageData();
            jsonReaderWriter = new JsonReadWrite();
        }

        public List<string> CreateCow(string inputMessage)
        {            
            //Load and show old input message if exact previous keyword is provided
            if (inputMessage.Equals(ShowPreviousKeyword))
            {
                var previousCow = jsonReaderWriter.ReadMessageDataFromFile(PreviousMessageFile);
                return previousCow.CowMessage;
            }

            var formattedMessage = new List<string>();
            var charCount = inputMessage.Length;
            var dividerLine = string.Empty;
            
            //Only create an output message if user has provided input
            if (!string.IsNullOrEmpty(inputMessage))
            {
                dividerLine = CreateDividerLine(charCount < maxInputLineLength ? charCount : maxInputLineLength);
                formattedMessage = CreateOutputMessage(inputMessage, charCount);
            }                  

            List<string> completeCowSayMessage = BuildCowSayMessage(dividerLine, CreateCow(), formattedMessage);

            //Only save to disk if there was an input message provided
            if (formattedMessage.Any())
            {
                SaveCowSayMessage(completeCowSayMessage);
            }

            return completeCowSayMessage;
        }

        private List<string> BuildCowSayMessage(string dividerLine, string[] cow, List<string> formattedMessage)
        {
            var completeCowSayMessage = new List<string>();

            if (formattedMessage.Any())
            {
                completeCowSayMessage.Add(dividerLine);
                completeCowSayMessage.AddRange(formattedMessage);
                completeCowSayMessage.Add(dividerLine);
            }            
            completeCowSayMessage.AddRange(cow);          

            return completeCowSayMessage;
        }

        private void SaveCowSayMessage(List<string> completeCowSayMessage)
        {
            messageData.CowMessage = completeCowSayMessage;
            jsonReaderWriter.WriteToFile(messageData, PreviousMessageFile);
        }

        private List<string> CreateOutputMessage(string inputMessage, int charCount)
        {
            var theMessageInLines = new List<string>();

            if (charCount > maxInputLineLength)
            {
                theMessageInLines = inputMessage.Split(maxInputLineLength).ToList();
            }
            else
            {
                theMessageInLines.Add(inputMessage);
            }

            for(int i = 0; i < theMessageInLines.Count; i++)
            {
                var originalLine = theMessageInLines[i];

                if (i == 0 && theMessageInLines.Count == 1)
                {
                    theMessageInLines[i] = $"{"< "}{originalLine}{" >"}";
                }
                else if (i == 0 && theMessageInLines.Count > 1)
                {
                    theMessageInLines[i] = $"{"/ "}{originalLine}{@" \"}";
                }
                else if ((i == 1 && theMessageInLines.Count <= 2) || i == theMessageInLines.Count-1)
                {
                    theMessageInLines[i] = $"{@"\ "}{originalLine}{WhiteSpaceGenerator(maxInputLineLength - originalLine.Length)}{" /"}";
                }
                else if (i >= 1 && theMessageInLines.Count > 2)
                {
                    theMessageInLines[i] = $"{"| "}{originalLine}{" |"}";
                }
            }           

            return theMessageInLines;
        }

        private string CreateDividerLine(int dividerLineLength)
        {
            string dividerLine = DividerWhiteSpace;
            var counter = 0;

            while (counter < dividerLineLength)
            {
                dividerLine += "-";
                counter++;
            }
            dividerLine += DividerWhiteSpace;

            return dividerLine;
        }

        private string[] CreateCow()
        {
            return Cow.GetCow().Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }

        private string WhiteSpaceGenerator(int numberOfSpaceRequired)
        {
            var whiteSpaceContainer = string.Empty;

            for(int i = 0; i < numberOfSpaceRequired; i++) 
            {
                whiteSpaceContainer += " ";
            }
            return whiteSpaceContainer;
        }
    }
}