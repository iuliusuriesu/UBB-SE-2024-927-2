using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.Entities
{
    public interface IBid
    {
        DateTime BidDateTime { get; set; }
        int BidId { get; set; }
        float BidSum { get; set; }
        User BidUser { get; set; }
    }
}