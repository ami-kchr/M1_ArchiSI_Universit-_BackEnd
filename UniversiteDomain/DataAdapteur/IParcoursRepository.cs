using System.Linq.Expressions;
using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapteur;

public interface IParcoursRepository : IRepository<Parcours>
{
    new Task<Parcours> CreateAsync(Parcours entity);
    new Task UpdateAsync(Parcours entity);
    new Task DeleteAsync(long id);
    new Task DeleteAsync(Parcours entity);
    new Task<Parcours?> FindAsync(long id);
    new Task<Parcours?> FindAsync(params object[] keyValues);
    new Task<Parcours?> FindByNomAsync(string nomParcours);
    new Task<List<Parcours>> FindByConditionAsync(Expression<Func<Parcours, bool>> condition);
    new Task<List<Parcours>> FindAllAsync();
    new Task SaveChangesAsync();
    Task<Parcours> AddEtudiantAsync(Parcours parcours, Etudiant etudiant);
    Task<Parcours> AddEtudiantAsync(long idParcours, long idEtudiant);
    Task<Parcours> AddEtudiantAsync(Parcours ? parcours, List<Etudiant> etudiants);
    Task<Parcours> AddEtudiantAsync(long idParcours, long[] idEtudiants);
    Task<Parcours> AddUeAsync(Parcours parcours, Ue ue);
    Task<Parcours> AddUeAsync(long idParcours, long idUe);
    Task<Parcours> AddUeAsync(Parcours? parcours, List<Ue> ues);
    Task<Parcours> AddUeAsync(long idParcours, long[] idUes);
}