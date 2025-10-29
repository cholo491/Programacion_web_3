﻿using Microsoft.AspNetCore.Identity;

namespace NetIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime FechaNacimiento { get; set; }
        public string? NombreCompleto { get; set; }
    }

}
