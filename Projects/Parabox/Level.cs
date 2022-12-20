namespace Parabox;

/// <summary>
/// A level which contains one or more worlds.
/// </summary>
public sealed class Level
{
	private readonly Dictionary<WorldAndPosition, Entity> entities;
	private readonly Dictionary<Entity, WorldAndPosition> positions;
	
	/// <summary>
	/// A list of the worlds in the level.
	/// </summary>
	public IReadOnlyList<World> Worlds { get; }

	/// <summary>
	/// The entities in the level.
	/// </summary>
	public IEnumerable<EntityDescriptor> Entities =>
		entities.Select(kvp =>
			new EntityDescriptor(kvp.Key, kvp.Value));



	public Level(IEnumerable<World> worlds, IEnumerable<EntityDescriptor> entities)
	{
		var es = entities.ToArray();
		this.entities = es
			.ToDictionary(e => e.Position, e => e.Entity);
		positions = es
			.ToDictionary(e => e.Entity, e => e.Position);
		
		Worlds = worlds.ToArray();
	}



	/// <summary>
	/// Tries to move an entity to a new position.
	/// </summary>
	/// <param name="entity">The entity to move.</param>
	/// <param name="newPosition">The position to move the entity to.</param>
	public void MoveEntity(Entity entity, WorldAndPosition newPosition)
	{
		if (entities.ContainsKey(newPosition)) return;
		
		var oldPosition = positions[entity];
		entities.Remove(oldPosition);
		
		entities[newPosition] = entity;
		positions[entity] = newPosition;
	}

	/// <summary>
	/// Gets the ID of a world.
	/// </summary>
	/// <param name="world">The world to get the ID of.</param>
	public int GetWorldId(World world)
	{
		for (var i = 0; i < Worlds.Count; i++)
		{
			if (Worlds[i] == world) return i;
		}

		throw new InvalidOperationException("World does not exist in the current level.");
	}
}
