using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Domain.Entities
{
    public class PLAYER
    {
        public int PLAYER_ID { get; set; }
        public required string PLAYER_NO { get; set; }
        public required string PLAYER_NAME { get; set; }
        public string? PLAYER_PROFILE { get; set; }
        public int CONTRACT_TYPE_ID { get; set; }
        public required string CONTRACT_TYPE_CODE { get; set; }
        public required string CONTRACT_TYPE_NAME { get; set; }
        public int TRANSFER_STATUS_ID { get; set; }
        public required string TRANSFER_STATUS_CODE { get; set; }
        public required string TRANSFER_STATUS_NAME { get; set; }
        public bool IS_ACTIVE { get; set; }
        public bool IS_DELETE { get; set; }
        public int CREATED_BY_ID { get; set; }
        public DateTime CREATED_DATE { get; set; }
        public int LAST_UPDATE_ID { get; set; }
        public DateTime LAST_UPDATE_DATE { get; set; }
        public System.Guid ROW_UN { get; set; }
    }
}
