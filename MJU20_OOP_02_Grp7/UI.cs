using System;
using System.Collections.Generic;

namespace MJU20_OOP_02_Grp7
{
    public class UI
    {
        public static List<String> EventMessageList = new List<string>();
        // view window size
        public static int height;
        public static int width;
        private static int _counter;

        public static void SetUISize(int x, int y)
        {
            width = x;
            height = y - 5;
            Console.SetWindowSize(x, y);
            Console.SetBufferSize(x, y);
            Console.OutputEncoding = System.Text.Encoding.Unicode;
        }

        public static void DrawEventMessages()
        {
            Console.SetCursorPosition(0, 0);

            if (EventMessageList.Count > 5) EventMessageList.RemoveAt(0);
            if (EventMessageList.Count >= 0)
            {
                foreach (var msg in EventMessageList)
                {
                    if (msg.Contains("took"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(msg);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(msg);
                    }
                }
            }
            if (EventMessageList.Count > 0)
            {
                _counter++;
                if (_counter % 10 == 0) EventMessageList.RemoveAt(0);
            }
        }

        public static void DrawScreen(char[,] map, Player player, Entity[] entities)
        {
            Console.CursorVisible = false;
            DrawMap(map, player.Position);
            DrawEntities(entities, player.Position);
            DrawPlayer(player);
            DrawStats(player);
            DrawEventMessages();
        }

        private static void DrawStats(Player player)
        {
            Draw(5, height + 1, ConsoleColor.Green, $"Player: {Game.player.PlayerName}  HP: {player.Hp}  Attack Power: {Game.player.Dmg}  Points: {Game.player.PlayerScore} Time: {Game.GetTick()/2} seconds");
        }

        private static void DrawPlayer(Player player)
        {
            Draw(width / 2, height / 2, player.Color, player.Symbol.ToString());
        }

        private static void DrawEntities(Entity[] entities, Point playerPosition)
        {
            foreach (Entity entity in entities)
            {
                Point screenPos = new Point(entity.Position.X + (width / 2) - playerPosition.X, entity.Position.Y + (height / 2) - playerPosition.Y);
                if (screenPos.X >= 0 && screenPos.X <= width && screenPos.Y >= 0 && screenPos.Y <= height)
                {
                    Draw(screenPos.X, screenPos.Y, entity.Color, entity.Symbol.ToString());
                }

            }
        }

        private static void DrawMap(char[,] map, Point playerPosition)
        {
            string background = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int mapX = playerPosition.X - (width / 2) + x;
                    int mapY = playerPosition.Y - (height / 2) + y;
                    if (mapX < 0 || mapY < 0 || mapX >= map.GetLength(0) || mapY >= map.GetLength(1))
                    {
                        background += " ";
                    }
                    else
                    {
                        background += map[mapX, mapY];
                    }
                }
                background += "\n";
            }
            Draw(0, 0, ConsoleColor.White, background);
        }

        private static void Draw(int left, int top, ConsoleColor color, string output)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
            Console.WriteLine(output);
        }
    }
}