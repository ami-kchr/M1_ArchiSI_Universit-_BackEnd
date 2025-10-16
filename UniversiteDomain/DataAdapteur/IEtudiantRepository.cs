﻿using System.Linq.Expressions;
using UniversiteDomain.Entities;

namespace UniversiteDomain.DataAdapteur;
 
public interface IEtudiantRepository : IRepository<Etudiant>
{
    new Task<Etudiant> CreateAsync(Etudiant entity);
    new Task UpdateAsync(Etudiant entity);
    new Task DeleteAsync(long id);
    new Task DeleteAsync(Etudiant entity);
    new Task<Etudiant?> FindAsync(long id);
    new Task<Etudiant?> FindAsync(params object[] keyValues);
    new Task<List<Etudiant>> FindByConditionAsync(Expression<Func<Etudiant, bool>> condition);
    new Task<List<Etudiant>> FindAllAsync();
    new Task SaveChangesAsync();
}