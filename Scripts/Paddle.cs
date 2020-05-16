using Godot;
using System;

public class Paddle : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Loading a Scene Instance with C#
    PackedScene BallActor = ResourceLoader.Load<PackedScene>("res://Actors/Ball.tscn");

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        return;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        // Current Y position of the GameObject/Actor/Node
        float CurrentY = Position.y;

        // Current Mouse X position in the Viewport(Screen)
        float MouseX = GetViewport().GetMousePosition().x;

        // Set the position of the GameObject/Actor/Node
        // Use the Mouse's X coordinate for the first Vector paramater
        // Use the Current Y coordinate for the second Vector paramater
        GlobalPosition = new Vector2(MouseX, CurrentY);
    }

    public override void _Input(InputEvent inputEvent)
    {
        // Check if the input event was a mouse click and if an input event is pressed down
        if (inputEvent is InputEventMouseButton && inputEvent.IsPressed())
        {
            // Create an instance of the Ball Actor
            RigidBody2D Ball = (RigidBody2D)BallActor.Instance();

            // Set the Ball's  position a little bit above the paddle
            Ball.Position = Position - new Vector2(0, 16);

            // Add the Ball to the Scene
            // GetTree gets the hierarchy of the World
            // Root is (0,0) in the World
            GetTree().Root.AddChild(Ball);
        }
    }
}
