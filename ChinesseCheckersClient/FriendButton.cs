using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChinesseCheckersClient
{
    class FriendButton : Button
    {
        private int idPlayer;
        private string nickname;
        private string email;

        public int IdPlayer
        {
            get { return idPlayer; }
            set { idPlayer = value; }
        }
        public string Nickname{
            get { return nickname; }
            set { nickname = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }
}
