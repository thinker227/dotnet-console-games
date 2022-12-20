namespace Parabox;

/// <summary>
/// A world containing entities.
/// </summary>
public sealed class World
{
	public IReadOnlySet<Position> Geometry { get; }

	public IReadOnlyDictionary<WorldAndPosition, Objective> Objectives { get; }

	public int Size { get; }

	
	
	public World(int size, IEnumerable<Position> geometry, IEnumerable<ObjectiveDescriptor> objectives)
	{
		Geometry = geometry.ToHashSet();
		Objectives = objectives.ToDictionary(obj => obj.Position, obj => obj.Objective);
		Size = size;
	}
}

public readonly record struct WorldAndPosition(
	Position Position,
	int WorldId);
