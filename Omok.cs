using System;

namespace 바둑2
{
    enum EStoneColor
    {
        Black = '○',
        White = '●'
    };
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0, y = 0;
            int player = 0; // 턴 증가시키기
            Random random = new Random();
            char[,] omok = new char[19, 19]; //19x19 오목판 이차원 배열
            while (true)
            {
                ResetOmok(omok);
                Console.Clear();
                PrintOmok(omok);
                while (true)
                {
                    if (player % 2 == 0) //흑돌턴 (플레이어)
                    {
                        do
                        {
                            GetBlackxy(ref x, ref y);
                        } while (Judgexy(x, y, omok) == false);
                        if (x == 99 && y == 99)
                        {
                            break;
                        }
                        omok[x - 1, y - 1] = (char)EStoneColor.Black;
                        Console.Clear();
                        PrintOmok(omok);
                        if (JudgeWinner(omok, (char)EStoneColor.Black) == true)
                        {
                            break;
                        }
                    }
                    else //백돌턴 (컴퓨터)
                    {
                        do
                        {
                            int computerX = random.Next(1, 20);
                            int computerY = random.Next(1, 20);
                            x = computerX;
                            y = computerY;
                        } while (Judgexy(x, y, omok) == false);
                        omok[x - 1, y - 1] = (char)EStoneColor.White;
                        Console.Clear();
                        PrintOmok(omok);
                        if (JudgeWinner(omok, (char)EStoneColor.White) == true)
                        {
                            break;
                        }
                        Console.WriteLine($"●'s X Y: {x} {y}");
                    }
                    player += 1;
                }
                if (x == 99 && y == 99)
                {
                    break;
                }
                Console.Write("Play again? (y/n): ");
                string again = Console.ReadLine();
                if (again == "y")
                {
                    player += 1;
                    continue;
                }
                else
                {
                    break;
                }
            }
        }
        static void ResetOmok(char[,] omok) //배열에 기호 넣기
        {
            omok[0, 0] = '┌';
            for (int i = 1; i < 18; i++)
            {
                omok[i, 0] = '┬';
            }
            omok[18, 0] = '┐';
            for (int j = 1; j < 18; j++)
            {
                omok[0, j] = '├';
                for (int i = 1; i < 18; i++)
                {
                    omok[i, j] = '┼';
                }
                omok[18, j] = '┤';
            }
            omok[0, 18] = '└';
            for (int i = 1; i < 18; i++)
            {
                omok[i, 18] = '┴';
            }
            omok[18, 18] = '┘';
        }
        static void PrintOmok(char[,] omok) //배열 이용한 오목판 출력 함수
        {
            Console.WriteLine("      ======Let's Play Omok!======\n"); //오목판 출력
            Console.Write("  ");
            for (int i = 1; i <= 19; i++) //맨위 숫자들 출력
            {
                Console.Write("{0,2}", i);
            }
            Console.WriteLine("");

            for (int j = 0; j < omok.GetLength(0); j++)
            {
                Console.Write("{0,2}", j + 1);
                for (int i = 0; i < omok.GetLength(1); i++)
                {
                    Console.Write($"{omok[i, j]}");
                }
                Console.WriteLine();
            }
        }
        static void GetBlackxy(ref int x, ref int y) //플레이어 x,y값 받는 함수
        {
            Console.Write($"○'s X Y: ");
            string playerXY = Console.ReadLine();
            string[] xy = playerXY.Split(' ');
            x = int.Parse(xy[0]);
            y = int.Parse(xy[1]);
        }
        static bool Judgexy(int x, int y, char[,] omok) //x,y값이 범위를 안벗어났는지 판단하는 함수
        {
            if (x == 99 && y == 99)
            {
                return true;
            }
            if (x < 1 || y < 1 || x > 19 || y > 19 || omok[x - 1, y - 1] == '○' || omok[x - 1, y - 1] == '●')
            {
                Console.WriteLine("Can not put there. Try again.");
                return false;
            }
            else
            {
                return true;
            }
        }
        static bool JudgeWinner(char[,] omok, char omokColor) //승리 조건 달성확인 함수
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (omok[i, j] == omokColor
                        && omok[i, j + 1] == omokColor
                        && omok[i, j + 2] == omokColor
                        && omok[i, j + 3] == omokColor
                        && omok[i, j + 4] == omokColor)
                    {
                        Console.WriteLine($"{omokColor} win.");
                        return true;
                    }
                    else if (omok[i, j] == omokColor
                        && omok[i + 1, j] == omokColor
                        && omok[i + 2, j] == omokColor
                        && omok[i + 3, j] == omokColor
                        && omok[i + 4, j] == omokColor)
                    {
                        Console.WriteLine($"{omokColor} win.");
                        return true;
                    }
                    else if (omok[i, j] == omokColor
                        && omok[i + 1, j + 1] == omokColor
                        && omok[i + 2, j + 2] == omokColor
                        && omok[i + 3, j + 3] == omokColor
                        && omok[i + 4, j + 4] == omokColor)
                    {
                        Console.WriteLine($"{omokColor} win.");
                        return true;
                    }
                    else if (omok[i + 4, j] == omokColor
                        && omok[i + 3, j + 1] == omokColor
                        && omok[i + 2, j + 2] == omokColor
                        && omok[i + 1, j + 3] == omokColor
                        && omok[i, j + 4] == omokColor)
                    {
                        Console.WriteLine($"{omokColor} win.");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}