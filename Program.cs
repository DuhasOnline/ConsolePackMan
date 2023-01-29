char[,] map = ReadMapGame("mapgame.txt");
ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);
Console.CursorVisible = false;

Task.Run(() =>
{
    while(true)
        pressedKey = Console.ReadKey();
});

int packManX = 1;
int packManY = 1;
int score = 0;

while (true)
{
    Console.Clear();
    HandleInput(pressedKey, ref packManX, ref packManY, map, ref score);
    
    Console.ForegroundColor = ConsoleColor.Green;
    DrawMap(map);

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.SetCursorPosition(packManX, packManY);
    Console.Write("@");

    Console.ForegroundColor = ConsoleColor.Red;
    Console.SetCursorPosition(32, 0);
    Console.Write($"Очков: {score}");

    Thread.Sleep(1000);
}

static char[,] ReadMapGame(string pathFile)
{
    string[] fileMap = File.ReadAllLines(pathFile);

    char[,] map = new char[GetMaxLengthOfLine(fileMap), fileMap.Length];

    for (int x = 0; x < map.GetLength(0); x++)
    {
        for(int y = 0; y < map.GetLength(1); y++)
        {
            map[x, y] = fileMap[y][x];
        }
    }
    return map;
}

static void DrawMap(char[,] map)
{
    for (int y = 0; y < map.GetLength(1); y++)
    {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            Console.Write(map[x, y]);
        }
        Console.Write("\n");
    }
}

static void HandleInput(ConsoleKeyInfo keyInfo, ref int packManX, ref int packManY, char[,] map, ref int score)
{
    int[] direction = GetDirection(keyInfo);

    int nextPackManPositionX = packManX + direction[0];
    int nextPackManPositionY = packManY + direction[1];

    char nextCell = map[nextPackManPositionX, nextPackManPositionY];

    if (nextCell == ' ' || nextCell == '.')
    {
        packManX = nextPackManPositionX;
        packManY = nextPackManPositionY;

        if (nextCell == '.') score++;
        map[nextPackManPositionX, nextPackManPositionY] = ' ';
    }
}

static int[] GetDirection(ConsoleKeyInfo pressedKey)
{
    int[] direction = { 0, 0 };

    switch (pressedKey.Key)
    {
        case ConsoleKey.UpArrow:
            direction[1] -= 1;
            break;
        case ConsoleKey.DownArrow:
            direction[1] += 1;
            break;
        case ConsoleKey.LeftArrow:
            direction[0] -= 1;
            break;
        case ConsoleKey.RightArrow:
            direction[0] += 1;
            break;
    }
    return direction;
}

static int GetMaxLengthOfLine(string[] lines)
{
    int maxLength = lines[0].Length;

    foreach(var line in lines)
        if (line.Length > maxLength)
            maxLength = line.Length;
    return maxLength;
}