using Parabox;

Level level = new(new World[]
{
	new(
		7,
		new Position[]
	{
		new(0, 0),
		new(0, 1),
		new(0, 2),
		new(0, 3),
		new(0, 4),
		new(0, 5),
		new(0, 6),
		new(1, 6),
		new(2, 6),
		new(3, 6),
		new(4, 6),
		new(5, 6),
		new(6, 6),
		new(6, 5),
		new(6, 4),
		new(6, 3),
		new(6, 2),
		new(6, 1),
		new(6, 0),
		new(5, 0),
		new(4, 0),
		new(3, 0),
		new(2, 0),
		new(1, 0)
	},
		new ObjectiveDescriptor[]
		{
			new(new(new(4, 3), 0), new Objective.Player()),
			new(new(new(3, 2), 0), new Objective.Box())
		})
}, new EntityDescriptor[]
{
	new(new(new(2, 3), 0), new Entity.Player()),
	new(new(new(3, 4), 0), new Entity.Box())
});

while (true)
{
	Render(level);
	var key = Console.ReadKey(true).Key;

	Position p = key switch
	{
		ConsoleKey.RightArrow => new(1, 0),
		ConsoleKey.LeftArrow => new(-1, 0),
		ConsoleKey.DownArrow => new(0, 1),
		ConsoleKey.UpArrow => new(0, -1),
		_ => new()
	};

	var playerEntities = level.Entities.Where(ed => ed.Entity is Entity.Player).ToArray();
	foreach (var playerEntity in playerEntities)
	{
		level.MoveEntity(playerEntity.Entity, playerEntity.Position with
		{
			Position = playerEntity.Position.Position + p
		});
	}
}



static void Render(Level level)
{
	var world = level.Worlds[0];
	
	HashSet<Position> drawn = new();
	
	Console.ForegroundColor = ConsoleColor.Gray;
	foreach (var g in world.Geometry)
	{
		if (drawn.Contains(g)) continue;
		drawn.Add(g);
		
		Console.SetCursorPosition(g.X, g.Y);
		Console.Write('#');
	}
	
	foreach (var entity in level.Entities)
	{
		if (drawn.Contains(entity.Position.Position)) continue;
		drawn.Add(entity.Position.Position);
		
		var (c, color) = entity.Entity switch
		{
			Entity.Player => ('p', ConsoleColor.Magenta),
			Entity.Box => ('b', ConsoleColor.Yellow),
			Entity.Portal portal => ((char)(portal.WorldId + '0'), ConsoleColor.Cyan),
			_ => ('?', ConsoleColor.Gray)
		};

		Console.SetCursorPosition(entity.Position.Position.X, entity.Position.Position.Y);
		Console.ForegroundColor = color;
		Console.Write(c);
	}
	
	foreach (var (p, o) in world.Objectives)
	{
		if (drawn.Contains(p.Position)) continue;
		drawn.Add(p.Position);
		
		Console.SetCursorPosition(p.Position.X, p.Position.Y);
		Console.ForegroundColor = ConsoleColor.White;
		Console.Write(o switch
		{
			Objective.Player => '=',
			Objective.Box => '_',
			_ => '?'
		});
	}

	Console.ForegroundColor = ConsoleColor.DarkGray;
	for (var x = 0; x < world.Size; x++)
	{
		for (var y = 0; y < world.Size; y++)
		{
			Position p = new(x, y);
			
			if (drawn.Contains(p)) continue;
			drawn.Add(p);
			
			Console.SetCursorPosition(p.X, p.Y);
			Console.Write('.');
		}
	}
	
	Console.ForegroundColor = ConsoleColor.Gray;
	Console.SetCursorPosition(0, world.Size + 1);
}
