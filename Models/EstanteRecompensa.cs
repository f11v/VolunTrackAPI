using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class EstanteRecompensa
{
    public int IdEstanteRecompensas { get; set; }

    public int? IdRecompensa { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Recompensa? IdRecompensaNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
