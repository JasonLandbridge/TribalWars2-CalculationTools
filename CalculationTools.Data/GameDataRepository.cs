using AutoMapper;
using CalculationTools.Common;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Data
{
    public class GameDataRepository : IGameDataRepository
    {
        private readonly IMapper _mapper;

        public GameDataRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void UpdateWorlds(List<IWorld> worlds)
        {
            if (worlds.Count == 0) { return; }

            foreach (IWorld world in worlds)
            {
                using (var db = new CalculationToolsDBContext())
                {
                    var entity = db.Worlds.SingleOrDefault(item => item.WorldCode == world.WorldCode);

                    if (entity != null)
                    {
                        // Make changes on entity
                        entity.Name = world.Name;
                        entity.Full = world.Full;
                        entity.Server = GetServer(world.WorldCode.Substring(0, 2));
                    }
                    else
                    {
                        var dbEntity = _mapper.Map<World>(world);
                        dbEntity.Server = GetServer(world.WorldCode.Substring(0, 2));

                        db.Entry(dbEntity).State = EntityState.Modified;
                        db.Entry(dbEntity.Server).State = EntityState.Modified;

                        db.Worlds.Add(dbEntity);
                    }

                    db.SaveChanges();
                }

            }

        }
        public void UpdateWorlds(List<ICharacter> characters)
        {
            if (characters.Count == 0) { return; }

            // Keep these two loop separate, all characters need to
            // first be added before the worlds can be inserted
            foreach (ICharacter character in characters)
            {
                // The updated object
                Character charEntity = _mapper.Map<Character>(character);
                AddCharacter(charEntity);
            }

            foreach (ICharacter character in characters)
            {
                // The updated object
                World worldEntity = _mapper.Map<World>(character);
                Character charEntity = _mapper.Map<Character>(character);
                AddWorld(worldEntity, charEntity);
            }
        }

        public Character GetCharacterById(int id)
        {
            using (var db = new CalculationToolsDBContext())
            {
                return db.Characters.FirstOrDefault(a => a.CharacterId == id);
            }
        }

        public void AddCharacter(Character character)
        {
            if (character == null) { return; }

            using (var db = new CalculationToolsDBContext())
            {
                // first update or add character
                var charDBEntity = db.Characters.SingleOrDefault(a => a.CharacterId == character.CharacterId);

                if (charDBEntity != null)
                {
                    charDBEntity.CharacterName = character.CharacterName;
                    charDBEntity.CharacterOwnerId = character.CharacterOwnerId;
                    charDBEntity.CharacterOwnerName = character.CharacterOwnerName;
                }
                else
                {
                    db.Entry(character).State = EntityState.Modified;
                    db.Characters.Add(character);
                }

                db.SaveChanges();
            }
        }

        public void AddWorld(World world, Character character = null)
        {
            if (world == null) { return; }

            using (var db = new CalculationToolsDBContext())
            {

                // Add or update the worlds with the above character
                var worldDBEntity = db.Worlds
                    .Include(p => p.Character)
                    .SingleOrDefault(item => item.WorldCode == world.WorldCode);

                if (worldDBEntity != null)
                {
                    // Make changes on entity
                    worldDBEntity.Name = world.Name;
                    worldDBEntity.AllowLogin = world.AllowLogin;
                    worldDBEntity.KeyRequired = world.KeyRequired;
                    worldDBEntity.Full = world.Maintenance;
                    worldDBEntity.Server = GetServer(world.WorldCode.Substring(0, 2));
                    if (character != null)
                    {
                        worldDBEntity.Character = GetCharacterById(character.CharacterId);
                    }

                }
                else
                {
                    if (character != null)
                    {
                        world.Character = GetCharacterById(character.CharacterId);
                    }
                    world.Server = GetServer(world.WorldCode.Substring(0, 2));

                    db.Entry(world).State = EntityState.Modified;
                    db.Entry(world.Character).State = EntityState.Modified;
                    db.Entry(world.Server).State = EntityState.Modified;

                    db.Worlds.Add(world);
                }

                db.SaveChanges();
            }
        }


        public void UpdateVillages(List<IVillage> villages)
        {
            if (villages.Count == 0) { return; }

            using (var db = new CalculationToolsDBContext())
            {
                foreach (IVillage village in villages)
                {
                    db.Villages.Add(_mapper.Map<Village>(village));
                }
                db.SaveChanges();
            }
        }


        public Server GetServer(string serverCode)
        {
            if (serverCode.IsNullOrEmpty() || serverCode.Length != 2) { return null; }

            using (var db = new CalculationToolsDBContext())
            {
                return db.Servers.FirstOrDefault(a => a.ServerCountryCode == serverCode);
            }

        }
    }
}
