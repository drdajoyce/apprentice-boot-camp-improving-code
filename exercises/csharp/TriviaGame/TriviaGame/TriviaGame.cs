using System;
using System.Collections.Generic;
using System.Linq;

namespace TriviaGame
{
    public class TriviaGame
    {
        List<string> players = new List<string>();

        int[] playerLocations = new int[6];
        int[] playerPurses = new int[6];

        bool[] inPenaltyBox = new bool[6];

        LinkedList<string> popQuestions = new LinkedList<string>();
        LinkedList<string> scienceQuestions = new LinkedList<string>();
        LinkedList<string> sportsQuestions = new LinkedList<string>();
        LinkedList<string> rockQuestions = new LinkedList<string>();

        int currentPlayer = 0;
        bool isGettingOutOfPenaltyBox;

        public TriviaGame()
        {
            for (int i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsGamePlayable()
        {
            return (NumberOfPlayers() >= 2);
        }

        public bool AddPlayer(string playerName)
        {


            players.Add(playerName);
            playerLocations[NumberOfPlayers()] = 0;
            playerPurses[NumberOfPlayers()] = 0;
            inPenaltyBox[NumberOfPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int NumberOfPlayers()
        {
            return players.Count;
        }

        public void RollDice(int roll)
        {
            Console.WriteLine(players[currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(players[currentPlayer] + " is getting out of the penalty box");
                    playerLocations[currentPlayer] = playerLocations[currentPlayer] + roll;
                    if (playerLocations[currentPlayer] > 11) playerLocations[currentPlayer] = playerLocations[currentPlayer] - 12;

                    Console.WriteLine(players[currentPlayer]
                            + "'s new location is "
                            + playerLocations[currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                playerLocations[currentPlayer] = playerLocations[currentPlayer] + roll;
                if (playerLocations[currentPlayer] > 11) playerLocations[currentPlayer] = playerLocations[currentPlayer] - 12;

                Console.WriteLine(players[currentPlayer]
                        + "'s new location is "
                        + playerLocations[currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(popQuestions.First());
                popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(scienceQuestions.First());
                scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(sportsQuestions.First());
                sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(rockQuestions.First());
                rockQuestions.RemoveFirst();
            }
        }


        private string CurrentCategory()
        {
            if (playerLocations[currentPlayer] == 0) return "Pop";
            if (playerLocations[currentPlayer] == 4) return "Pop";
            if (playerLocations[currentPlayer] == 8) return "Pop";
            if (playerLocations[currentPlayer] == 1) return "Science";
            if (playerLocations[currentPlayer] == 5) return "Science";
            if (playerLocations[currentPlayer] == 9) return "Science";
            if (playerLocations[currentPlayer] == 2) return "Sports";
            if (playerLocations[currentPlayer] == 6) return "Sports";
            if (playerLocations[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasQuestionAnsweredCorrectly()
        {
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    playerPurses[currentPlayer]++;
                    Console.WriteLine(players[currentPlayer]
                            + " now has "
                            + playerPurses[currentPlayer]
                            + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                else
                {
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                playerPurses[currentPlayer]++;
                Console.WriteLine(players[currentPlayer]
                        + " now has "
                        + playerPurses[currentPlayer]
                        + " Gold Coins.");

                bool winner = DidPlayerWin();
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(playerPurses[currentPlayer] == 6);
        }
    }

}