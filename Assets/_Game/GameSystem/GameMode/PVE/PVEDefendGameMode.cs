using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOK1game.Game
{
    [Serializable]
    public class PVEDefendGameMode : BasePVEGameMode
    {
        public PVEDefendGameMode(EGameModeId id)
        {
            _id = id;
        }
    }
}
