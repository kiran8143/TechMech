using System.Collections.Generic;
using TechMech.Models;

namespace TechMech.ViewModels
{
    public class RandomMovieViewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}