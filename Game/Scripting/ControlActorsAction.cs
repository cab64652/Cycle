using CycleGame.Game.Casting;
using CycleGame.Game.Services;


namespace CycleGame.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService _keyboardService;
        private Point _direction1 = new Point(Constants.CELL_SIZE, 0);
        private Point _direction2 = new Point(Constants.CELL_SIZE, 0);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this._keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            // left
            if (_keyboardService.IsKeyDown("a"))
            {
                _direction1 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (_keyboardService.IsKeyDown("d"))
            {
                _direction1 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (_keyboardService.IsKeyDown("w"))
            {
                _direction1 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (_keyboardService.IsKeyDown("s"))
            {
                _direction1 = new Point(0, Constants.CELL_SIZE);
            }


            // left
            if (_keyboardService.IsKeyDown("left"))
            {
                _direction2 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (_keyboardService.IsKeyDown("right"))
            {
                _direction2 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (_keyboardService.IsKeyDown("up"))
            {
                _direction2 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (_keyboardService.IsKeyDown("down"))
            {
                _direction2 = new Point(0, Constants.CELL_SIZE);
            }


            Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
            cycle1.TurnHead(_direction1);

            Cycle cycle2 = (Cycle)cast.GetFirstActor("cycle2");
            cycle2.TurnHead(_direction2);

        }
    }
}