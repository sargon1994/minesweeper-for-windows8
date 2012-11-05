using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MineSweeper.View
{
    interface ContextSensitive
    {
        void setContext(ViewContext context);
    }
}
