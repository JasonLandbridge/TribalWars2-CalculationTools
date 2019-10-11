using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CalculationTools.WebSocket
{
    public static class PlayerData
    {
        public static DateTime LastUpdated { get; set; }

        public static bool IsLoggedIn { get; set; }
        public static int PlayerId { get; set; }
        public static string Name { get; set; }



    }
}
