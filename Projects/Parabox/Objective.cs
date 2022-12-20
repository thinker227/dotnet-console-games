namespace Parabox;

/// <summary>
/// A completion objective for a level.
/// </summary>
public abstract record class Objective
{
	/// <summary>
	/// An objective where the player should be located.
	/// </summary>
	public sealed record class Player : Objective;

	/// <summary>
	/// An objective where a box or portal should be located.
	/// </summary>
	public sealed record class Box : Objective;
}

/// <summary>
/// A pair of an objective and a world position.
/// </summary>
public readonly record struct ObjectiveDescriptor(
	WorldAndPosition Position,
	Objective Objective);
