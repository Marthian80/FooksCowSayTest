namespace FooksCowSayTest
{
    public static class Extensions
    {
        public static IEnumerable<string> Split(this string stringToSplit, int splittedLength)
        {
            if (string.IsNullOrEmpty(stringToSplit) || splittedLength < 1)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < stringToSplit.Length; i += splittedLength)
            {
                if (stringToSplit.Length - i >= splittedLength)
                {
                    yield return stringToSplit.Substring(i, splittedLength);
                }
                else
                {
                    yield return stringToSplit.Substring(i, stringToSplit.Length - i);
                }
                    
            }
        }
    }
}
