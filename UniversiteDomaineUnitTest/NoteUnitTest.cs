using System.Linq.Expressions;
using Moq;
using UniversiteDomain.DataAdapters;
using UniversiteDomain.DataAdapters.DataAdaptersFactory;
using UniversiteDomain.DataAdapteur;
using UniversiteDomain.Entities;
using UniversiteDomain.UseCases.NoteUseCases.Add;

namespace UniversiteDomaineUnitTest;

public class NoteUnitTest
{

    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public async Task AddNoteUseCase_Test()
    {
        long idEtud = 1;
        long idUe = 2;

        var etudiant = new Etudiant
        {
            Id = idEtud,
            Nom = "Durant",
            ParcoursSuivi = new Parcours
            {
                Id = 10,
                NomParcours = "MIAGE",
                UesEnseignees = new List<Ue>()
            }
        };

        var ue = new Ue
        {
            Id = idUe,
            NumeroUe = "UE22",
            Intitule = "Base de données"
        };

        etudiant.ParcoursSuivi.UesEnseignees.Add(ue);

        // Mocks
        var mockFactory = new Mock<IRepositoryFactory>();

        var mockEtudRepo = new Mock<IEtudiantRepository>();
        mockEtudRepo.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Etudiant, bool>>>()))
            .ReturnsAsync(new List<Etudiant> { etudiant });

        var mockUeRepo = new Mock<IUeRepository>();
        mockUeRepo.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Ue, bool>>>()))
            .ReturnsAsync(new List<Ue> { ue });

        var mockNoteRepo = new Mock<INoteRepository>();
        mockNoteRepo.Setup(r => r.FindNoteAsync(idEtud, idUe))
            .ReturnsAsync((Note?)null); // pas encore noté

        mockFactory.Setup(f => f.EtudiantRepository()).Returns(mockEtudRepo.Object);
        mockFactory.Setup(f => f.UeRepository()).Returns(mockUeRepo.Object);
        mockFactory.Setup(f => f.NoteRepository()).Returns(mockNoteRepo.Object);

        var useCase = new AddNoteUseCase(mockFactory.Object);

        var note = await useCase.ExecuteAsync(idEtud, idUe, 15);

        Assert.That(note.Valeur, Is.EqualTo(15));
        Assert.That(note.EtudiantId, Is.EqualTo(idEtud));
        Assert.That(note.UeId, Is.EqualTo(idUe));
    }

}