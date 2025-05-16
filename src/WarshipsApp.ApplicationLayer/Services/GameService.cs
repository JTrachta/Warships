using WarshipsApp.ApplicationLayer.Services.Abstractions;
using WarshipsApp.DomainLayer.Models;
using WarshipsApp.InterfaceLayer.Dtos;

namespace WarshipsApp.ApplicationLayer.Services
{
    public class GameService : IGameService
    {
        private GameSession? _gameSession;

        public ServerResponse StartGame(StartGameRequest startGameRequest)
        {
            if (string.IsNullOrEmpty(startGameRequest?.NamePlayer1) || string.IsNullOrEmpty(startGameRequest?.NamePlayer2))
            {
                throw new ArgumentException("Player names cannot be null or empty.");
            }

            if (startGameRequest.MapSize < 10 || startGameRequest.MapSize > 20)
            {
                throw new ArgumentOutOfRangeException("Map size must be between 10 and 20.");
            }

            _gameSession = new GameSession(startGameRequest.NamePlayer1, startGameRequest.NamePlayer2, startGameRequest.MapSize);

            return new ServerResponse
            {
                ServerResponseMessage = $"Game started successfully. Player {_gameSession.Players[_gameSession._currentPlayerIndex].PlayerName} beggins"
            };
        }

        public ServerResponse Fire(FireRequest fireRequest)
        {
            if (_gameSession == null)
            {
                throw new InvalidOperationException("Game session is not started. Please start a game first.");
            }

            if (fireRequest.XPosition < 0 || fireRequest.XPosition >= _gameSession.MapSize ||
                fireRequest.YPosition < 0 || fireRequest.YPosition >= _gameSession.MapSize)
            {
                throw new ArgumentException("Invalid coordinates. Please provide valid coordinates.");
            }

            var responseMessage = string.Empty;
            var currentPlayer = _gameSession.Players[_gameSession._currentPlayerIndex];
            var opponentPlayer = _gameSession.Players[(_gameSession._currentPlayerIndex + 1) % 2];

            var hitSpot = opponentPlayer.GamePlan.Map[fireRequest.XPosition, fireRequest.YPosition];

            if (hitSpot.IsOccupied)
            {
                var strickenShip = opponentPlayer.Ships.First(s => s.HullSections.Any(h => h.Id == hitSpot.ShipSectionId));

                strickenShip.HullSections.First(h => h.Id == hitSpot.ShipSectionId).IsHit = true;
                strickenShip.IsDestroyed = strickenShip.HullSections.All(h => h.IsHit);

                if (!strickenShip.IsDestroyed)
                {
                    //ship was hit but not destroyed, current player can fire again
                    responseMessage = $"{currentPlayer.PlayerName} hit {opponentPlayer.PlayerName}'s ship! {opponentPlayer.PlayerName} can fire again.";
                }
                else if (strickenShip.IsDestroyed && opponentPlayer.Ships.All(s => s.IsDestroyed))
                {
                    //ship was hit and destroyed, oponent has no ships left, end the game
                    responseMessage = $"{currentPlayer.PlayerName} hit and destroyed {opponentPlayer.PlayerName}'s ship! {opponentPlayer.PlayerName} wins the game!";
                    _gameSession = null; 
                }
                else
                {
                    //ship was hit and destroyed, opnent still has ships left, current player can fire again
                    responseMessage = $"{currentPlayer.PlayerName} hit and destroyed {opponentPlayer.PlayerName}'s {strickenShip.Type}! {currentPlayer.PlayerName} can fire again.";
                }
            }
            else
            {
                //missed shot , opponent player can fire
                responseMessage = $"{currentPlayer.PlayerName} missed {opponentPlayer.PlayerName}'s ship! Next Player is {opponentPlayer.PlayerName}.";
                _gameSession._currentPlayerIndex = (_gameSession._currentPlayerIndex + 1) % 2;
            }

            return new ServerResponse
            {
                ServerResponseMessage = responseMessage
            };
        }
    }
}
