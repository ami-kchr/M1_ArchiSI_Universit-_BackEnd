using Moq;
using UniversiteDomain.DataAdapteur;
using UniversiteDomain.Entities;
using UniversiteDomain.Exceptions;
using UniversiteDomain.UseCase.ParcoursUseCases.Create;

namespace UniversiteDomaineUnitTest;

public class ParcoursUnitTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateParcoursUseCase_ValidParcours_ShouldReturnCreatedParcours()
    {
        long id = 1;
        string nomParcours = "Informatique";
        int anneeFormation = 1;

        // On crée le parcours qui doit être ajouté en base
        Parcours parcoursSansId = new Parcours
        {
            NomParcours = nomParcours,
            AnneeFormation = anneeFormation
        };

        // Créons le mock du repository
        var mock = new Mock<IParcoursRepository>();

        // Simulation de la fonction FindByNomAsync (pas de doublon)
        mock.Setup(repo => repo.FindByNomAsync(It.IsAny<string>()))
            .ReturnsAsync((Parcours?)null);

        // Simulation de la fonction CreateAsync
        Parcours parcoursCree = new Parcours
        {
            Id = id,
            NomParcours = nomParcours,
            AnneeFormation = anneeFormation
        };
        mock.Setup(repo => repo.CreateAsync(parcoursSansId))
            .ReturnsAsync(parcoursCree);

        // Création du faux repository
        var fauxParcoursRepository = mock.Object;

        // Création du use case en injectant notre faux repository
        CreateParcoursUseCase useCase = new CreateParcoursUseCase(fauxParcoursRepository);

        // Appel du use case
        var parcoursTeste = await useCase.ExecuteAsync(parcoursSansId);

        // Vérification du résultat
        Assert.That(parcoursTeste.Id, Is.EqualTo(parcoursCree.Id));
        Assert.That(parcoursTeste.NomParcours, Is.EqualTo(parcoursCree.NomParcours));
        Assert.That(parcoursTeste.AnneeFormation, Is.EqualTo(parcoursCree.AnneeFormation));
    }

    [Test]
    public void CreateParcoursUseCase_DuplicateNom_ShouldThrowException()
    {
        string nomParcours = "Informatique";
        int anneeFormation = 1;

        Parcours parcoursSansId = new Parcours
        {
            NomParcours = nomParcours,
            AnneeFormation = anneeFormation
        };

        var mock = new Mock<IParcoursRepository>();

        // On simule un parcours existant avec le même nom
        Parcours parcoursExistant = new Parcours
        {
            Id = 2,
            NomParcours = nomParcours,
            AnneeFormation = 2
        };
        mock.Setup(repo => repo.FindByNomAsync(It.IsAny<string>()))
            .ReturnsAsync(parcoursExistant);

        var fauxParcoursRepository = mock.Object;
        CreateParcoursUseCase useCase = new CreateParcoursUseCase(fauxParcoursRepository);

        // On s’attend à une exception DuplicateNomParcoursException
        Assert.ThrowsAsync<DuplicateNomParcoursException>(async () =>
        {
            await useCase.ExecuteAsync(parcoursSansId);
        });
    }

    [Test]
    public void CreateParcoursUseCase_InvalidAnnee_ShouldThrowException()
    {
        string nomParcours = "Maths";
        int anneeFormation = 3; // invalide (doit être 1 ou 2)

        Parcours parcoursSansId = new Parcours
        {
            NomParcours = nomParcours,
            AnneeFormation = anneeFormation
        };

        var mock = new Mock<IParcoursRepository>();
        mock.Setup(repo => repo.FindByNomAsync(It.IsAny<string>()))
            .ReturnsAsync((Parcours?)null);

        var fauxParcoursRepository = mock.Object;
        CreateParcoursUseCase useCase = new CreateParcoursUseCase(fauxParcoursRepository);

        Assert.ThrowsAsync<InvalidAnneeFormationException>(async () =>
        {
            await useCase.ExecuteAsync(parcoursSansId);
        });
    }
}
