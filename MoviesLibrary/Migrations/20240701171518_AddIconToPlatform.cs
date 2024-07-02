using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoviesLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddIconToPlatform : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    lastname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("actors_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    categoryname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("categories_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "platforms",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    platformname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("platforms_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    premierdate = table.Column<DateOnly>(type: "date", nullable: false),
                    imageurl = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    seenstatus = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    seendate = table.Column<DateOnly>(type: "date", nullable: true),
                    platformid = table.Column<int>(type: "integer", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("movies_pkey", x => x.id);
                    table.ForeignKey(
                        name: "movies_platformid_fkey",
                        column: x => x.platformid,
                        principalTable: "platforms",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "movieactor",
                columns: table => new
                {
                    movieid = table.Column<int>(type: "integer", nullable: false),
                    actorid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("movieactor_pkey", x => new { x.movieid, x.actorid });
                    table.ForeignKey(
                        name: "movieactor_actorid_fkey",
                        column: x => x.actorid,
                        principalTable: "actors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "movieactor_movieid_fkey",
                        column: x => x.movieid,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "moviecategory",
                columns: table => new
                {
                    movieid = table.Column<int>(type: "integer", nullable: false),
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("moviecategory_pkey", x => new { x.movieid, x.categoryid });
                    table.ForeignKey(
                        name: "moviecategory_categoryid_fkey",
                        column: x => x.categoryid,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "moviecategory_movieid_fkey",
                        column: x => x.movieid,
                        principalTable: "movies",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_movieactor_actorid",
                table: "movieactor",
                column: "actorid");

            migrationBuilder.CreateIndex(
                name: "IX_moviecategory_categoryid",
                table: "moviecategory",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_movies_platformid",
                table: "movies",
                column: "platformid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "movieactor");

            migrationBuilder.DropTable(
                name: "moviecategory");

            migrationBuilder.DropTable(
                name: "actors");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "movies");

            migrationBuilder.DropTable(
                name: "platforms");
        }
    }
}
