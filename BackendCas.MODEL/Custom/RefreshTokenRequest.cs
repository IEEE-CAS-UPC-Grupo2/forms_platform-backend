﻿namespace BackendCas.MODEL.Custom;

public class RefreshTokenRequest
{
    public string ExpiredToken { get; set; }
    public string RefreshToken { get; set; }
}