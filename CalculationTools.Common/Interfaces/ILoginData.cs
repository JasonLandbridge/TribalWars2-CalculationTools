using System.Collections.Generic;

namespace CalculationTools.Common
{
    public interface ILoginData
    {
        int PlayerId { get; set; }
        string Name { get; set; }
        IList<ICharacter> Characters { get; }
        IList<IWorld> Worlds { get; }
        int ServerTimestamp { get; set; }
    }
}