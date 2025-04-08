﻿namespace Modules.Authentication.API.Extensions
{
    public class KeycloakExtension
    {
        public string AuthorizationUrl { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string MetadataAddress { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }
}
