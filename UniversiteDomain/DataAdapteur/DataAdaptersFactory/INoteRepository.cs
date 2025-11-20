using UniversiteDomain.DataAdapteur;
using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapters;

public interface INoteRepository : IRepository<Note>
{
    Task<Note?> FindNoteAsync(long idEtudiant, long idUe);
}