namespace FooksCowSayTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            var cowSay = new CowSay();
            var continueLoop = true;
            while (continueLoop)
            {
                var cow = cowSay.CreateCow(input);
                foreach (var line in cow)
                {
                    Console.WriteLine(line);
                }
                input = Console.ReadLine();
                if (input == "exit")
                {
                    continueLoop = false;
                }
            }
        }
    }
}
