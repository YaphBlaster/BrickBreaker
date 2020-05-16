using Godot;
using System;

public class World : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";


    private int score;

    // Syntax for Getter/Setter with default value
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            SetScore(value);
        }
    }


    public void SetScore(int value)
    {
        score = value;
        GetNode("Score").Set("text", $"Score: {score}");
    }


}
