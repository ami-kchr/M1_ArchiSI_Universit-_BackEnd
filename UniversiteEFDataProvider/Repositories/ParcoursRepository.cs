using Microsoft.EntityFrameworkCore;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapteur;
using UniversiteDomain.Entities;
using UniversiteEFDataProvider.Data;

namespace UniversiteEFDataProvider.Repositories;

public class ParcoursRepository(UniversiteDbContext context)
    : Repository<Parcours>(context), IParcoursRepository
{
    public async Task<Parcours> AddEtudiantAsync(long idParcours, long idEtudiant)
    {
        var parcours = await Context.Parcours!
            .Include(p => p.Inscrits)
            .FirstAsync(p => p.Id == idParcours);

        var etudiant = await Context.Etudiants!.FirstAsync(e => e.Id == idEtudiant);

        parcours.Inscrits!.Add(etudiant);

        await Context.SaveChangesAsync();
        return parcours;
    }

    public Task<Parcours?> FindByNomAsync(string nomParcours)
    {
        throw new NotImplementedException();
    }

    public async Task<Parcours> AddEtudiantAsync(Parcours parcours, Etudiant etudiant)
    {
        return await AddEtudiantAsync(parcours.Id, etudiant.Id);
    }

    public async Task<Parcours> AddEtudiantAsync(long idParcours, long[] idEtudiants)
    {
        var parcours = await Context.Parcours!
            .Include(p => p.Inscrits)
            .FirstAsync(p => p.Id == idParcours);

        foreach (var id in idEtudiants)
        {
            var etu = await Context.Etudiants!.FirstAsync(e => e.Id == id);
            parcours.Inscrits!.Add(etu);
        }

        await Context.SaveChangesAsync();
        return parcours;
    }

    public Task<Parcours> AddUeAsync(Parcours parcours, Ue ue)
    {
        throw new NotImplementedException();
    }

    public async Task<Parcours> AddUeAsync(long idParcours, long idUe)
    {
        var parcours = await Context.Parcours!
            .Include(p => p.UesEnseignees)
            .FirstAsync(p => p.Id == idParcours);

        var ue = await Context.Ues!.FirstAsync(u => u.Id == idUe);

        parcours.UesEnseignees!.Add(ue);

        await Context.SaveChangesAsync();
        return parcours;
    }

    public async Task<Parcours> AddUeAsync(long idParcours, long[] idUes)
    {
        var parcours = await Context.Parcours!
            .Include(p => p.UesEnseignees)
            .FirstAsync(p => p.Id == idParcours);

        foreach (var id in idUes)
        {
            var ue = await Context.Ues!.FirstAsync(u => u.Id == id);
            parcours.UesEnseignees!.Add(ue);
        }

        await Context.SaveChangesAsync();
        return parcours;
    }

    public async Task<Parcours> AddEtudiantAsync(Parcours? parcours, List<Etudiant> etudiants)
    {
        return await AddEtudiantAsync(parcours!.Id, etudiants.Select(e => e.Id).ToArray());
    }

    public async Task<Parcours> AddUeAsync(Parcours? parcours, List<Ue> ues)
    {
        return await AddUeAsync(parcours!.Id, ues.Select(u => u.Id).ToArray());
    }
}
