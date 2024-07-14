using System;
using System.Collections.Generic;

namespace BackendCas.MODEL;

public partial class Administrator
{
    public int IdAdministrator { get; set; }

    public string? NameAdministrator { get; set; }

    public string? EmailAdministrator { get; set; }

    public string? PasswordAdministrator { get; set; }

    public virtual ICollection<EventsCa> EventsCas { get; } = new List<EventsCa>();

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; } =
        new List<HistorialRefreshToken>();
}