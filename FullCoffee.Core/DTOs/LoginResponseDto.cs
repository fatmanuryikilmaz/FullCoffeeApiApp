﻿namespace FullCoffee.Core.DTOs
{
    public class LoginResponseDto
    {
        public string Token {  get; set; }
        public DateTime Expiration {  get; set; }
    }
}
