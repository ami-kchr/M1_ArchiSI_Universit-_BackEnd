using UniversiteDomain.DataAdapteur;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions;

namespace UniversiteDomain.UseCase.ParcoursUseCases.Create;

public class CreateParcoursUseCase(IParcoursRepository parcoursRepository)
{
    public async Task<Parcours> ExecuteAsync(string nomParcours, int anneeFormation)
    {
        var parcours = new Parcours
        {
            NomParcours = nomParcours,
            AnneeFormation = anneeFormation
        };

        return await ExecuteAsync(parcours);
    }

    public async Task<Parcours> ExecuteAsync(Parcours parcours)
    {
        await CheckBusinessRules(parcours);
        Parcours p = await parcoursRepository.CreateAsync(parcours);
        parcoursRepository.SaveChangesAsync().Wait();
        return p;
    }

    private async Task CheckBusinessRules(Parcours parcours)
    {
        ArgumentNullException.ThrowIfNull(parcours);
        ArgumentNullException.ThrowIfNull(parcours.NomParcours);
        ArgumentNullException.ThrowIfNull(parcoursRepository);

        // Vérification de la longueur minimale du nom
        if (parcours.NomParcours.Length < 3)
            throw new ArgumentException(parcours.NomParcours + " incorrect - Le nom d'un parcours doit contenir au moins 3 caractères");

        // Vérification de l’année de formation (entre 1 et 2)
        if (parcours.AnneeFormation is not (1 or 2))
            throw new InvalidAnneeFormationException(parcours.AnneeFormation + " n’est pas une année valide pour un parcours (valeurs acceptées : 1 à 5)");

        // Vérification de l’unicité du nom du parcours
        List<Parcours> existe = await parcoursRepository.FindByConditionAsync(p => p.NomParcours.Equals(parcours.NomParcours));

        if (existe is { Count: > 0 })
            throw new DuplicateNomParcoursException(parcours.NomParcours + " existe déjà.");
    }
}