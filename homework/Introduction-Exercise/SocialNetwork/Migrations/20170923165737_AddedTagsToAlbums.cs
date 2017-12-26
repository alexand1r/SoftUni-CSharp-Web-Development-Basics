using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Migrations
{
    public partial class AddedTagsToAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlbumId",
                table: "Tags",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AlbumId",
                table: "Tags",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Albums_AlbumId",
                table: "Tags",
                column: "AlbumId",
                principalTable: "Albums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Albums_AlbumId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_AlbumId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "AlbumId",
                table: "Tags");
        }
    }
}
