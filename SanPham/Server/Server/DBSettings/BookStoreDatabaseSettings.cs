﻿namespace Server.DBSettings
{
    public class BookStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;

        public string CategoryCollection {  get; set; } = null!;

        public string RefreshTokenCollection {  get; set; } = null!;
    }
}
