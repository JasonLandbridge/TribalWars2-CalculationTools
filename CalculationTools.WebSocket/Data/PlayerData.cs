using System;
using System.Collections.Generic;
using System.Text;
using CalculationTools.WebSocket.Utilities;

namespace CalculationTools.WebSocket
{
    public static class PlayerData
    {
        public static DateTime LastUpdated { get; set; }
        public static int PlayerId { get; set; }
        public static string Name { get; set; }


        public static Result IsLoggedIn { get; set; } = Result.None;
    }
}
