using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdoptifySystem.Models
{
    public class KennelHistoryViewModel
    {
        public int Animal_Kennel_History_ID { get; set; }
        public Nullable<int> Animal_ID { get; set; }
        public Nullable<int> Kennel_ID { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual Kennel Kennel { get; set; }
    }
}