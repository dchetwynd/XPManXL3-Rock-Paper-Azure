using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            while (true)
            {
                string ourMove = string.Empty;

                if (OpponentOnlyPlaysDynamite())
                    ourMove = AllowedMoves.Waterbomb;
                else
                {
                    if (CanWePlayDynamite())
                        ourMove = GetOurNextMoveWithDynamiteAllowed();
                    else
                        ourMove = GetOurNextMoveWithDynamiteNotAllowed();
                }

                if (ourMove == AllowedMoves.Dynamite)
                    OurDynamiteCount++;

                Console.WriteLine(ourMove);

                string opponentMove = Console.ReadLine();

                OpponentMoves.Add(opponentMove);
                OurMoves.Add(ourMove);
            }
        }

        private static string GetOurNextMoveWithDynamiteNotAllowed()
        {
            if (OpponentOnlyPlaysRock())
                return AllowedMoves.Paper;
            else if (OpponentOnlyPlaysPaper())
                return AllowedMoves.Scissors;
            else if (OpponentOnlyPlaysScissors())
                return AllowedMoves.Rock;
            else if (OpponentRarestMoveIsRock())
                return AllowedMoves.Scissors;
            else if (OpponentRarestMoveIsPaper())
                return AllowedMoves.Rock;
            else
                return AllowedMoves.Paper;
        }

        private static string GetOurNextMoveWithDynamiteAllowed()
        {
            if (!OurMoves.Contains(AllowedMoves.Dynamite))
                return AllowedMoves.Dynamite;
            else
            {
                if ((GetOurLastMove() != AllowedMoves.Dynamite) || OpponentLastMoveWasNotWaterbomb())
                    return AllowedMoves.Dynamite;
                else
                    return GenerateRandomMoveWithoutDynamite();
            }
        }

        private static bool OpponentLastMoveWasNotWaterbomb()
        {
            return (OpponentMoves.Count > 0)
                   && OpponentMoves[OpponentMoves.Count - 1] != AllowedMoves.Waterbomb;
        }

        private static string GenerateRandomMoveWithoutDynamite()
        {
            Random random = new Random();
            int moveIndex = random.Next(4);
            return MovesWithoutDynamite[moveIndex];
        }

        private static bool OpponentRarestMoveIsPaper()
        {
            return (GetOpponentPaperCount() < GetOpponentRockCount())
                && (GetOpponentPaperCount() < GetOpponentScissorsCount());
        }

        private static bool OpponentRarestMoveIsRock()
        {
            return (GetOpponentRockCount() < GetOpponentPaperCount())
                && (GetOpponentRockCount() < GetOpponentScissorsCount());
        }

        private static int GetOpponentScissorsCount()
        {
            return OpponentMoves.Count(x => x == AllowedMoves.Scissors);
        }

        private static int GetOpponentPaperCount()
        {
            return OpponentMoves.Count(x => x == AllowedMoves.Paper);
        }

        private static int GetOpponentRockCount()
        {
            return OpponentMoves.Count(x => x == AllowedMoves.Rock);
        }

        private static bool OpponentOnlyPlaysScissors()
        {
            return OpponentMoves.All(x => x == AllowedMoves.Scissors);
        }

        private static bool OpponentOnlyPlaysPaper()
        {
            return OpponentMoves.All(x => x == AllowedMoves.Paper);
        }

        private static bool OpponentOnlyPlaysRock()
        {
            return OpponentMoves.All(x => x == AllowedMoves.Rock);
        }

        private static bool OpponentOnlyPlaysDynamite()
        {
            return OpponentMoves.All(x => x == "dynamite");
        }

        private static bool CanWePlayDynamite()
        {
            return OurDynamiteCount < 100;
        }

        private static string GetOurLastMove()
        {
            return (OurMoves.Count > 0) ? OurMoves[OurMoves.Count - 1] : string.Empty;
        }

        public static List<string> MovesWithoutDynamite = new List<string>()
                                                              {
                                                                  AllowedMoves.Rock,
                                                                  AllowedMoves.Paper,
                                                                  AllowedMoves.Scissors,
                                                                  AllowedMoves.Waterbomb
                                                              };

        public static List<string> OurMoves = new List<string>();
        public static List<string> OpponentMoves = new List<string>();
        public static int OurDynamiteCount = 0;
    }
}
