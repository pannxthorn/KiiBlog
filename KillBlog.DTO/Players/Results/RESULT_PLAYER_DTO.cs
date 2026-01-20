using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillBlog.DTO.Players
{
    public class RESULT_PLAYER_DTO
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

        [SetsRequiredMembers]
        public RESULT_PLAYER_DTO() { }
    }
    
}
