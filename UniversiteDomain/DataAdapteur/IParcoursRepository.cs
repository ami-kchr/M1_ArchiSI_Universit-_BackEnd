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
}