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
        #region Fields

        private readonly IMapper _mapper;
        #endregion Fields

        #region Constructors

        public GameDataRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        #endregion Constructors

        #region Methods

        #region WorldMethods

        public List<World> GetCharacterWorlds(int characterId)
        {
            List<World> result = new List<World>();

            //using (var db = new CalculationToolsDBContext())
            //{
            //    result = db.Worlds
            //        .Include(c => c.CharacterWorld)
            //        .ThenInclude(x => x.Character)
            //        .Where(i => i.CharacterWorld.Any(c => c.World == ) == characterId)

            //        .AsNoTracking().ToList();
            //}

            return result;
        }

        #endregion

        #region Accounts

        public Account AddAccount()
        {
            using (var db = new CalculationToolsDBContext())
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
            using (var db = new CalculationToolsDBContext())
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
                using (var db = new CalculationToolsDBContext())
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
            using (var db = new CalculationToolsDBContext())
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

        #region Servers

        public List<Server> GetServers()
        {
            using (var db = new CalculationToolsDBContext())
            {
                return db.Servers
                    .AsNoTracking()
                    .ToList();
            }
        }

        #endregion

        public Character GetCharacterById(int id)
        {
            using (var db = new CalculationToolsDBContext())
            {
                return db.Characters.FirstOrDefault(a => a.Id == id);
            }
        }

        public Server GetServer(string serverCode)
        {
            if (serverCode.IsNullOrEmpty() || serverCode.Length != 2) { return null; }

            using (var db = new CalculationToolsDBContext())
            {
                return db.Servers.AsTracking().FirstOrDefault(a => a.Id == serverCode);
            }

        }

        public void UpdateGroups(List<IGroup> groupList)
        {
            if (groupList.Count == 0) { return; }

            foreach (IGroup group in groupList)
            {
                using (var db = new CalculationToolsDBContext())
                {
                    var groupEntity = _mapper.Map<Group>(group);

                    db.Attach(groupEntity);
                    db.Entry(groupEntity).Property("CharacterId").CurrentValue = group.CharacterId;
                    db.Groups.Add(groupEntity);

                    db.SaveChanges();
                }
            }
        }

        public void UpdateVillages(List<IVillage> villages)
        {
            if (villages.Count == 0) { return; }

            foreach (IVillage village in villages)
            {
                using (var db = new CalculationToolsDBContext())
                {
                    var villageEntity = _mapper.Map<Village>(village);
                    db.Attach(villageEntity);
                    // db.Entry(villageEntity).Property("CharacterId").CurrentValue = group.CharacterId;

                    db.Villages.Add(villageEntity);
                    db.SaveChanges();
                }
            }
        }

        public void UpdateAccountData(ILoginData loginData)
        {
            using (var db = new CalculationToolsDBContext())
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
                using (var db = new CalculationToolsDBContext())
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


            // Parse the characters

            List<Character> characterList = new List<Character>();

            foreach (ICharacter character in loginData.Characters)
            {
                characterList.Add(_mapper.Map<Character>(character));
            }

            if (characterList.Count > 0)
            {
                // Add or update the worlds 
                using (var db = new CalculationToolsDBContext())
                {
                    foreach (Character character in characterList)
                    {
                        var entity = db.Characters.FirstOrDefault(item => item.Id == character.Id);

                        if (entity != null)
                        {
                            // Make changes on entity
                            entity.CharacterName = character.CharacterName;
                            entity.CharacterOwnerId = character.CharacterOwnerId;
                            entity.CharacterOwnerName = character.CharacterOwnerName;
                            entity.AllowLogin = character.AllowLogin;
                            entity.WorldId = character.WorldId;
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
        #endregion Methods

        #region Methods



        #endregion
    }
}
