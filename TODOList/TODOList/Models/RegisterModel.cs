using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TODOList.Models
{
    public class RegisterModel
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public int Queid { get; set; }
        public string Answers { get; set; }
        //public string Questions { get; internal set; }
    }
}