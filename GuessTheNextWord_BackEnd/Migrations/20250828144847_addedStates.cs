﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessTheNextWord_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class addedStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Games");
        }
    }
}
