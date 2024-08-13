﻿unsafe
{
    _ = long.TryParse(args[0], out long trials);
    long rolls = 0;
    long maxOnes = 0;
    DateTime startTime = DateTime.UtcNow;
    Console.WriteLine("Start Time: {0}", startTime);
    Parallel.For(0, trials, (i, loopState) =>
    {
        if (loopState.ShouldExitCurrentIteration)
            return;
        Random random = new ();
        int oneCount = 0;
        for (int j = 0; j < 231; j++)
        {
            if (loopState.ShouldExitCurrentIteration)
                return;
            int roll = random.Next(1,5);
            oneCount += Convert.ToInt32((roll % 4) == 0);
        }
        Interlocked.Increment(ref rolls);

        if (oneCount > maxOnes)
            Interlocked.Exchange(ref maxOnes, oneCount);

        if (maxOnes > 177)
            loopState.Stop();
    });

    Console.WriteLine("Highest Ones Roll: {0}", maxOnes);
    Console.WriteLine("Number of Roll Sessions: {0}", rolls);
    DateTime endTime = DateTime.UtcNow;
    Console.WriteLine("End Time: {0}", endTime);
    Console.WriteLine("Took {0}", endTime - startTime);
}