unsafe
{
    long rolls = 0;
    long maxOnes = 0;
    Random random = new ();
    DateTime startTime = DateTime.UtcNow;
    Console.WriteLine("Start Time: {0}", startTime);
    Parallel.For(0, 1_000_000_000, (i, loopState) =>
    {
        if (loopState.ShouldExitCurrentIteration)
            return;
        int[] numbers = [0, 0, 0, 0];

        for (int j = 0; j < 231; j++)
        {
            if (loopState.ShouldExitCurrentIteration)
                return;
            int roll = random.Next(1,5);
            Interlocked.Increment(ref numbers[roll - 1]);
        }
        Interlocked.Increment(ref rolls);

        if (numbers[0] > maxOnes)
            Interlocked.Exchange(ref maxOnes, numbers[0]);

        if (numbers[0] > 177)
            loopState.Stop();
    });

    Console.WriteLine("Highest Ones Roll: {0}", maxOnes);
    Console.WriteLine("Number of Roll Sessions: {0}", rolls);
    DateTime endTime = DateTime.UtcNow;
    Console.WriteLine("End Time: {0}", endTime);
    Console.WriteLine("Took {0}", endTime - startTime);
}