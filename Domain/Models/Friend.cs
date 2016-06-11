using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Models
{
    public class Friend
    {
        public int FriendId { get; set; }

        public bool Accepted { get; set; }

        public int UserIntSenderId { get; set; }
        public virtual UserInt UserIntSender { get; set; }

        public int UserIntReceiverId { get; set; }
        public virtual UserInt UserIntReceiver { get; set; }

        public DateTime Added { get; set; }
    }
}
