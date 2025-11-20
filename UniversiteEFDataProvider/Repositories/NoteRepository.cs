using Microsoft.EntityFrameworkCore;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.Entities;
using UniversiteEFDataProvider.Data;

namespace UniversiteEFDataProvider.Repositories;

public class NoteRepository(UniversiteDbContext context)
    : Repository<Note>(context), INoteRepository
{
    public async Task<Note?> FindNoteAsync(long idEtudiant, long idUe)
    {
        return await Context.Notes!
            .FirstOrDefaultAsync(n => n.EtudiantId == idEtudiant && n.UeId == idUe);
    }
}