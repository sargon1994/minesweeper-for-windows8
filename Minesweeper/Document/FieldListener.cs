using MineSweeperViewProject.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.Document
{
    public interface FieldListener
    {
        void fieldChanged(Field field);
        void gameEnded(Field field, bool win);
    }
}
