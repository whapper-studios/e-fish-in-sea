using Godot;
using System;


public partial class pink_man : CharacterBody2D
{
	[Export]
	public AnimatedSprite2D pink_man_sprite;
	
	public const float Speed = 300.0f;
	public const float JumpVelocity = -1050.0f;
	public const float Gravity = 2250;
	
	public override void _Ready()
	{
		pink_man_sprite.Play("idle");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		if (Input.IsActionJustReleased("ui_accept") && velocity.Y < 0)
		{
			velocity.Y = velocity.Y * .4f;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float direction = Input.GetAxis("ui_left", "ui_right");
		velocity.X = direction * Speed;
		
		if(!IsOnFloor())
		{
			velocity += new Vector2(0, Gravity) * (float)delta;
			pink_man_sprite.Play("jump");
		}
		else if(velocity.X > 0)
		{
			pink_man_sprite.Play("run");
		}
		else if(velocity.X < 0)
		{
			pink_man_sprite.Play("run");
		}
		else {
			pink_man_sprite.Play("idle");
		}
		
		if (velocity.X < 0)
		{
			pink_man_sprite.FlipH = true;
		}
		else if (velocity.X > 0)
		{
			pink_man_sprite.FlipH = false;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
