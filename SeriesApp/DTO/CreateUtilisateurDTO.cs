using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesApp.Models;

namespace SeriesApp.DTO;
public class CreateUtilisateurDTO
{
    public string? Nom
    {
        get; set;
    }

    public string? Prenom
    {
        get; set;
    }

    public string? Mobile
    {
        get; set;
    }

    public string? Mail
    {
        get; set;
    }

    public string? Pwd
    {
        get; set;
    }

    public string? Rue
    {
        get; set;
    }

    public string? CodePostal
    {
        get; set;
    }

    public string? Ville
    {
        get; set;
    }

    public string? Pays
    {
        get; set;
    }

    public float? Latitude
    {
        get; set;
    }

    public float? Longitude
    {
        get; set;
    }

    public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
}
