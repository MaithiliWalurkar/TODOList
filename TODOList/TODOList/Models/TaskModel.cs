using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TODOList.Models
{
    public class TaskModel
    {
        public int CatId { get; set; }
        
        public string Task { get; set; }

        public string Category { get; set; }
    }
}