using System;
using System.Collections.Generic;
using System.Linq;

public interface IFilm
{
    string Title { get; set; }
    string Director { get; set; }
    int Year { get; set; }
}

public class Film : IFilm
{
    public string Title { get; set; }
    public string Director { get; set; }
    public int Year { get; set; }

    public Film(string title, string director, int year)
    {
        Title = title;
        Director = director;
        Year = year;
    }
}

public interface IFilmLibrary
{
    void AddFilm(IFilm film);
    void RemoveFilm(string title);
    List<IFilm> GetFilms();
    List<IFilm> SearchFilms(string query);
    int GetTotalFilmCount();
}

public class FilmLibrary : IFilmLibrary
{
    private List<IFilm> _films = new List<IFilm>();

    public void AddFilm(IFilm film)
    {
        _films.Add(film);
    }

    public void RemoveFilm(string title)
    {
        var film = _films.FirstOrDefault(f => f.Title == title);
        if (film != null)
            _films.Remove(film);
    }

    public List<IFilm> GetFilms()
    {
        return _films;
    }

    public List<IFilm> SearchFilms(string query)
    {
        return _films
            .Where(f => f.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
                     || f.Director.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public int GetTotalFilmCount()
    {
        return _films.Count;
    }
}

class Solution
{
    static void Main()
    {
        FilmLibrary library = new FilmLibrary();

        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(',');
            string title = input[0];
            string director = input[1];
            int year = Convert.ToInt32(input[2]);

            library.AddFilm(new Film(title, director, year));
        }

        string query = Console.ReadLine();

        var result = library.SearchFilms(query);

        foreach (var film in result)
        {
            Console.WriteLine($"{film.Title},{film.Director},{film.Year}");
        }

        Console.WriteLine("Total Films: " + library.GetTotalFilmCount());
    }
}