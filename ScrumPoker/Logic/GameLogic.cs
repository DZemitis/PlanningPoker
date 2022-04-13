using System.Runtime.Versioning;
using ScrumPoker.Core.Models;

namespace ScrumPoker.Logic;

public static class GameLogic
{
    public static List<Player> Players = new List<Player>();
    public static List<VotingResults> PlayerVotes = new List<VotingResults>();

    public static List<VotingResults> SetResult()
    {
        foreach (var player in Players)
        {
            PlayerVotes.Add(new VotingResults() {PlayerName = player.Name, Vote = player.Vote});
        }
        
        return PlayerVotes;
    }

    public static void AddPlayerData(Player userInput)
    {
        Players.Add(userInput);
    }

    public static void RestartVoting()
    {
        Players.Clear();
        PlayerVotes.Clear();
    }
}