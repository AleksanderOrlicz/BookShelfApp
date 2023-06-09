﻿using System.ComponentModel;

namespace BookShelfApp.Entities
{
    public class Book : EntityBase
    {   
        public Book() 
        {
            
        }
        public Book(string title)
        {
            this.Title = title;
        }
        public string? Title { get; set; }
        public const string? Category = "Book"; 
        //public string Description { get; set; }
        //public string Author { get; set; }
        
        public override string ToString() => $"Id: {Id}, Title: {Title} ";
    }
}
