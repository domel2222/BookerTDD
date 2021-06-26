﻿using System;

namespace Booker.Modals
{
    public abstract class BaseBooking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateTime { get; set; }
    }
}