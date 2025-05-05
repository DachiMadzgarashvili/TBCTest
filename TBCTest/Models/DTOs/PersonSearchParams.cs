using System.ComponentModel.DataAnnotations;

namespace TBCTest.Models.DTOs
{
    /// <summary>
    /// Parameters for searching and paging Persons.
    /// </summary>
    public class PersonSearchParams
    {
        /// <summary> Quick search across names and personal number (SQL LIKE) </summary>
        public string? Quick { get; set; }

        /// <summary> Filter by Georgian first name (LIKE) </summary>
        public string? FirstNameGe { get; set; }

        /// <summary> Filter by English first name (LIKE) </summary>
        public string? FirstNameEn { get; set; }

        /// <summary> Filter by Georgian last name (LIKE) </summary>
        public string? LastNameGe { get; set; }

        /// <summary> Filter by English last name (LIKE) </summary>
        public string? LastNameEn { get; set; }

        /// <summary> Filter by gender exact match </summary>
        public string? Gender { get; set; }

        /// <summary> Filter by personal number (LIKE) </summary>
        public string? PersonalNumber { get; set; }

        /// <summary> Filter by CityId exact match </summary>
        public int? CityId { get; set; }

        /// <summary> Page number (1-based) </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary> Items per page </summary>
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;
    }
}
