using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineLife;

namespace OnlineLifeTests
{
    [TestClass]
    public class PlaylistTest
    {
        [TestMethod]
        public void ParseFromJSON()
        {
            string json = @"{
    'playlist': [
        {
            'comment':'Season 1',
            'playlist':[
                {
                    'comment': 'Episode 1.1',
                    'file': 'http://test.example.com/file_1_1.flv',
                    'download': 'http://test.example.com/download_1_1.flv'
                },
                {
                    'comment': 'Episode 1.2',
                    'file': 'http://test.example.com/file_1_2.flv',
                    'download': 'http://test.example.com/download_1_2.flv'
                }
            ]
        },
        {
            'comment':'Season 2',
            'playlist':[
                {
                    'comment': 'Episode 2.1',
                    'file': 'http://test.example.com/file_2_1.flv',
                    'download': 'http://test.example.com/download_2_1.flv'
                },
                {
                    'comment': 'Episode 2.2',
                    'file': 'http://test.example.com/file_2_2.flv',
                    'download': 'http://test.example.com/download_2_2.flv'
                },
                {
                    'comment': 'Episode 2.3',
                    'file': 'http://test.example.com/file_2_3.flv',
                    'download': 'http://test.example.com/download_2_3.flv'
                }
            ]
        }
    ]
}";
            var expected = new Playlist(new Season[] {
                new Season("Season 1", new Episode[] {
                    new Episode(
                        "Episode 1.1",
                        new Uri("http://test.example.com/file_1_1.flv"),
                        new Uri("http://test.example.com/download_1_1.flv")),
                    new Episode(
                        "Episode 1.2",
                        new Uri("http://test.example.com/file_1_2.flv"),
                        new Uri("http://test.example.com/download_1_2.flv")),
                }),
                new Season("Season 2", new Episode[] {
                    new Episode(
                        "Episode 2.1",
                        new Uri("http://test.example.com/file_2_1.flv"),
                        new Uri("http://test.example.com/download_2_1.flv")),
                    new Episode(
                        "Episode 2.2",
                        new Uri("http://test.example.com/file_2_2.flv"),
                        new Uri("http://test.example.com/download_2_2.flv")),
                    new Episode(
                        "Episode 2.3",
                        new Uri("http://test.example.com/file_2_3.flv"),
                        new Uri("http://test.example.com/download_2_3.flv")),
                }),
            });

            var actual = Playlist.FromJSON(json);

            Assert.AreEqual(expected, actual);
        }
    }
}
