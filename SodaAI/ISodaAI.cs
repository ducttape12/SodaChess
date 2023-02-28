using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaAI
{
    public interface ISodaAI
    {
        public AIMove GetMoveForCurrentPlayer();
    }
}
