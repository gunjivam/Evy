using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Eevee.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertiserType",
                columns: table => new
                {
                    AdvertiserTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertiserType", x => x.AdvertiserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    ArtistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Listens = table.Column<int>(nullable: false),
                    Rating = table.Column<float>(nullable: false),
                    WordVec = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.ArtistID);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    FrequencyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hz = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.FrequencyID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentManufacturer",
                columns: table => new
                {
                    InstrumentManufacturerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentManufacturer", x => x.InstrumentManufacturerID);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentType",
                columns: table => new
                {
                    InstrumentTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentType", x => x.InstrumentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Advertiser",
                columns: table => new
                {
                    AdvertiserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    CardNumber = table.Column<long>(nullable: false),
                    MonthsRemaining = table.Column<int>(nullable: false),
                    AutoPay = table.Column<bool>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Clicks = table.Column<int>(nullable: false),
                    Views = table.Column<int>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    AdvertiserTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertiser", x => x.AdvertiserID);
                    table.ForeignKey(
                        name: "FK_Advertiser_AdvertiserType_AdvertiserTypeID",
                        column: x => x.AdvertiserTypeID,
                        principalTable: "AdvertiserType",
                        principalColumn: "AdvertiserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ArtistID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.AlbumID);
                    table.ForeignKey(
                        name: "FK_Album_Artist_ArtistID",
                        column: x => x.ArtistID,
                        principalTable: "Artist",
                        principalColumn: "ArtistID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    InstrumentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    GenreID = table.Column<int>(nullable: false),
                    InstrumentManufacturerID = table.Column<int>(nullable: false),
                    InstrumentTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.InstrumentID);
                    table.ForeignKey(
                        name: "FK_Instrument_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instrument_InstrumentManufacturer_InstrumentManufacturerID",
                        column: x => x.InstrumentManufacturerID,
                        principalTable: "InstrumentManufacturer",
                        principalColumn: "InstrumentManufacturerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Instrument_InstrumentType_InstrumentTypeID",
                        column: x => x.InstrumentTypeID,
                        principalTable: "InstrumentType",
                        principalColumn: "InstrumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    InstrumentTypeID = table.Column<int>(nullable: false),
                    FrequencyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Note_Frequency_FrequencyID",
                        column: x => x.FrequencyID,
                        principalTable: "Frequency",
                        principalColumn: "FrequencyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Note_InstrumentType_InstrumentTypeID",
                        column: x => x.InstrumentTypeID,
                        principalTable: "InstrumentType",
                        principalColumn: "InstrumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistID);
                    table.ForeignKey(
                        name: "FK_Playlist_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Song",
                columns: table => new
                {
                    SongID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Duration = table.Column<string>(nullable: false),
                    Listens = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Lyrics = table.Column<string>(nullable: true),
                    WordVec = table.Column<string>(nullable: true),
                    GenreID = table.Column<int>(nullable: true),
                    AlbumID = table.Column<int>(nullable: true),
                    Fp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Song", x => x.SongID);
                    table.ForeignKey(
                        name: "FK_Song_Album_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Album",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Song_Genre_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "GenreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongInstrumentAssignment",
                columns: table => new
                {
                    SongInstrumentAssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(nullable: false),
                    InstrumentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongInstrumentAssignment", x => x.SongInstrumentAssignmentID);
                    table.ForeignKey(
                        name: "FK_SongInstrumentAssignment_Instrument_InstrumentID",
                        column: x => x.InstrumentID,
                        principalTable: "Instrument",
                        principalColumn: "InstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    HistoryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_History_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaylistSongAssignment",
                columns: table => new
                {
                    PlaylistSongAssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaylistID = table.Column<int>(nullable: false),
                    SongID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistSongAssignment", x => x.PlaylistSongAssignmentID);
                    table.ForeignKey(
                        name: "FK_PlaylistSongAssignment_Playlist_PlaylistID",
                        column: x => x.PlaylistID,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistSongAssignment_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SongFrequencyAssignment",
                columns: table => new
                {
                    SongFrequencyAssignmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SongID = table.Column<int>(nullable: false),
                    FrequencyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongFrequencyAssignment", x => x.SongFrequencyAssignmentID);
                    table.ForeignKey(
                        name: "FK_SongFrequencyAssignment_Frequency_FrequencyID",
                        column: x => x.FrequencyID,
                        principalTable: "Frequency",
                        principalColumn: "FrequencyID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SongFrequencyAssignment_Song_SongID",
                        column: x => x.SongID,
                        principalTable: "Song",
                        principalColumn: "SongID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertiser_AdvertiserTypeID",
                table: "Advertiser",
                column: "AdvertiserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Album_ArtistID",
                table: "Album",
                column: "ArtistID");

            migrationBuilder.CreateIndex(
                name: "IX_History_SongID",
                table: "History",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_History_UserID",
                table: "History",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_GenreID",
                table: "Instrument",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_InstrumentManufacturerID",
                table: "Instrument",
                column: "InstrumentManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_InstrumentTypeID",
                table: "Instrument",
                column: "InstrumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_FrequencyID",
                table: "Note",
                column: "FrequencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Note_InstrumentTypeID",
                table: "Note",
                column: "InstrumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Playlist_UserID",
                table: "Playlist",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongAssignment_PlaylistID",
                table: "PlaylistSongAssignment",
                column: "PlaylistID");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistSongAssignment_SongID",
                table: "PlaylistSongAssignment",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_AlbumID",
                table: "Song",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_Song_GenreID",
                table: "Song",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_SongFrequencyAssignment_FrequencyID",
                table: "SongFrequencyAssignment",
                column: "FrequencyID");

            migrationBuilder.CreateIndex(
                name: "IX_SongFrequencyAssignment_SongID",
                table: "SongFrequencyAssignment",
                column: "SongID");

            migrationBuilder.CreateIndex(
                name: "IX_SongInstrumentAssignment_InstrumentID",
                table: "SongInstrumentAssignment",
                column: "InstrumentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertiser");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Note");

            migrationBuilder.DropTable(
                name: "PlaylistSongAssignment");

            migrationBuilder.DropTable(
                name: "SongFrequencyAssignment");

            migrationBuilder.DropTable(
                name: "SongInstrumentAssignment");

            migrationBuilder.DropTable(
                name: "AdvertiserType");

            migrationBuilder.DropTable(
                name: "Playlist");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "Song");

            migrationBuilder.DropTable(
                name: "Instrument");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "InstrumentManufacturer");

            migrationBuilder.DropTable(
                name: "InstrumentType");

            migrationBuilder.DropTable(
                name: "Artist");
        }
    }
}
