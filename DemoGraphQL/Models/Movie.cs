﻿using System;

namespace DemoGraphQL.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Length { get; set; }
        public int DirectorId { get; set; }
    }
}
