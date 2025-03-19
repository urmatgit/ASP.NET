﻿using PromoCodeFactory.Core.Domain;
using System;
using System.Collections.Generic;

namespace PromoCodeFactory.Core.Domain.PromoCodeManagement
{
    public class Customer
        : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        //TODO: Списки Preferences и Promocodes 
        //Many-To-many
        public List<CustomerPreference> Preferences { get; set; }
        //One-To-Many
        public List<PromoCode> PromoCodes { get; set; }
    }
}