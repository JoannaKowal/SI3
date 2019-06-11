using System.Collections;
using System.Collections.Generic;

namespace SI3Backend
{
    public interface Heuristic
    {
        double Evaluate(GameState gameState);
    }
}