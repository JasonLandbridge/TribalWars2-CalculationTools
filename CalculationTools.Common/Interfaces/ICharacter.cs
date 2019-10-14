namespace CalculationTools.Common
{
    public interface ICharacter
    {
        int CharacterId { get; set; }

        string CharacterName { get; set; }

        string WorldId { get; set; }

        string WorldName { get; set; }

        bool AllowLogin { get; set; }

        int CharacterOwnerId { get; set; }

        string CharacterOwnerName { get; set; }

    }
}