using Godot;

public partial class PlayerShip : CharacterBody2D
{
	[Export]
	public float Speed = 250f;

	private Vector2 targetPosition;

	public override void _Ready()
	{
		targetPosition = GlobalPosition;
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouse &&
	mouse.Pressed &&
	mouse.ButtonIndex == MouseButton.Left)
{
	targetPosition = GetGlobalMousePosition();

	Vector2 direction = targetPosition - GlobalPosition;
	Rotation = direction.Angle() - Mathf.Pi/2;
}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = targetPosition - GlobalPosition;

		if (direction.Length() > 5)
		{
			Velocity = direction.Normalized() * Speed;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}
}
