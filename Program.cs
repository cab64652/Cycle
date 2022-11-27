using CycleGame.Game.Casting;
using CycleGame.Game.Directing;
using CycleGame.Game.Scripting;
using CycleGame.Game.Services;


namespace CycleGame 
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            Point start1 = new Point(Constants.MAX_X / 2, Constants.MAX_Y / 2);
            Color color1 = Constants.GREEN;

            Point start2 = new Point(Constants.MAX_X / 4, Constants.MAX_Y / 4);
            Color color2 = Constants.RED;

            // create the cast
            Cast cast = new Cast();
            cast.AddActor("cycle1", new Cycle(start1, color1));
            cast.AddActor("cycle2", new Cycle(start2, color2));
            // cast.AddActor("food", new Food());
            // cast.AddActor("score", new Score());

            // create the services
            KeyboardService keyboardService = new KeyboardService();
            VideoService videoService = new VideoService(false);
           
            // create the script
            Script script = new Script();
            script.AddAction("input", new ControlActorsAction(keyboardService));
            script.AddAction("update", new MoveActorsAction());
            script.AddAction("update", new HandleCollisionsAction());
            script.AddAction("output", new DrawActorsAction(videoService));

            // start the game
            Director director = new Director(videoService);
            director.StartGame(cast, script);
        }
    }
}