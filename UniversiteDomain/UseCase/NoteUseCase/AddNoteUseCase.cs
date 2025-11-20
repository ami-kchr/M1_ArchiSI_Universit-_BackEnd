using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions;
using UniversiteDomain.Exceptions.NoteExceptions;

namespace UniversiteDomain.UseCases.NoteUseCases.Add;

public class AddNoteUseCase(IRepositoryFactory factory)
{
    public async Task<Note> ExecuteAsync(long idEtudiant, long idUe, float valeur)
    {
        await CheckBusinessRules(idEtudiant, idUe, valeur);

        var note = new Note
        {
            EtudiantId = idEtudiant,
            UeId = idUe,
            Valeur = valeur
        };

        var repo = factory.NoteRepository();
        await repo.CreateAsync(note);
        await factory.SaveChangesAsync();

        return note;
    }

    private async Task CheckBusinessRules(long idEtudiant, long idUe, double valeur)
    {
        // 1. Valeur entre 0 et 20
        if (valeur < 0 || valeur > 20)
            throw new NoteOutOfRangeException($"La note {valeur} n'est pas valide. Elle doit être comprise entre 0 et 20.");

        // 2. L'étudiant existe
        var etudiants = await factory.EtudiantRepository()
            .FindByConditionAsync(e => e.Id == idEtudiant);

        if (etudiants.Count == 0)
            throw new EtudiantNotFoundException(idEtudiant.ToString());

        var etudiant = etudiants[0];

        // 3. L'UE existe
        var ues = await factory.UeRepository()
            .FindByConditionAsync(u => u.Id == idUe);

        if (ues.Count == 0)
            throw new UeNotFoundException(idUe.ToString());

        var ue = ues[0];

        // 4. Vérifier que l'UE appartient au parcours de l'étudiant
        if (etudiant.ParcoursSuivi == null ||
            etudiant.ParcoursSuivi.UesEnseignees?.All(u => u.Id != idUe) == true)
        {
            throw new NoteUeNotInParcoursException(
                $"UE {ue.NumeroUe} n'est pas enseignée dans le parcours {etudiant.ParcoursSuivi?.NomParcours}");
        }

        // 5. Vérifier que l'étudiant n'a pas déjà une note dans cette UE
        var ancienneNote = await factory.NoteRepository()
            .FindNoteAsync(idEtudiant, idUe);

        if (ancienneNote != null)
            throw new DuplicateNoteException(
                $"L'étudiant {idEtudiant} a déjà une note dans l'UE {idUe}");
    }
}
