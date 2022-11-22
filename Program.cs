using Cycle.Game.Casting;
using Cycle.Game.Directing;
using Cycle.Game.Scripting;
using Cycle.Game.Services;


namespace Cycle
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
            Point start = new Point(Constants.MAX_X / 2, Constants.MAX_Y / 2);
            Color color = Constants.GREEN;

            // create the cast
            Cast cast = new Cast();
            cast.AddActor("food", new Food());
            cast.AddActor("snake", new Snake(start, color));
            cast.AddActor("score", new Score());

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