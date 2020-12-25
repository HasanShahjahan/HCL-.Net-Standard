using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.TestCaller.Models
{
    public class CommonResponse
    {
        public bool is_request_success { get; set; }
        public int booking_id { get; set; }
        public List<int> assigned_lockers { get; set; }
        public string hardware_door_number { get; set; }
        public List<List<List<Compartment>>> locker_preview { get; set; }

    }
}
