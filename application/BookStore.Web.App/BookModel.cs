﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Web.App
{
    public class BookModel
    {
        public string Isbn { get; set; }
        public string Author { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}