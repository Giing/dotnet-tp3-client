using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using SeriesApp.DTO;
using SeriesApp.Models;

namespace SeriesApp.Services;
public class WSService
{
    private static WSService instance = null;
    private HttpClient client = new HttpClient();
    private string path;
    private IMapper mapper;
    
    public WSService()
    {
        var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
        path = loader.GetString("WSSeriesLocalUri");
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Utilisateur, CreateUtilisateurDTO>();
        });
        mapper = config.CreateMapper();
    }

    public static WSService GetInstance()
    {
        if (instance == null)
        {
            instance = new WSService();
        }
        return instance;
    }

    public async Task<Utilisateur> GetUtilisateurByEmail(string email)
    {
        try
        {
            Utilisateur utilisateur = null;
            HttpResponseMessage response = await client.GetAsync(path + "Utilisateurs/GetUtilisateurByEmail/" + email.ToLower());

            if(response.IsSuccessStatusCode)
                utilisateur = await response.Content.ReadAsAsync<Utilisateur>();

            return utilisateur;
        }
        catch
        {
            return null;
        }
    }



    public async Task<bool> SaveUtilisateur(Utilisateur utilisateur)
    {
        try
        {
            if(utilisateur.NotesUtilisateur == null)
                utilisateur.NotesUtilisateur = new List<Notation>();

            string json = JsonConvert.SerializeObject(utilisateur);
            HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(path + "Utilisateurs/" + utilisateur.UtilisateurId, body);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Utilisateur> AddUtilisateur(Utilisateur utilisateur)
    {
        try
        {
            CreateUtilisateurDTO dto = mapper.Map<Utilisateur, CreateUtilisateurDTO>(utilisateur);

            if (dto.NotesUtilisateur == null)
                dto.NotesUtilisateur = new List<Notation>();

            string json = JsonConvert.SerializeObject(dto);
            HttpContent body = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(path + "Utilisateurs/PostUtilisateur", body);

            if (response.IsSuccessStatusCode)
                utilisateur = await response.Content.ReadAsAsync<Utilisateur>();

            return utilisateur;
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}
