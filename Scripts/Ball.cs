using Godot;
using System;

public class Ball : RigidBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // [Export] Exposes variables in order to be used inside the Godot editor
    [Export]
    private int speedUp = 10;
    private const int MAXSPEED = 400;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        // These 2 lines are needed for collision detection 
        // ie. GetCollidingBodies()
        ContactMonitor = true;
        ContactsReported = 1;

        // Global scope methods and constants are available in the GD class
        //Ex.
        // GD.Print("Ready");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Get all Node bodies that are currently colliding with the Ball
        Godot.Collections.Array bodies = GetCollidingBodies();

        // The base level class is Node2D, this is why the body variable is being initiliazed as the Node2D type
        foreach (Node2D body in bodies)
        {
            // Check if the collided body is a StaticBody2D type and if the Node is part of the "Bricks" group
            if (body is StaticBody2D hit && hit.IsInGroup("Bricks"))
            {
                // Get the World Node and increment the Score by 10
                GetNode<World>("/root/World").Score += 10;
                // Queue Node for deletion at the end of the current frame
                hit.QueueFree();
            }

            // Check if the Node's name is "Paddle"
            if (body.Name.Equals("Paddle"))
            {
                // Get the speed at which the Ball is travelling
                float currentSpeed = LinearVelocity.Length();
                // Reference to the anchor right below the Paddle object
                Position2D anchor = body.GetNode<Position2D>("Anchor");
                // Get the direction that the ball is travelling in relative to the anchor
                Vector2 direction = Position - anchor.GlobalPosition;
                // Normalize (value of 0-1) the direction and multiply it by the minimum value of the current speed + the speedup constant, or the maximum speed
                Vector2 boostVelocity = direction.Normalized() * Math.Min(currentSpeed + speedUp, MAXSPEED);
                // Set the Ball's LinearVelocity to the new velocity
                LinearVelocity = boostVelocity;
            }

        }
        // If the position of the Ball's Y position exceeds the extend of the ViewPort's Y position
        if (Position.y > GetViewportRect().End.y)
        {
            // Queue the Ball for deletion
            QueueFree();
        }
    }
}
