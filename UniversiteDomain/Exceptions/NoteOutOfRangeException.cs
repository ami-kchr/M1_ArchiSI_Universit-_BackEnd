namespace UniversiteDomain.Exceptions.NoteExceptions;

public class NoteOutOfRangeException : Exception
{
    public NoteOutOfRangeException(string message) : base(message) { }
}