using Microsoft.AspNetCore.Mvc;
using WarshipsApp.ApplicationLayer.Services.Abstractions;
using WarshipsApp.InterfaceLayer.Dtos;

namespace WarshipsApp.Controllers
{
    [ApiController]
    [Route("game")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        [Route("start")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        public IActionResult StartGame([FromBody] StartGameRequest startGameRequest)
        {
            return Ok(_gameService.StartGame(startGameRequest));
        }

        [HttpPost]
        [Route("fire")]
        [ProducesResponseType(typeof(ServerResponse), StatusCodes.Status200OK)]
        public IActionResult Fire([FromBody] FireRequest fireRequest)
        {
             return Ok(_gameService.Fire(fireRequest));
        }
    }
}
