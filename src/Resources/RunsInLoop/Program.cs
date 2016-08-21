#region

using System.Threading;

#endregion

namespace RunsInLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(5000);
            }
        }
    }
}