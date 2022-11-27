using System;
using System.Collections.Generic;
using System.Data;
using CycleGame.Game.Casting;
using CycleGame.Game.Services;


namespace CycleGame.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the cycle1.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;
        private bool _isWinner1 = false;
        private bool _isWinner2 = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (_isGameOver == false)
            {
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Sets the game over flag if the cycle1 collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
            Actor head1 = cycle1.GetHead();
            List<Actor> body1 = cycle1.GetBody();
            cycle1.GrowTail(1, Constants.GREEN);

            Cycle cycle2 = (Cycle)cast.GetFirstActor("cycle2");
            Actor head2 = cycle2.GetHead();
            List<Actor> body2 = cycle2.GetBody();
            cycle2.GrowTail(1, Constants.RED);

            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    _isGameOver = true;
                    _isWinner2 = true;
                }
            }

            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    _isGameOver = true;
                    _isWinner1 = true;
                }
            }

            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    _isGameOver = true;
                    _isWinner1 = true;
                }
            }

              foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    _isGameOver = true;
                    _isWinner2 = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
                List<Actor> segments1 = cycle1.GetSegments();
                Cycle cycle2 = (Cycle)cast.GetFirstActor("cycle2");
                List<Actor> segments2 = cycle2.GetSegments();

                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                if (_isWinner1 == true)
                {
                    Actor message = new Actor();
                    message.SetText("Game Over!\n Green Wins!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);

                    foreach (Actor segment in segments2)
                    {
                        segment.SetColor(Constants.WHITE);
                    }

                }

                else if (_isWinner2 == true)
                {
                    Actor message = new Actor();
                    message.SetText("Game Over!\n Red Wins!");
                    message.SetPosition(position);
                    cast.AddActor("messages", message);

                    foreach (Actor segment in segments1)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                }
            }
        }
    }
}