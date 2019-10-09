using System;
using System.Collections.Generic;
using System.Text;

namespace CalculationTools.WebSocket
{
    public static class PlayerData
    {
        public static int PlayerId { get; set; }
        public static string Name { get; set; }

        public static bool IsLoggedIn { get; set; }
    }
}
