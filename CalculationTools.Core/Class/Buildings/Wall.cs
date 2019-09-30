using System.Collections.Generic;

namespace ClassLibrary.Class.Buildings
{
    public class Wall : BaseBuilding
    {
        public override string Code { get; } = "Wall";

        public override List<int> HitpointList { get; } = new List<int>
        {
            3,
            3,
            4,
            4,
            4,
            5,
            5,
            6,
            6,
            7,
            8,
            9,
            9,
            10,
            11,
            13,
            14,
            15,
            17,
            18,
        };
    }
}