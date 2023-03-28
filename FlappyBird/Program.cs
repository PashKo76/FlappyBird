namespace FlappyBird
{
    delegate void Update();
    internal class Program
    {
        static void Main(string[] args)
        {
            Scene scene = new Scene(200, 50, 2);
            bool IsGamePlay = true;
            scene.Start();
            DateTimeOffset GameStart = DateTimeOffset.Now;
            while (IsGamePlay)
            {
                scene.Update(ref IsGamePlay);
            }
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Game Started in {GameStart}");
            Console.WriteLine($"Game Ended in {DateTimeOffset.Now}");
            Console.ReadKey();
        }
    }
}