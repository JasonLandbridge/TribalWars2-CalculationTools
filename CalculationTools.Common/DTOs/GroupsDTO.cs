using CalculationTools.Common.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CalculationTools.Common
{
    public class GroupsDTO
    {

        [JsonProperty("groups")]
        public IList<GroupDTO> Groups { get; set; }

        public IList<IGroup> ToIGroupList()
        {
            return Groups.Cast<IGroup>().ToList();
        }
    }
}
