using Microsoft.EntityFrameworkCore;
using MusicApi;
using MusicApi.DataModel;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MusicApiTests
{
    public class MusicContextTests
    {
        private MusicDataContextFactory contextFactory = new MusicDataContextFactory();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Query_Playlist_Success()
        {
            IEnumerable<Album> albums = new List<Album>();

            using (MusicDataContext context = contextFactory.CreateDbContext())
            {
                IQueryable<Playlist> playlists = context.Playlists
                    .Include(x => x.PlaylistTracks)
                    .ThenInclude(x => x.Track)
                    .Where(x=> x.Name.Equals("Music Videos"));

                foreach (Playlist list in playlists)
                {
                    Debug.WriteLine($"({list.PlaylistId}) {list.Name}");
                    albums = list.PlaylistTracks.Select(x => x.Track.Album);

                    foreach (Album album in albums)
                    {
                        Debug.WriteLine(album.Title);
                    }
                }
            }

            Assert.True(albums.Any(), "Playlists should contain albums");
        }

        [Test]
        public void Create_Playlist_Success()
        {
            string playListName = "All Classical";

            using (MusicDataContext context = contextFactory.CreateDbContext())
            {
                IQueryable<Track> classicalSongs = context.Tracks.Where(x => x.Genre.Name == "Classical");
                
                Playlist playList = new Playlist();
                playList.Name = playListName;
                playList.PlaylistTracks = classicalSongs.Select(x => new PlaylistTrack()
                {
                    Track = x,
                    Playlist = playList
                }).ToList();

                context.Playlists.AddAsync(playList);
                context.SaveChanges();

                Playlist savedPlaylist = context.Playlists.Where(x => x.Name.Equals(playListName)).FirstOrDefault();

                Assert.True(savedPlaylist != null, $"Playlist ({playListName}) should have been saved");
            }
        }
    }
}