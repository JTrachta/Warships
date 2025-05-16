using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarshipsApp.InterfaceLayer.Dtos;

namespace WarshipsApp.ApplicationLayer.Services.Abstractions
{
    public interface IGameService
    {
        ServerResponse StartGame(StartGameRequest startGameRequest);
        ServerResponse Fire(FireRequest fireRequest);
    }
}
