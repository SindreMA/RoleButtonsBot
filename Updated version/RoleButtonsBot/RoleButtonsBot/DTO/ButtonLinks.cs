using System;
using System.Collections.Generic;
using System.Text;

namespace RoleButtonsBot.DTO
{
    public class ButtonLinks
    {
        public ulong MessageID { get; set; }
        public ulong RoleID { get; set; }
        public string Emoji { get; set; }
        public ulong CreatedByID { get; set; }
        public DateTime Created { get; set; }
        public int ID { get; set; }
        public ulong GuildID { get; set; }
    }
}
