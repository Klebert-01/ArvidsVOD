namespace VOD.Common.DTOs;


    public class DirectorDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class CreateDirectorDTO
    {
        public string? Name { get; set; }
    }

    public class FullDirectorDTO : DirectorDTO
    {
        public virtual List<FilmDTO> Films { get; set; } = new();
    }
