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
    /// The responsibility of HandleCollisionsAction is to handle the situation when the cycle1 
    /// collides with the food, or the cycle1 collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool _isGameOver = false;

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
                // HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        // /// <summary>
        // /// Updates the score nd moves the food if the cycle1 collides with it.
        // /// </summary>
        // /// <param name="cast">The cast of actors.</param>
        // private void HandleFoodCollisions(Cast cast)
        // {
        //     Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
        //     Score score = (Score)cast.GetFirstActor("score");
        //     Food food = (Food)cast.GetFirstActor("food");
            
        //     if (cycle1.GetHead().GetPosition().Equals(food.GetPosition()))
        //     {
        //         int points = food.GetPoints();
        //         cycle1.GrowTail(points);
        //         score.AddPoints(points);
        //         food.Reset();
        //     }
        // }

        /// <summary>
        /// Sets the game over flag if the cycle1 collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
            Actor head = cycle1.GetHead();
            List<Actor> body = cycle1.GetBody();

            foreach (Actor segment in body)
            {
                if (segment.GetPosition().Equals(head.GetPosition()))
                {
                    _isGameOver = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (_isGameOver == true)
            {
                Cycle cycle1 = (Cycle)cast.GetFirstActor("cycle1");
                List<Actor> segments = cycle1.GetSegments();
                // Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                message.SetText("Game Over!");
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                foreach (Actor segment in segments)
                {
                    segment.SetColor(Constants.WHITE);
                }
                // food.SetColor(Constants.WHITE);
            }
        }

    }
}