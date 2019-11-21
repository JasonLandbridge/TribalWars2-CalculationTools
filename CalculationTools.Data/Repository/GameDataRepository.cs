using AutoMapper;
using CalculationTools.Common;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationTools.Data
{
    public class GameDataRepository : IGameDataRepository
    {
        #region Fields

        private readonly IMapper _mapper;
        private readonly ISocketRepository _socketRepository;

        private bool _isConnected = false;


        #endregion Fields

        #region Constructors

        public GameDataRepository(ISocketRepository socketRepository, IMapper mapper)
        {
            _mapper = mapper;
            _socketRepository = socketRepository;

            // Keep track of the connection status
            DataEvents.ConnectionStatus += (sender, b) => { _isConnected = b; };
        }

        #endregion Constructors


        #region Properties
        public bool IsInUnitTestMode { get; set; }
        public DbContextOptions<CalculationToolsDBContext> DbContextOptions { get; set; }

        #endregion

        #region Methods

        public void DeleteDB()
        {
            using (var db = GetDBContext())
            {
                db.Database.EnsureDeleted();
            }
        }

        private CalculationToolsDBContext GetDBContext()
        {
            if (IsInUnitTestMode)
            {
                if (DbContextOptions != null)
                {
                    return new CalculationToolsDBContext(DbContextOptions);
                }
                throw new ArgumentNullException(nameof(DbContextOptions),
                    "Make sure to first provide a valid DBContextOptions to use this repository in unit testing.");
            }
            return new CalculationToolsDBContext();

        }


        #region Accounts


        public Account AddAccount()
        {
            using (var db = GetDBContext())
            {
                Account account = new Account
                {
                    // By default create a Dutch server
                    OnServerId = GetServer("nl").Id
                };

                db.Entry(account.OnServer).State = EntityState.Modified;
                db.Accounts.Add(account);
                db.SaveChanges();
                return account;
            }
        }

        public void UpdateAccount(Account account)
        {
            if (account == null) { return; }
            using (var db = GetDBContext())
            {
                if (account.DefaultCharacter != null)
                    db.Attach(account.DefaultCharacter);

                if (account.OnServer != null)
                    db.Attach(account.OnServer);

                db.Accounts.Update(account);
                db.SaveChanges();
            }
        }

        public bool DeleteAccount(int accountId)
        {
            if (accountId > 0)
            {
                using (var db = GetDBContext())
                {
                    Account account = db.Accounts.FirstOrDefault(a => a.Id == accountId);
                    if (account != null)
                    {
                        db.Accounts.Remove(account);
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteAccount(Account account)
        {
            if (account == null) { return false; }

            return DeleteAccount(account.Id);
        }

        /// <summary>
        /// Returns the account with the given internal AccountOwnerId, not with the TW2 given id!
        /// </summary>
        /// <param name="accountId">The AccountOwnerId connected to the account</param>
        /// <param name="tw2AccountId">Is the passed id from TW2?</param>
        /// <returns>The account associated with the Id</returns>
        public Account GetAccount(int accountId, bool tw2AccountId = false)
        {
            var list = GetAccounts();
            if (tw2AccountId)
            {
                return list.Count > 0 ? list.FirstOrDefault(a => a.TW2AccountID == accountId) : null;
            }
            return list.Count > 0 ? list.FirstOrDefault(a => a.Id == accountId) : null;
        }

        /// <summary>
        /// Returns the account with the given username.
        /// </summary>
        /// <param name="username">The username connected to the account</param>
        /// <returns></returns>
        public Account GetAccount(string username)
        {
            var list = GetAccounts();
            return list.Count > 0 ? list.FirstOrDefault(a => a.Username == username) : null;
        }



        public List<Account> GetAccounts(bool onlyConfirmed = false)
        {
            using (var db = GetDBContext())
            {
                var list = db.Accounts
                    .Include(a => a.OnServer)
                    .Include(a => a.DefaultCharacter)
                    .ThenInclude(a => a.World)
                    .Include(a => a.CharacterList)
                    .ThenInclude(a => a.World)
                    .ToList();

                return onlyConfirmed ? list.Where(a => a.IsConfirmed).ToList() : list;
            }
        }

        #endregion

        #region GetWorld

        public World GetWorld(string worldId)
        {
            using (var db = GetDBContext())
            {
                return db.Worlds
                    .Include(x => x.Characters)
                    .Include(x => x.OnServer)
                    .FirstOrDefault(a => a.WorldId == worldId);
            }
        }

        #endregion

        #region Servers

        public List<Server> GetServers()
        {
            using (var db = GetDBContext())
            {
                return db.Servers
                    .AsNoTracking()
                    .ToList();
            }
        }

        #endregion

        public Character GetCharacterById(int id, bool tw2CharacterId = false)
        {
            using (var db = GetDBContext())
            {
                if (tw2CharacterId)
                {
                    return db.Characters
                        .Include(x => x.AccountOwner)
                        .Include(x => x.World)
                        .FirstOrDefault(a => a.CharacterId == id);
                }

                return db.Characters
                    .Include(x => x.AccountOwner)
                    .Include(x => x.World)
                    .FirstOrDefault(a => a.Id == id);
            }
        }

        public Server GetServer(string serverCode)
        {
            if (serverCode.IsNullOrEmpty() || serverCode.Length != 2) { return null; }

            using (var db = GetDBContext())
            {
                return db.Servers.FirstOrDefault(a => a.Id == serverCode);
            }

        }

        public void UpdateGroups(List<IGroup> groupList)
        {
            //if (groupList.Count == 0) { return; }

            //foreach (IGroup group in groupList)
            //{
            //    using (var db = GetDBContext())
            //    {
            //        var groupEntity = _mapper.Map<Group>(group);

            //        db.Attach(groupEntity);
            //        db.Entry(groupEntity).Property("CharacterId").CurrentValue = group.CharacterId;
            //        db.Groups.Add(groupEntity);

            //        db.SaveChanges();
            //    }
            //}
        }

        #region Village
        public bool UpdateVillages(List<IVillage> villages)
        {
            if (villages.Count == 0) { return false; }

            using (var db = GetDBContext())
            {

                // It is assumed that all incoming villages always are on the same world.
                var world = new World();
                if (!string.IsNullOrEmpty(villages[0].WorldId))
                {
                    world = db.Worlds.Find(villages[0].WorldId);
                }

                foreach (IVillage village in villages)
                {
                    var villageEntity = _mapper.Map<Village>(village);

                    int characterId = villageEntity.CharacterId ?? default;

                    var entity = db.Villages.FirstOrDefault(item => item.Id == villageEntity.Id);
                    if (entity != null && entity != villageEntity)
                    {
                        db.Update(entity);

                        // Make changes on entity
                        entity.Name = villageEntity.Name;
                        entity.X = villageEntity.X;
                        entity.Y = villageEntity.Y;
                        entity.Points = villageEntity.Points;
                    }
                    else
                    {
                        entity = villageEntity;
                        db.Add(entity);
                    }

                    //Relationships
                    entity.World = world;
                    entity.WorldId = world.WorldId;

                    if (entity.CharacterId != null)
                    {
                        entity.Character = db.Characters.FirstOrDefault(
                            x => x.CharacterId == characterId &&
                                 x.WorldId == entity.WorldId);
                    }

                }

                db.SaveChanges();
            }

            DataEvents.InvokeVillagesUpdated();

            return true;
        }

        public List<Village> GetVillages(int characterId = 0)
        {
            using (var db = GetDBContext())
            {
                if (characterId > 0)
                {
                    return db.Villages.Where(x => x.CharacterId == characterId).ToList();
                }
                else
                {
                    return db.Villages.ToList();
                }

            }
        }

        public async Task<List<Village>> GetVillagesByAutocompleteAsync(string villageName)
        {
            if (string.IsNullOrEmpty(villageName)) { return new List<Village>(); }

            if (_isConnected)
            {
                var villages = await _socketRepository.GetVillagesByAutocomplete(villageName);

                UpdateVillages(villages);
            }

            using (var db = GetDBContext())
            {
                return db.Villages.Where(x => x.Name.StartsWith(villageName)).ToList();
            }
        }


        #endregion
        public void UpdateLoginData(ILoginData loginData)
        {
            using (var db = GetDBContext())
            {
                // Update the logged in account
                Account account = GetAccount(loginData.Name);
                if (account != null)
                {
                    db.Attach(account);
                    account.TW2AccountID = loginData.PlayerId;
                    account.IsConfirmed = true;
                    db.SaveChanges();
                }
            }

            ParseWorlds(loginData);

            ParseCharacters(loginData);
        }

        private void ParseCharacters(ILoginData loginData)
        {
            // Parse the characters
            List<Character> characterList = new List<Character>();
            foreach (ICharacter character in loginData.Characters)
            {
                characterList.Add(_mapper.Map<Character>(character));
            }

            if (characterList.Count > 0)
            {
                // Add or update the worlds 
                using (var db = GetDBContext())
                {
                    foreach (Character character in characterList)
                    {
                        var entity = db.Characters.FirstOrDefault(
                            item => item.CharacterId == character.CharacterId &&
                                    item.WorldId == character.WorldId);

                        if (entity != null)
                        {
                            // Make changes on entity
                            entity.CharacterName = character.CharacterName;
                            entity.CharacterOwnerId = character.CharacterOwnerId;
                            entity.CharacterOwnerName = character.CharacterOwnerName;
                            entity.AllowLogin = character.AllowLogin;
                            entity.WorldId = character.WorldId;

                            Account accountOwner = GetAccount(character.CharacterOwnerId, true);
                            if (accountOwner != null)
                            {
                                character.AccountOwnerId = accountOwner.Id;
                            }

                        }
                        else
                        {
                            db.Attach(character);
                            Account accountOwner = GetAccount(character.CharacterOwnerId, true);
                            if (accountOwner != null)
                            {
                                character.AccountOwnerId = accountOwner.Id;
                            }
                            db.Characters.Add(character);
                        }
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                //TODO add logger here
            }
        }

        private void ParseWorlds(ILoginData loginData)
        {
            List<World> worldList = new List<World>();

            // TW2 regards used worlds by the player as characters and empty worlds as world objects
            // This will parse the world data from a characterDTO to a world entities.
            foreach (IWorld world in loginData.Worlds)
            {
                worldList.Add(_mapper.Map<World>(world));
            }
            foreach (ICharacter character in loginData.Characters)
            {
                worldList.Add(_mapper.Map<World>(character));
            }

            // Filter on unique WorldCode, they are duplicates anyway.
            worldList = worldList.GroupBy(p => p.WorldId)
                .Select(g => g.First())
                .ToList();
            if (worldList.Count > 0)
            {
                // Add or update the worlds 
                using (var db = GetDBContext())
                {
                    foreach (World world in worldList)
                    {
                        var entity = db.Worlds.FirstOrDefault(item => item.WorldId == world.WorldId);
                        if (entity != null)
                        {
                            // Make changes on entity
                            entity.Name = world.Name;
                            entity.Full = world.Full;
                            entity.KeyRequired = world.KeyRequired;
                            entity.Maintenance = world.Maintenance;
                            entity.OnServerId = GetServer(world.WorldId.Substring(0, 2)).Id;
                        }
                        else
                        {
                            db.Attach(world);
                            world.OnServerId = GetServer(world.WorldId.Substring(0, 2)).Id;
                            db.Worlds.Add(world);
                        }
                        db.SaveChanges();
                    }
                }
            }
            else
            {
                //TODO add logger here
            }
        }

        #endregion Methods

        #region Methods



        #endregion
    }
}
