using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class UserSentNotificationViewModel
    {
        public int ID { get; set; }
        public string TitleNotification { get; set; }
        public string LevelEmergency { get; set; }
        public DateTime ?DateCreated { get; set; }
        public string Location { get; set; }
    }
}