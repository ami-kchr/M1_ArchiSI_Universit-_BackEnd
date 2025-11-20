using UniversiteDomain.DataAdapteur;

namespace UniversiteDomain.DataAdapters.DataAdaptersFactory;

public interface IRepositoryFactory
{
    IUeRepository UeRepository();
    IParcoursRepository ParcoursRepository();
    IEtudiantRepository EtudiantRepository();
    INoteRepository NoteRepository();

    // Méthodes de gestion de la dadasource
    // Ce sont des méthodes qui permettent de gérer l'ensemble du data source
    // comme par exemple tout supprimer ou tout créer
    Task EnsureDeletedAsync();
    Task EnsureCreatedAsync();
    Task SaveChangesAsync();
}