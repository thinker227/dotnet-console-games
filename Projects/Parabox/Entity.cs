namespace Parabox;

/// <summary>
/// Entities within the world.
/// </summary>
public abstract record class Entity
{
	/// <summary>
	/// The player entity.
	/// </summary>
	public sealed record class Player : Entity;

	/// <summary>
	/// A movable box.
	/// </summary>
	public sealed record class Box : Entity;

	/// <summary>
	/// A portal to another world.
	/// </summary>
	/// <param name="WorldId">The ID of the world to portal leads to.</param>
	public sealed record class Portal(
		int WorldId) : Entity;
}

/// <summary>
/// A pair of an entity and a world position.
/// </summary>
public readonly record struct EntityDescriptor(
	WorldAndPosition Position,
	Entity Entity);
