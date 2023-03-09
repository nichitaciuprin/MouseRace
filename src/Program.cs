public static class Program
{
    private static void Main()
    {
        var timer = new System.Diagnostics.Stopwatch();
        var fixedDeltaTime = 0.02f;
        var fixedDeltaTimeInMilliseconds = 20;
        var bound = new Rect(new Vector2(0,0), new Vector2(700,700));
        var game = new Game(bound);
        var gameWindow = new GameWindow(game);

        while (true)
        {
            timer.Restart();

            game.Update(fixedDeltaTime);
            gameWindow.Render(game);
            if (gameWindow.ShouldClose) return;

            timer.Stop();

            var diff = timer.ElapsedMilliseconds;
            var waitTime = fixedDeltaTimeInMilliseconds - diff;
            if (waitTime < 0) waitTime = 0;
            Thread.Sleep((int)waitTime);
        }
    }
}