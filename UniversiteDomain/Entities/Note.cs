namespace UniversiteDomain.Entities;

public class Note
{
    public float Valeur { get; set; }

    // Association vers Etudiant
    public long EtudiantId { get; set; }
    public Etudiant Etudiant { get; set; } = null!;
    
    

    // Association vers Ue
    public long UeId { get; set; }
    public Ue Ue { get; set; } = null!;

    public override string ToString()
    {
        return $"Note {Valeur}/20 pour UE {Ue?.NumeroUe} de l'étudiant {Etudiant?.Nom}";
    }
}